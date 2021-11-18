
CREATE VIEW comp_synonyms AS
SELECT
    owner
,   synonym_name
,   s.table_owner   AS src_table_owner
,   s.table_name    AS src_table_name
,   s.db_link       AS src_db_link
,   t.table_owner   AS tgt_table_owner
,   t.table_name    AS tgt_table_name
,   t.db_link       AS tgt_db_link
FROM src_synonyms s
JOIN tgt_synonyms t USING (owner, synonym_name)
ORDER BY owner, synonym_name
;
