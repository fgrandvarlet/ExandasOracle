
CREATE VIEW comp_table_indexes AS
SELECT
    index_name
,   table_name
,   s.index_type        AS src_index_type
,   s.uniqueness        AS src_uniqueness
,   s.compression       AS src_compression
,   s.prefix_length     AS src_prefix_length
,   s.tablespace_name   AS src_tablespace_name
,   s.include_column    AS src_include_column
,   s.logging           AS src_logging
,   s.status            AS src_status
,   s.degree            AS src_degree
,   s.partitioned       AS src_partitioned
,   s.temporary         AS src_temporary
,   s.duration          AS src_duration
,   t.index_type        AS tgt_index_type
,   t.uniqueness        AS tgt_uniqueness
,   t.compression       AS tgt_compression
,   t.prefix_length     AS tgt_prefix_length
,   t.tablespace_name   AS tgt_tablespace_name
,   t.include_column    AS tgt_include_column
,   t.logging           AS tgt_logging
,   t.status            AS tgt_status
,   t.degree            AS tgt_degree
,   t.partitioned       AS tgt_partitioned
,   t.temporary         AS tgt_temporary
,   t.duration          AS tgt_duration
FROM src_table_indexes s
JOIN tgt_table_indexes t USING (table_name, index_name)
ORDER BY table_name, index_name
;
