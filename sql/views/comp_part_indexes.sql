
CREATE VIEW comp_part_indexes AS
SELECT
    index_name
,   table_name
,   s.partitioning_type         AS src_partitioning_type
,   s.subpartitioning_type      AS src_subpartitioning_type
,   s.partition_count           AS src_partition_count
,   s.def_subpartition_count    AS src_def_subpartition_count
,   s.partitioning_key_count    AS src_partitioning_key_count
,   s.subpartitioning_key_count AS src_subpartitioning_key_count
,   s.locality                  AS src_locality
,   s.alignment                 AS src_alignment
,   s.def_tablespace_name       AS src_def_tablespace_name
,   s.def_logging               AS src_logging
,   s.def_parameters            AS src_def_parameters
,   s.interval                  AS src_interval
,   s.autolist                  AS src_autolist
,   s.interval_subpartition     AS src_interval_subpartition
,   s.autolist_subpartition     AS src_autolist_subpartition
,   t.partitioning_type         AS tgt_partitioning_type
,   t.subpartitioning_type      AS tgt_subpartitioning_type
,   t.partition_count           AS tgt_partition_count
,   t.def_subpartition_count    AS tgt_def_subpartition_count
,   t.partitioning_key_count    AS tgt_partitioning_key_count
,   t.subpartitioning_key_count AS tgt_subpartitioning_key_count
,   t.locality                  AS tgt_locality
,   t.alignment                 AS tgt_alignment
,   t.def_tablespace_name       AS tgt_def_tablespace_name
,   t.def_logging               AS tgt_logging
,   t.def_parameters            AS tgt_def_parameters
,   t.interval                  AS tgt_interval
,   t.autolist                  AS tgt_autolist
,   t.interval_subpartition     AS tgt_interval_subpartition
,   t.autolist_subpartition     AS tgt_autolist_subpartition
FROM src_part_indexes s
JOIN tgt_part_indexes t USING (table_name, index_name)
ORDER BY table_name, index_name
;
