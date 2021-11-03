
CREATE VIEW comp_constraints AS
SELECT
    constraint_name
,   table_name
,   s.constraint_type       AS src_constraint_type
,   s.search_condition      AS src_search_condition
,   s.search_condition_vc   AS src_search_condition_vc
,   s.status                AS src_status
,   s.deferrable            AS src_deferrable
,   s.deferred              AS src_deferred
,   s.validated             AS src_validated
,   s.invalid               AS src_invalid
,   s.view_related          AS src_view_related
,   t.constraint_type       AS tgt_constraint_type
,   t.search_condition      AS tgt_search_condition
,   t.search_condition_vc   AS tgt_search_condition_vc
,   t.status                AS tgt_status
,   t.deferrable            AS tgt_deferrable
,   t.deferred              AS tgt_deferred
,   t.validated             AS tgt_validated
,   t.invalid               AS tgt_invalid
,   t.view_related          AS tgt_view_related
FROM src_constraints s
JOIN tgt_constraints t USING(constraint_name, table_name)
JOIN common_tables USING(table_name)
ORDER BY table_name, constraint_name
;
