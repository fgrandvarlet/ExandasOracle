
CREATE VIEW comp_clusters AS
SELECT
    cluster_name
,   s.tablespace_name   AS src_tablespace_name
,   s.cluster_type      AS src_cluster_type
,   s.clu_function      AS src_clu_function
,   s.hashkeys          AS src_hashkeys
,   s.degree            AS src_degree
,   s.cache             AS src_cache
,   s.single_table      AS src_single_table
,   s.dependencies      AS src_dependencies
,   t.tablespace_name   AS tgt_tablespace_name
,   t.cluster_type      AS tgt_cluster_type
,   t.clu_function      AS tgt_clu_function
,   t.hashkeys          AS tgt_hashkeys
,   t.degree            AS tgt_degree
,   t.cache             AS tgt_cache
,   t.single_table      AS tgt_single_table
,   t.dependencies      AS tgt_dependencies
FROM src_clusters s
JOIN tgt_clusters t USING (cluster_name)
ORDER BY cluster_name
;
