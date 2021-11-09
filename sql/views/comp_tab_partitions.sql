
CREATE VIEW comp_tab_partitions AS
SELECT
    table_name
,   partition_name
,   s.composite                 AS src_composite
,   s.subpartition_count        AS src_subpartition_count
,   s.high_value                AS src_high_value
,   s.high_value_length         AS src_high_value_length
,   s.partition_position        AS src_partition_position
,   s.tablespace_name           AS src_tablespace_name
,   s.logging                   AS src_logging
,   s.compression               AS src_compression
,   s.compress_for              AS src_compress_for
,   s.is_nested                 AS src_is_nested
,   s.parent_table_partition    AS src_parent_table_partition
,   s.interval                  AS src_interval
,   s.indexing                  AS src_indexing
,   s.read_only                 AS src_read_only
,   t.composite                 AS tgt_composite
,   t.subpartition_count        AS tgt_subpartition_count
,   t.high_value                AS tgt_high_value
,   t.high_value_length         AS tgt_high_value_length
,   t.partition_position        AS tgt_partition_position
,   t.tablespace_name           AS tgt_tablespace_name
,   t.logging                   AS tgt_logging
,   t.compression               AS tgt_compression
,   t.compress_for              AS tgt_compress_for
,   t.is_nested                 AS tgt_is_nested
,   t.parent_table_partition    AS tgt_parent_table_partition
,   t.interval                  AS tgt_interval
,   t.indexing                  AS tgt_indexing
,   t.read_only                 AS tgt_read_only
FROM src_tab_partitions s
JOIN tgt_tab_partitions t USING (table_name, partition_name)
ORDER BY table_name, partition_name
;
