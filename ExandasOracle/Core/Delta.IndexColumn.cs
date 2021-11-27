using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Domain;
using ExandasOracle.Dao;

namespace ExandasOracle.Core
{
    public partial class Delta
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="list"></param>
        private void DeltaIndexColumn(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.index_name, s.table_name, s.column_name FROM src_ind_columns s" +
                " LEFT JOIN tgt_ind_columns t USING(index_name, table_name, column_name)" +
                " JOIN common_indexes USING(index_name, table_name)" +
                " WHERE t.index_name IS NULL " +
                " ORDER BY table_name, index_name, column_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var parentObject = string.Format("{0}.{1}", (string)dr["table_name"], (string)dr["index_name"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "INDEX COLUMN", (string)dr["column_name"], parentObject, LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.index_name, t.table_name, t.column_name FROM tgt_ind_columns t" +
                " LEFT JOIN src_ind_columns s USING(index_name, table_name, column_name)" +
                " JOIN common_indexes USING(index_name, table_name)" +
                " WHERE s.index_name IS NULL " +
                " ORDER BY table_name, index_name, column_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var parentObject = string.Format("{0}.{1}", (string)dr["table_name"], (string)dr["index_name"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "INDEX COLUMN", (string)dr["column_name"], parentObject, LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_ind_columns";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceIndexColumn = new IndexColumn
                    {
                        IndexName = (string)dr["index_name"],
                        TableOwner = (string)dr["src_table_owner"],
                        TableName = (string)dr["table_name"],
                        ColumnName = (string)dr["column_name"],
                        ColumnPosition = (int)dr["src_column_position"],
                        ColumnLength = (int)dr["src_column_length"],
                        CharLength = dr["src_col_char_length"] is DBNull ? null : (int?)dr["src_col_char_length"],
                        Descend = dr["src_descend"] is DBNull ? null : (string)dr["src_descend"],
                        CollatedColumnId = dr["src_collated_column_id"] is DBNull ? null : (int?)dr["src_collated_column_id"],
                    };
                    var targetIndexColumn = new IndexColumn
                    {
                        IndexName = (string)dr["index_name"],
                        TableOwner = (string)dr["tgt_table_owner"],
                        TableName = (string)dr["table_name"],
                        ColumnName = (string)dr["column_name"],
                        ColumnPosition = (int)dr["tgt_column_position"],
                        ColumnLength = (int)dr["tgt_column_length"],
                        CharLength = dr["tgt_col_char_length"] is DBNull ? null : (int?)dr["tgt_col_char_length"],
                        Descend = dr["tgt_descend"] is DBNull ? null : (string)dr["tgt_descend"],
                        CollatedColumnId = dr["tgt_collated_column_id"] is DBNull ? null : (int?)dr["tgt_collated_column_id"],
                    };
                    sourceIndexColumn.Compare(targetIndexColumn, this._comparisonSet, list);
                }
            }
        }

    }
}
