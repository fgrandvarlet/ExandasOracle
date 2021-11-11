
CREATE VIEW comp_tab_subpartitions AS
SELECT
    table_name
,   partition_name
,   subpartition_name
,   s.high_value                AS src_high_value
,   s.high_value_length         AS src_high_value_length
,   s.partition_position        AS src_partition_position
,   s.subpartition_position     AS src_subpartition_position
,   s.tablespace_name           AS src_tablespace_name
,   s.logging                   AS src_logging
,   s.compression               AS src_compression
,   s.compress_for              AS src_compress_for
,   s.interval                  AS src_interval
,   s.indexing                  AS src_indexing
,   s.read_only                 AS src_read_only
,   t.high_value                AS tgt_high_value
,   t.high_value_length         AS tgt_high_value_length
,   t.partition_position        AS tgt_partition_position
,   t.subpartition_position     AS tgt_subpartition_position
,   t.tablespace_name           AS tgt_tablespace_name
,   t.logging                   AS tgt_logging
,   t.compression               AS tgt_compression
,   t.compress_for              AS tgt_compress_for
,   t.interval                  AS tgt_interval
,   t.indexing                  AS tgt_indexing
,   t.read_only                 AS tgt_read_only
FROM src_tab_subpartitions s
JOIN tgt_tab_subpartitions t USING (table_name, partition_name, subpartition_name)
ORDER BY table_name, partition_name, subpartition_name
;
