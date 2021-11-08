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
        private void DeltaConstraintColumn(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.constraint_name, s.table_name, s.column_name FROM src_cons_columns s" +
                " LEFT JOIN tgt_cons_columns t USING(constraint_name, table_name, column_name)" +
                " JOIN common_constraints USING(table_name, constraint_name)" +
                " WHERE t.constraint_name IS NULL " +
                " ORDER BY table_name, constraint_name, column_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var parentObject = string.Format("{0}.{1}", (string)dr["table_name"], (string)dr["constraint_name"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "CONSTRAINT COLUMN", (string)dr["constraint_name"], parentObject, Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.constraint_name, t.table_name, t.column_name FROM tgt_cons_columns t" +
                " LEFT JOIN src_cons_columns s USING(constraint_name, table_name, column_name)" +
                " JOIN common_constraints USING(table_name, constraint_name)" +
                " WHERE s.constraint_name IS NULL " +
                " ORDER BY table_name, constraint_name, column_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var parentObject = string.Format("{0}.{1}", (string)dr["table_name"], (string)dr["constraint_name"]);
                    var report = new DeltaReport(this._comparisonSet.Uid, "CONSTRAINT COLUMN", (string)dr["constraint_name"], parentObject, Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_cons_columns";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceConstraintColumn = new ConstraintColumn
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        ColumnName = (string)dr["column_name"],
                        Position = dr["src_col_position"] is DBNull ? null : (int?)dr["src_col_position"],
                    };
                    var targetConstraintColumn = new ConstraintColumn
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        ColumnName = (string)dr["column_name"],
                        Position = dr["tgt_col_position"] is DBNull ? null : (int?)dr["tgt_col_position"],
                    };
                    sourceConstraintColumn.Compare(targetConstraintColumn, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
