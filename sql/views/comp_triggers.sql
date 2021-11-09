
CREATE VIEW comp_triggers AS
SELECT
    trigger_name
,   s.trigger_type      AS src_trigger_type
,   s.triggering_event  AS src_triggering_event
,   s.table_owner       AS src_table_owner
,   s.base_object_type  AS src_base_object_type
,   s.table_name        AS src_table_name
,   s.column_name       AS src_column_name
,   s.referencing_names AS src_referencing_names
,   s.when_clause       AS src_when_clause
,   s.status            AS src_status
,   s.description       AS src_description
,   s.action_type       AS src_action_type
,   s.trigger_body      AS src_trigger_body
,   s.before_statement  AS src_before_statement
,   s.before_row        AS src_before_row
,   s.after_row         AS src_after_row
,   s.after_statement   AS src_after_statement
,   s.instead_of_row    AS src_instead_of_row
,   s.fire_once         AS src_fire_once
,   s.apply_server_only AS src_apply_server_only
,   t.trigger_type      AS tgt_trigger_type
,   t.triggering_event  AS tgt_triggering_event
,   t.table_owner       AS tgt_table_owner
,   t.base_object_type  AS tgt_base_object_type
,   t.table_name        AS tgt_table_name
,   t.column_name       AS tgt_column_name
,   t.referencing_names AS tgt_referencing_names
,   t.when_clause       AS tgt_when_clause
,   t.status            AS tgt_status
,   t.description       AS tgt_description
,   t.action_type       AS tgt_action_type
,   t.trigger_body      AS tgt_trigger_body
,   t.before_statement  AS tgt_before_statement
,   t.before_row        AS tgt_before_row
,   t.after_row         AS tgt_after_row
,   t.after_statement   AS tgt_after_statement
,   t.instead_of_row    AS tgt_instead_of_row
,   t.fire_once         AS tgt_fire_once
,   t.apply_server_only AS tgt_apply_server_only
FROM src_triggers s
JOIN tgt_triggers t USING (trigger_name)
ORDER BY trigger_name
;
