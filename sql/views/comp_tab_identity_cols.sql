
CREATE VIEW comp_tab_identity_cols AS
SELECT
    table_name
,   column_name    
,   s.generation_type   AS src_generation_type
,   s.sequence_name     AS src_sequence_name
,   s.identity_options  AS src_identity_options
,   t.generation_type   AS tgt_generation_type
,   t.sequence_name     AS tgt_sequence_name
,   t.identity_options  AS tgt_identity_options
FROM src_tab_identity_cols s
JOIN tgt_tab_identity_cols t USING (table_name, column_name)
ORDER BY table_name, column_name
;
