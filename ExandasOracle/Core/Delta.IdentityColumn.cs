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
        private void DeltaIdentityColumn(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name, s.column_name FROM src_tab_identity_cols s" +
                " LEFT JOIN tgt_tab_identity_cols t USING(table_name, column_name)" +
                " JOIN common_tab_cols USING(table_name, column_name)" +
                " WHERE t.table_name IS NULL" +
                " ORDER BY table_name, column_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "IDENTITY COLUMN", (string)dr["column_name"], (string)dr["table_name"], Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.table_name, t.column_name FROM tgt_tab_identity_cols t" +
                " LEFT JOIN src_tab_identity_cols s USING(table_name, column_name)" +
                " JOIN common_tab_cols USING(table_name, column_name)" +
                " WHERE s.table_name IS NULL" +
                " ORDER BY table_name, column_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "IDENTITY COLUMN", (string)dr["column_name"], (string)dr["table_name"], Strings.ObjectInTarget);
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
                    var sourceIdentityColumn = new IdentityColumn
                    {
                        TableName = (string)dr["table_name"],
                        ColumnName = (string)dr["column_name"],
                        GenerationType = dr["src_generation_type"] is DBNull ? null : (string)dr["src_generation_type"],
                        SequenceName = (string)dr["src_sequence_name"],
                        IdentityOptions = dr["src_identity_options"] is DBNull ? null : (string)dr["src_identity_options"],
                    };
                    var targetIdentityColumn = new IdentityColumn
                    {
                        TableName = (string)dr["table_name"],
                        ColumnName = (string)dr["column_name"],
                        GenerationType = dr["tgt_generation_type"] is DBNull ? null : (string)dr["tgt_generation_type"],
                        SequenceName = (string)dr["tgt_sequence_name"],
                        IdentityOptions = dr["tgt_identity_options"] is DBNull ? null : (string)dr["tgt_identity_options"],
                    };
                    sourceIdentityColumn.Compare(targetIdentityColumn, this._comparisonSet, list);
                }
            }
        }

    }
}
