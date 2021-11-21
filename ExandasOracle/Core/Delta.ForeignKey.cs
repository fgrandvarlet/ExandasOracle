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
        private void DeltaForeignKey(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name, s.constraint_name FROM src_foreign_keys s" +
                " LEFT JOIN tgt_foreign_keys t USING(table_name, constraint_name)" +
                " JOIN common_tables USING(table_name)" +
                " WHERE t.table_name IS NULL " +
                " ORDER BY table_name, constraint_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "FOREIGN KEY", (string)dr["constraint_name"], (string)dr["table_name"], Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.table_name, t.constraint_name FROM tgt_foreign_keys t" +
                " LEFT JOIN src_foreign_keys s USING(table_name, constraint_name)" +
                " JOIN common_tables USING(table_name)" +
                " WHERE s.table_name IS NULL " +
                " ORDER BY table_name, constraint_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "FOREIGN KEY", (string)dr["constraint_name"], (string)dr["table_name"], Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_foreign_keys";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceForeignKey = new ForeignKey
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        ROwner = dr["src_r_owner"] is DBNull ? null : (string)dr["src_r_owner"],
                        RConstraintName = dr["src_r_constraint_name"] is DBNull ? null : (string)dr["src_r_constraint_name"],
                        DeleteRule = dr["src_delete_rule"] is DBNull ? null : (string)dr["src_delete_rule"],
                        Status = dr["src_status"] is DBNull ? null : (string)dr["src_status"],
                        Deferrable = dr["src_deferrable"] is DBNull ? null : (string)dr["src_deferrable"],
                        Deferred = dr["src_deferred"] is DBNull ? null : (string)dr["src_deferred"],
                        Validated = dr["src_validated"] is DBNull ? null : (string)dr["src_validated"],
                        Invalid = dr["src_invalid"] is DBNull ? null : (string)dr["src_invalid"],
                        ViewRelated = dr["src_view_related"] is DBNull ? null : (string)dr["src_view_related"],
                    };
                    var targetForeignKey = new ForeignKey
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        ROwner = dr["tgt_r_owner"] is DBNull ? null : (string)dr["tgt_r_owner"],
                        RConstraintName = dr["tgt_r_constraint_name"] is DBNull ? null : (string)dr["tgt_r_constraint_name"],
                        DeleteRule = dr["tgt_delete_rule"] is DBNull ? null : (string)dr["tgt_delete_rule"],
                        Status = dr["tgt_status"] is DBNull ? null : (string)dr["tgt_status"],
                        Deferrable = dr["tgt_deferrable"] is DBNull ? null : (string)dr["tgt_deferrable"],
                        Deferred = dr["tgt_deferred"] is DBNull ? null : (string)dr["tgt_deferred"],
                        Validated = dr["tgt_validated"] is DBNull ? null : (string)dr["tgt_validated"],
                        Invalid = dr["tgt_invalid"] is DBNull ? null : (string)dr["tgt_invalid"],
                        ViewRelated = dr["tgt_view_related"] is DBNull ? null : (string)dr["tgt_view_related"],
                    };
                    sourceForeignKey.Compare(targetForeignKey, this._comparisonSet, list);
                }
            }
        }

    }
}
