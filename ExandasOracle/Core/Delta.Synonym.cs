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
        private void DeltaSynonym(FbConnection conn, List<Synonym> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            if (this._comparisonSet.Schema1 == this._comparisonSet.Schema2)
            {
                sql = "SELECT s.owner, s.synonym_name FROM src_synonyms s" +
                    " LEFT JOIN tgt_synonyms t USING(owner, synonym_name)" +
                    " WHERE t.owner IS NULL" +
                    " ORDER BY owner, synonym_name";
            }
            else
            {
                sql = "SELECT s.owner, s.synonym_name FROM src_synonyms s" +
                    " LEFT JOIN tgt_synonyms t USING(owner, synonym_name)" +
                    " WHERE t.owner IS NULL AND s.owner = 'PUBLIC'" +
                    " ORDER BY owner, synonym_name";
            }
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var parentObject = string.Format("owner={0}", (string)dr["owner"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "SYNONYM", (string)dr["synonym_name"], parentObject, Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            if (this._comparisonSet.Schema1 == this._comparisonSet.Schema2)
            {
                sql = "SELECT t.owner, t.synonym_name FROM tgt_synonyms t" +
                    " LEFT JOIN src_synonyms s USING(owner, synonym_name)" +
                    " WHERE s.owner IS NULL" +
                    " ORDER BY owner, synonym_name";
            }
            else
            {
                sql = "SELECT t.owner, t.synonym_name FROM tgt_synonyms t" +
                    " LEFT JOIN src_synonyms s USING(owner, synonym_name)" +
                    " WHERE s.owner IS NULL AND t.owner = 'PUBLIC'" +
                    " ORDER BY owner, synonym_name";
            }
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var parentObject = string.Format("owner={0}", (string)dr["owner"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "SYNONYM", (string)dr["synonym_name"], parentObject, Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_synonyms";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceSynonym = new Synonym
                    {
                        Owner = (string)dr["owner"],
                        SynonymName = (string)dr["synonym_name"],
                        TableOwner = dr["src_table_owner"] is DBNull ? null : (string)dr["src_table_owner"],
                        TableName = dr["src_table_name"] is DBNull ? null : (string)dr["src_table_name"],
                        DbLink = dr["src_db_link"] is DBNull ? null : (string)dr["src_db_link"],
                    };
                    var targetSynonym = new Synonym
                    {
                        Owner = (string)dr["owner"],
                        SynonymName = (string)dr["synonym_name"],
                        TableOwner = dr["tgt_table_owner"] is DBNull ? null : (string)dr["tgt_table_owner"],
                        TableName = dr["tgt_table_name"] is DBNull ? null : (string)dr["tgt_table_name"],
                        DbLink = dr["tgt_db_link"] is DBNull ? null : (string)dr["tgt_db_link"],
                    };
                    sourceSynonym.Compare(targetSynonym, this._comparisonSet, list);
                }
            }
        }

    }
}
