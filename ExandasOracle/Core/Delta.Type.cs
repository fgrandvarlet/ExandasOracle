using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;
using ExandasOracle.Properties;

namespace ExandasOracle.Core
{
    public partial class Delta
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="list"></param>
        private void DeltaType(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.type_name FROM src_types s" +
                " LEFT JOIN tgt_types t USING(type_name)" +
                " WHERE t.type_name IS NULL" +
                " ORDER BY type_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "TYPE", (string)dr["type_name"], null, Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.type_name FROM tgt_types t" +
                " LEFT JOIN src_types s USING(type_name)" +
                " WHERE s.type_name IS NULL" +
                " ORDER BY type_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "TYPE", (string)dr["type_name"], null, Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_types";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceType = new Type
                    {
                        TypeName = (string)dr["type_name"],
                        TypeCode = dr["src_type_code"] is DBNull ? null : (string)dr["src_type_code"],
                        Attributes = dr["src_attributes"] is DBNull ? null : (decimal?)dr["src_attributes"],
                        Methods = dr["src_methods"] is DBNull ? null : (decimal?)dr["src_methods"],
                        Predefined = dr["src_predefined"] is DBNull ? null : (string)dr["src_predefined"],
                        Incomplete = dr["src_incomplete"] is DBNull ? null : (string)dr["src_incomplete"],
                        Final = dr["src_final"] is DBNull ? null : (string)dr["src_final"],
                        Instantiable = dr["src_instantiable"] is DBNull ? null : (string)dr["src_instantiable"],
                        Persistable = dr["src_persistable"] is DBNull ? null : (string)dr["src_persistable"],
                        SupertypeOwner = dr["src_supertype_owner"] is DBNull ? null : (string)dr["src_supertype_owner"],
                        SupertypeName = dr["src_supertype_name"] is DBNull ? null : (string)dr["src_supertype_name"],
                        LocalAttributes = dr["src_local_attributes"] is DBNull ? null : (decimal?)dr["src_local_attributes"],
                        LocalMethods = dr["src_local_methods"] is DBNull ? null : (decimal?)dr["src_local_methods"],
                    };
                    var targetType = new Type
                    {
                        TypeName = (string)dr["type_name"],
                        TypeCode = dr["tgt_type_code"] is DBNull ? null : (string)dr["tgt_type_code"],
                        Attributes = dr["tgt_attributes"] is DBNull ? null : (decimal?)dr["tgt_attributes"],
                        Methods = dr["tgt_methods"] is DBNull ? null : (decimal?)dr["tgt_methods"],
                        Predefined = dr["tgt_predefined"] is DBNull ? null : (string)dr["tgt_predefined"],
                        Incomplete = dr["tgt_incomplete"] is DBNull ? null : (string)dr["tgt_incomplete"],
                        Final = dr["tgt_final"] is DBNull ? null : (string)dr["tgt_final"],
                        Instantiable = dr["tgt_instantiable"] is DBNull ? null : (string)dr["tgt_instantiable"],
                        Persistable = dr["tgt_persistable"] is DBNull ? null : (string)dr["tgt_persistable"],
                        SupertypeOwner = dr["tgt_supertype_owner"] is DBNull ? null : (string)dr["tgt_supertype_owner"],
                        SupertypeName = dr["tgt_supertype_name"] is DBNull ? null : (string)dr["tgt_supertype_name"],
                        LocalAttributes = dr["tgt_local_attributes"] is DBNull ? null : (decimal?)dr["tgt_local_attributes"],
                        LocalMethods = dr["tgt_local_methods"] is DBNull ? null : (decimal?)dr["tgt_local_methods"],
                    };
                    sourceType.Compare(targetType, this._comparisonSet, list);
                }
            }
        }

    }
}
