
CREATE VIEW comp_clu_columns AS
SELECT
    cluster_name
,   clu_column_name
,   s.table_name        AS src_table_name
,   s.tab_column_name   AS src_tab_column_name
,   t.table_name        AS tgt_table_name
,   t.tab_column_name   AS tgt_tab_column_name
FROM src_clu_columns s
JOIN tgt_clu_columns t USING (cluster_name, clu_column_name)
JOIN common_clusters USING (cluster_name)
ORDER BY cluster_name, clu_column_name
;
