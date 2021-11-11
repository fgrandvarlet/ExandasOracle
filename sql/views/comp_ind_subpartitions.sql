
CREATE VIEW comp_ind_subpartitions AS
SELECT
    index_name
,   partition_name
,   subpartition_name
,   s.high_value            AS src_high_value
,   s.high_value_length     AS src_high_value_length
,   s.partition_position    AS src_partition_position
,   s.subpartition_position AS src_subpartition_position
,   s.status                AS src_status
,   s.tablespace_name       AS src_tablespace_name
,   s.logging               AS src_logging
,   s.compression           AS src_compression
,   s.parameters            AS src_parameters
,   s.interval              AS src_interval
,   t.high_value            AS tgt_high_value
,   t.high_value_length     AS tgt_high_value_length
,   t.partition_position    AS tgt_partition_position
,   t.subpartition_position AS tgt_subpartition_position
,   t.status                AS tgt_status
,   t.tablespace_name       AS tgt_tablespace_name
,   t.logging               AS tgt_logging
,   t.compression           AS tgt_compression
,   t.parameters            AS tgt_parameters
,   t.interval              AS tgt_interval
FROM src_ind_subpartitions s
JOIN tgt_ind_subpartitions t USING (index_name, partition_name, subpartition_name)
ORDER BY index_name, partition_name, subpartition_name
;
