
CREATE VIEW common_ind_partitions AS
SELECT index_name, partition_name
FROM src_ind_partitions JOIN tgt_ind_partitions USING(index_name, partition_name)
;
