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
        private void DeltaTrigger(FbConnection conn, List<DeltaReport> list)
        {
            string sql;
            FbCommand cmd;

            // phase 1 : source minus target
            sql = "SELECT s.trigger_name, s.table_name FROM src_triggers s" +
                " LEFT JOIN tgt_triggers t USING(trigger_name)" +
                " WHERE t.trigger_name IS NULL" +
                " AND (EXISTS (SELECT 1 FROM common_tables WHERE table_name = s.table_name) OR s.table_name IS NULL)" +
                " ORDER BY trigger_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "TRIGGER", (string)dr["trigger_name"], (string)dr["table_name"], Strings.ObjectInSource);
                    list.Add(report);
                }
            }

            // phase 2 : target minus source
            sql = "SELECT t.trigger_name, t.table_name FROM tgt_triggers t" +
                " LEFT JOIN src_triggers s USING(trigger_name)" +
                " WHERE s.trigger_name IS NULL" +
                " AND (EXISTS (SELECT 1 FROM common_tables WHERE table_name = t.table_name) OR t.table_name IS NULL)" +
                " ORDER BY trigger_name";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var report = new DeltaReport(this._comparisonSet.Uid, "TRIGGER", (string)dr["trigger_name"], (string)dr["table_name"], Strings.ObjectInTarget);
                    list.Add(report);
                }
            }

            // phase 3 : property differences between source and target
            sql = "SELECT * FROM comp_triggers";
            cmd = new FbCommand(sql, conn);

            using (FbDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    var sourceTrigger = new Trigger
                    {
                        TriggerName = (string)dr["trigger_name"],
                        TriggerType = dr["src_trigger_type"] is DBNull ? null : (string)dr["src_trigger_type"],
                        TriggeringEvent = dr["src_triggering_event"] is DBNull ? null : (string)dr["src_triggering_event"],
                        TableOwner = dr["src_table_owner"] is DBNull ? null : (string)dr["src_table_owner"],
                        BaseObjectType = dr["src_base_object_type"] is DBNull ? null : (string)dr["src_base_object_type"],
                        TableName = dr["src_table_name"] is DBNull ? null : (string)dr["src_table_name"],
                        ColumnName = dr["src_column_name"] is DBNull ? null : (string)dr["src_column_name"],
                        ReferencingNames = dr["src_referencing_names"] is DBNull ? null : (string)dr["src_referencing_names"],
                        WhenClause = dr["src_when_clause"] is DBNull ? null : (string)dr["src_when_clause"],
                        Status = dr["src_status"] is DBNull ? null : (string)dr["src_status"],
                        Description = dr["src_description"] is DBNull ? null : (string)dr["src_description"],
                        ActionType = dr["src_action_type"] is DBNull ? null : (string)dr["src_action_type"],
                        TriggerBody = dr["src_trigger_body"] is DBNull ? null : (string)dr["src_trigger_body"],
                        BeforeStatement = dr["src_before_statement"] is DBNull ? null : (string)dr["src_before_statement"],
                        BeforeRow = dr["src_before_row"] is DBNull ? null : (string)dr["src_before_row"],
                        AfterRow = dr["src_after_row"] is DBNull ? null : (string)dr["src_after_row"],
                        AfterStatement = dr["src_after_statement"] is DBNull ? null : (string)dr["src_after_statement"],
                        InsteadOfRow = dr["src_instead_of_row"] is DBNull ? null : (string)dr["src_instead_of_row"],
                        FireOnce = dr["src_fire_once"] is DBNull ? null : (string)dr["src_fire_once"],
                        ApplyServerOnly = dr["src_apply_server_only"] is DBNull ? null : (string)dr["src_apply_server_only"],
                    };
                    var targetTrigger = new Trigger
                    {
                        TriggerName = (string)dr["trigger_name"],
                        TriggerType = dr["tgt_trigger_type"] is DBNull ? null : (string)dr["tgt_trigger_type"],
                        TriggeringEvent = dr["tgt_triggering_event"] is DBNull ? null : (string)dr["tgt_triggering_event"],
                        TableOwner = dr["tgt_table_owner"] is DBNull ? null : (string)dr["tgt_table_owner"],
                        BaseObjectType = dr["tgt_base_object_type"] is DBNull ? null : (string)dr["tgt_base_object_type"],
                        TableName = dr["tgt_table_name"] is DBNull ? null : (string)dr["tgt_table_name"],
                        ColumnName = dr["tgt_column_name"] is DBNull ? null : (string)dr["tgt_column_name"],
                        ReferencingNames = dr["tgt_referencing_names"] is DBNull ? null : (string)dr["tgt_referencing_names"],
                        WhenClause = dr["tgt_when_clause"] is DBNull ? null : (string)dr["tgt_when_clause"],
                        Status = dr["tgt_status"] is DBNull ? null : (string)dr["tgt_status"],
                        Description = dr["tgt_description"] is DBNull ? null : (string)dr["tgt_description"],
                        ActionType = dr["tgt_action_type"] is DBNull ? null : (string)dr["tgt_action_type"],
                        TriggerBody = dr["tgt_trigger_body"] is DBNull ? null : (string)dr["tgt_trigger_body"],
                        BeforeStatement = dr["tgt_before_statement"] is DBNull ? null : (string)dr["tgt_before_statement"],
                        BeforeRow = dr["tgt_before_row"] is DBNull ? null : (string)dr["tgt_before_row"],
                        AfterRow = dr["tgt_after_row"] is DBNull ? null : (string)dr["tgt_after_row"],
                        AfterStatement = dr["tgt_after_statement"] is DBNull ? null : (string)dr["tgt_after_statement"],
                        InsteadOfRow = dr["tgt_instead_of_row"] is DBNull ? null : (string)dr["tgt_instead_of_row"],
                        FireOnce = dr["tgt_fire_once"] is DBNull ? null : (string)dr["tgt_fire_once"],
                        ApplyServerOnly = dr["tgt_apply_server_only"] is DBNull ? null : (string)dr["tgt_apply_server_only"],
                    };
                    sourceTrigger.Compare(targetTrigger, this._comparisonSet, list);
                }
            }
        }

    }
}
