using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Core;
using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Dao.Firebird
{
    public class ReferenceDaoFirebird : AbstractDaoFirebird, IReferenceDao
    {
        public ReferenceDaoFirebird(string connectionString) : base(connectionString)
        {
        }

        protected override FbCommand CreateCommand(Criteria criteria)
        {
            throw new NotImplementedException();
        }

        public bool NeedInitialization()
        {
            const string sql = "SELECT count(*) FROM entity_reference";
            bool result;

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);
                int count = (int)cmd.ExecuteScalar();
                result = count == 0;
            }
            return result;
        }

        public void InitializeReferences()
        {
            string sql;
            FbCommand cmd;

            var properties = BuildPropertyReferenceList();
            var entities = BuildEntityReferenceSet(properties);

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                FbTransaction tran = conn.BeginTransaction();
                try
                {
                    sql = "INSERT INTO entity_reference VALUES(@entity)";
                    cmd = new FbCommand(sql, conn, tran);
                    cmd.Prepare();

                    foreach (EntityReference er in entities)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("entity", er.Entity);
                        cmd.ExecuteNonQuery();
                    }

                    sql = "INSERT INTO property_reference VALUES(@entity, @property)";
                    cmd = new FbCommand(sql, conn, tran);
                    cmd.Prepare();

                    foreach (PropertyReference pr in properties)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("entity", pr.Entity);
                        cmd.Parameters.AddWithValue("property", pr.Property);
                        cmd.ExecuteNonQuery();
                    }

                    tran.Commit();
                }
                catch (Exception)
                {
                    tran.Rollback();
                    throw;
                }
            }

        }

        private static List<PropertyReference> BuildPropertyReferenceList()
        {
            var list = new List<PropertyReference>();

            var references = Resources.REFERENCES;
            foreach (var line in references.SplitToLines())
            {
                var items = line.Split(';');
                var propertyReference = new PropertyReference
                {
                    Entity = items[0],
                    Property = items[1],
                };
                list.Add(propertyReference);
            }
            return list;
        }

        private static HashSet<EntityReference> BuildEntityReferenceSet(List<PropertyReference> properties)
        {
            HashSet<EntityReference> hset = new HashSet<EntityReference>();

            foreach (PropertyReference pr in properties)
            {
                hset.Add(new EntityReference { Entity = pr.Entity });
            }
            return hset;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EntityReference> GetEntityReferenceList()
        {
            var list = new List<EntityReference>();

            const string sql = "SELECT entity FROM entity_reference ORDER BY entity";

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);
                using (FbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var er = new EntityReference();

                        er.Entity = (string)dr["entity"];

                        list.Add(er);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="er"></param>
        /// <returns></returns>
        public List<PropertyReference> GetPropertyReferenceListByEntity(EntityReference er)
        {
            var list = new List<PropertyReference>();

            const string sql = "SELECT entity, property FROM property_reference WHERE entity = @entity ORDER BY property";

            using (FbConnection conn = GetFirebirdConnection())
            {
                conn.Open();
                var cmd = new FbCommand(sql, conn);
                cmd.Parameters.AddWithValue("entity", er.Entity);

                using (FbDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        var pr = new PropertyReference();

                        pr.Entity = (string)dr["entity"];
                        pr.Property = (string)dr["property"];

                        list.Add(pr);
                    }
                }
            }
            return list;
        }

    }
}
