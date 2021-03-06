using System;
using System.Collections.Generic;
using FirebirdSql.Data.FirebirdClient;

using ExandasOracle.Dao;
using ExandasOracle.Domain;

namespace ExandasOracle.Core
{
    public partial class Delta
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="conn"></param>
        /// <param name="list"></param>
        private void DeltaCheck(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.table_name, s.constraint_name FROM src_checks s" +
                " LEFT JOIN tgt_checks t USING(table_name, constraint_name)" +
                " JOIN common_tables USING(table_name)" +
                " WHERE t.table_name IS NULL " +
                " ORDER BY table_name, constraint_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "CHECK", (string)dr["constraint_name"], (string)dr["table_name"], LabelId.ObjectInSourceNotInTarget);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.table_name, t.constraint_name FROM tgt_checks t" +
                " LEFT JOIN src_checks s USING(table_name, constraint_name)" +
                " JOIN common_tables USING(table_name)" +
                " WHERE s.table_name IS NULL " +
                " ORDER BY table_name, constraint_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "CHECK", (string)dr["constraint_name"], (string)dr["table_name"], LabelId.ObjectInTargetNotInSource);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_checks";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceCheck = new Check
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        SearchCondition = dr["src_search_condition"] is DBNull ? null : (string)dr["src_search_condition"],
                        SearchConditionVC = dr["src_search_condition_vc"] is DBNull ? null : (string)dr["src_search_condition_vc"],
                        Status = dr["src_status"] is DBNull ? null : (string)dr["src_status"],
                        Deferrable = dr["src_deferrable"] is DBNull ? null : (string)dr["src_deferrable"],
                        Deferred = dr["src_deferred"] is DBNull ? null : (string)dr["src_deferred"],
                        Validated = dr["src_validated"] is DBNull ? null : (string)dr["src_validated"],
                        Invalid = dr["src_invalid"] is DBNull ? null : (string)dr["src_invalid"],
                        ViewRelated = dr["src_view_related"] is DBNull ? null : (string)dr["src_view_related"],
                    };
                    var targetCheck = new Check
                    {
                        ConstraintName = (string)dr["constraint_name"],
                        TableName = (string)dr["table_name"],
                        SearchCondition = dr["tgt_search_condition"] is DBNull ? null : (string)dr["tgt_search_condition"],
                        SearchConditionVC = dr["tgt_search_condition_vc"] is DBNull ? null : (string)dr["tgt_search_condition_vc"],
                        Status = dr["tgt_status"] is DBNull ? null : (string)dr["tgt_status"],
                        Deferrable = dr["tgt_deferrable"] is DBNull ? null : (string)dr["tgt_deferrable"],
                        Deferred = dr["tgt_deferred"] is DBNull ? null : (string)dr["tgt_deferred"],
                        Validated = dr["tgt_validated"] is DBNull ? null : (string)dr["tgt_validated"],
                        Invalid = dr["tgt_invalid"] is DBNull ? null : (string)dr["tgt_invalid"],
                        ViewRelated = dr["tgt_view_related"] is DBNull ? null : (string)dr["tgt_view_related"],
                    };
                    sourceCheck.Compare(targetCheck, this._comparisonSet.Uid, list);
                }
            }
        }

    }
}
