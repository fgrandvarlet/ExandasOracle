
CREATE VIEW comp_clu_columns AS
SELECT
    cluster_name
,   clu_column_name
,   table_name
,   s.tab_column_name   AS src_tab_column_name
,   t.tab_column_name   AS tgt_tab_column_name
FROM src_clu_columns s
JOIN tgt_clu_columns t USING (cluster_name, clu_column_name, table_name)
ORDER BY cluster_name, clu_column_name, table_name
;
