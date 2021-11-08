
CREATE VIEW comp_part_tables AS
SELECT
    table_name
,   s.partitioning_type         AS src_partitioning_type
,   s.subpartitioning_type      AS src_subpartitioning_type
,   s.partition_count           AS src_partition_count
,   s.def_subpartition_count    AS src_def_subpartition_count
,   s.partitioning_key_count    AS src_partitioning_key_count
,   s.subpartitioning_key_count AS src_subpartitioning_key_count
,   s.status                    AS src_status
,   s.def_tablespace_name       AS src_def_tablespace_name
,   s.def_logging               AS src_def_logging
,   s.def_compression           AS src_def_compression
,   s.def_compress_for          AS src_def_compress_for
,   s.ref_ptn_constraint_name   AS src_ref_ptn_constraint_name
,   s.interval                  AS src_interval
,   s.autolist                  AS src_autolist
,   s.interval_subpartition     AS src_interval_subpartition
,   s.autolist_subpartition     AS src_autolist_subpartition
,   s.is_nested                 AS src_is_nested
,   s.def_indexing              AS src_def_indexing
,   s.def_read_only             AS src_def_read_only
,   t.partitioning_type         AS tgt_partitioning_type
,   t.subpartitioning_type      AS tgt_subpartitioning_type
,   t.partition_count           AS tgt_partition_count
,   t.def_subpartition_count    AS tgt_def_subpartition_count
,   t.partitioning_key_count    AS tgt_partitioning_key_count
,   t.subpartitioning_key_count AS tgt_subpartitioning_key_count
,   t.status                    AS tgt_status
,   t.def_tablespace_name       AS tgt_def_tablespace_name
,   t.def_logging               AS tgt_def_logging
,   t.def_compression           AS tgt_def_compression
,   t.def_compress_for          AS tgt_def_compress_for
,   t.ref_ptn_constraint_name   AS tgt_ref_ptn_constraint_name
,   t.interval                  AS tgt_interval
,   t.autolist                  AS tgt_autolist
,   t.interval_subpartition     AS tgt_interval_subpartition
,   t.autolist_subpartition     AS tgt_autolist_subpartition
,   t.is_nested                 AS tgt_is_nested
,   t.def_indexing              AS tgt_def_indexing
,   t.def_read_only             AS tgt_def_read_only
FROM src_part_tables s
JOIN tgt_part_tables t USING (table_name)
ORDER BY table_name
;
