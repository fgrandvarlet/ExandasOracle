
CREATE VIEW common_indexes AS
SELECT index_name, table_name FROM common_table_indexes
UNION ALL
SELECT index_name, table_name FROM common_cluster_indexes
;
