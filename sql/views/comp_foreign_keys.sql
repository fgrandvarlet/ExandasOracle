
CREATE VIEW comp_foreign_keys AS
SELECT
    constraint_name
,   table_name
,   s.r_owner           AS src_r_owner
,   s.r_constraint_name AS src_r_constraint_name
,   s.delete_rule       AS src_delete_rule
,   s.status            AS src_status
,   s.deferrable        AS src_deferrable
,   s.deferred          AS src_deferred
,   s.validated         AS src_validated
,   s.invalid           AS src_invalid
,   s.view_related      AS src_view_related
,   t.r_owner           AS tgt_r_owner
,   t.r_constraint_name AS tgt_r_constraint_name
,   t.delete_rule       AS tgt_delete_rule
,   t.status            AS tgt_status
,   t.deferrable        AS tgt_deferrable
,   t.deferred          AS tgt_deferred
,   t.validated         AS tgt_validated
,   t.invalid           AS tgt_invalid
,   t.view_related      AS tgt_view_related
FROM src_foreign_keys s
JOIN tgt_foreign_keys t USING(constraint_name, table_name)
ORDER BY table_name, constraint_name
;
