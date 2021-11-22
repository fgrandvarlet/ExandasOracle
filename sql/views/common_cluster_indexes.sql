
CREATE VIEW common_cluster_indexes AS
SELECT index_name, table_name
FROM src_cluster_indexes JOIN tgt_cluster_indexes USING(index_name, table_name)
;
