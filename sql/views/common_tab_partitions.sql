
CREATE VIEW common_tab_partitions AS
SELECT table_name, partition_name
FROM src_tab_partitions JOIN tgt_tab_partitions USING(table_name, partition_name)
;
