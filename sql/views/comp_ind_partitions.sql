
CREATE VIEW comp_ind_partitions AS
SELECT
    index_name
,   partition_name
,   s.composite             AS src_composite
,   s.subpartition_count    AS src_subpartition_count
,   s.high_value            AS src_high_value
,   s.high_value_length     AS src_high_value_length
,   s.partition_position    AS src_partition_position
,   s.status                AS src_status
,   s.tablespace_name       AS src_tablespace_name
,   s.logging               AS src_logging
,   s.compression           AS src_compression
,   t.composite             AS tgt_composite
,   t.subpartition_count    AS tgt_subpartition_count
,   t.high_value            AS tgt_high_value
,   t.high_value_length     AS tgt_high_value_length
,   t.partition_position    AS tgt_partition_position
,   t.status                AS tgt_status
,   t.tablespace_name       AS tgt_tablespace_name
,   t.logging               AS tgt_logging
,   t.compression           AS tgt_compression
FROM src_ind_partitions s
JOIN tgt_ind_partitions t USING(index_name, partition_name)
ORDER BY index_name, partition_name
;
