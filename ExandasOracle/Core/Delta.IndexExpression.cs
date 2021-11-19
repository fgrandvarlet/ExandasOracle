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
        private void DeltaIndexExpression(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.index_name, s.column_position FROM src_ind_expressions s" +
                " LEFT JOIN tgt_ind_expressions t USING(index_name, column_position)" +
                " JOIN common_indexes ci ON s.table_name = ci.table_name AND s.index_name = ci.index_name" +
                " WHERE t.index_name IS NULL" +
                " ORDER BY index_name, column_position";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var objectValue = string.Format("{0}#{1}", (string)dr["index_name"], (decimal)dr["column_position"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "INDEX EXPRESSION", objectValue, (string)dr["table_name"], Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.index_name, t.column_position FROM tgt_ind_expressions t" +
                " LEFT JOIN src_ind_expressions s USING(index_name, column_position)" +
                " JOIN common_indexes ci ON t.table_name = ci.table_name AND t.index_name = ci.index_name" +
                " WHERE s.index_name IS NULL" +
                " ORDER BY index_name, column_position";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var objectValue = string.Format("{0}#{1}", (string)dr["index_name"], (decimal)dr["column_position"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "INDEX EXPRESSION", objectValue, (string)dr["table_name"], Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_ind_expressions";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceIndexExpression = new IndexExpression
                    {
                        IndexName = (string)dr["index_name"],
                        TableOwner = (string)dr["src_table_owner"],
                        TableName = (string)dr["src_table_name"],
                        ColumnExpression = dr["src_column_expression"] is DBNull ? null : (string)dr["src_column_expression"],
                        ColumnPosition = (decimal)dr["column_position"],
                    };
                    var targetIndexExpression = new IndexExpression
                    {
                        IndexName = (string)dr["index_name"],
                        TableOwner = (string)dr["tgt_table_owner"],
                        TableName = (string)dr["tgt_table_name"],
                        ColumnExpression = dr["tgt_column_expression"] is DBNull ? null : (string)dr["tgt_column_expression"],
                        ColumnPosition = (decimal)dr["column_position"],
                    };
                    sourceIndexExpression.Compare(targetIndexExpression, this._comparisonSet, list);
                }
            }
        }

    }
}
