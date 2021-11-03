using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;

// TODO localization
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
        private void DeltaTable(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name FROM src_tables s" +
                " LEFT JOIN tgt_tables t USING (table_name)" +
                " WHERE t.table_name IS NULL" +
                " ORDER BY table_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "TABLE", (string)dr["table_name"], "Table dans source absente en cible");
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.table_name FROM tgt_tables t" +
                " LEFT JOIN src_tables s USING (table_name)" +
                " WHERE s.table_name IS NULL" +
                " ORDER BY table_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "TABLE", (string)dr["table_name"], "Table dans cible absente en source");
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_tables";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceTable = new Table
                    {
                        TableName = (string)dr["table_name"],
                        TablespaceName = dr["src_tablespace_name"] is DBNull ? null : (string)dr["src_tablespace_name"]
                    };
                    var targetTable = new Table
                    {
                        TableName = (string)dr["table_name"],
                        TablespaceName = dr["tgt_tablespace_name"] is DBNull ? null : (string)dr["tgt_tablespace_name"]
                    };
                    sourceTable.Compare(targetTable, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
