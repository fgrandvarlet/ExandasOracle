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
        private void DeltaDatabaseLink(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.db_link FROM src_db_links s" +
                " LEFT JOIN tgt_db_links t USING(db_link)" +
                " WHERE t.db_link IS NULL" +
                " ORDER BY db_link";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "DATABASE LINK", (string)dr["db_link"], Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.db_link FROM tgt_db_links t" +
                " LEFT JOIN src_db_links s USING(db_link)" +
                " WHERE s.db_link IS NULL" +
                " ORDER BY db_link";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "DATABASE_LINK", (string)dr["db_link"], Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_db_links";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceDatabaseLink = new DatabaseLink
                    {
                        DbLink = (string)dr["db_link"],
                        Username = dr["src_username"] is DBNull ? null : (string)dr["src_username"],
                        Host = dr["src_host"] is DBNull ? null : (string)dr["src_host"],
                        ShardInternal = dr["src_shard_internal"] is DBNull ? null : (string)dr["src_shard_internal"],
                        Valid = dr["src_valid"] is DBNull ? null : (string)dr["src_valid"],
                    };
                    var targetDatabaseLink = new DatabaseLink
                    {
                        DbLink = (string)dr["db_link"],
                        Username = dr["tgt_username"] is DBNull ? null : (string)dr["tgt_username"],
                        Host = dr["tgt_host"] is DBNull ? null : (string)dr["tgt_host"],
                        ShardInternal = dr["tgt_shard_internal"] is DBNull ? null : (string)dr["tgt_shard_internal"],
                        Valid = dr["tgt_valid"] is DBNull ? null : (string)dr["tgt_valid"],
                    };
                    sourceDatabaseLink.Compare(targetDatabaseLink, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
