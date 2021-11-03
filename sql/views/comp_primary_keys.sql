
CREATE VIEW comp_primary_keys AS
SELECT
    constraint_name
,   table_name
,   s.status        AS src_status
,   s.deferrable    AS src_deferrable
,   s.deferred      AS src_deferred
,   s.validated     AS src_validated
,   s.rely          AS src_rely
,   s.index_owner   AS src_index_owner
,   s.index_name    AS src_index_name
,   s.invalid       AS src_invalid
,   s.view_related  AS src_view_related
,   t.status        AS tgt_status
,   t.deferrable    AS tgt_deferrable
,   t.deferred      AS tgt_deferred
,   t.validated     AS tgt_validated
,   t.rely          AS tgt_rely
,   t.index_owner   AS tgt_index_owner
,   t.index_name    AS tgt_index_name
,   t.invalid       AS tgt_invalid
,   t.view_related  AS tgt_view_related
FROM src_primary_keys s
JOIN tgt_primary_keys t USING(constraint_name, table_name)
JOIN common_tables USING(table_name)
ORDER BY table_name, constraint_name
;
