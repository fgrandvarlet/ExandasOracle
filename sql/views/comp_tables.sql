
CREATE VIEW comp_tables AS
SELECT
    table_name
,   s.tablespace_name   AS src_tablespace_name
,   s.cluster_name      AS src_cluster_name
,   s.iot_name          AS src_iot_name
,   s.status            AS src_status
,   s.logging           AS src_logging
,   s.degree            AS src_degree
,   s.partitioned       AS src_partitioned
,   s.iot_type          AS src_iot_type
,   s.temporary         AS src_temporary
,   s.nested            AS src_nested
,   s.duration          AS src_duration
,   s.cluster_owner     AS src_cluster_owner
,   s.compression       AS src_compression
,   s.compress_for      AS src_compress_for
,   s.dropped           AS src_dropped
,   s.read_only         AS src_read_only
,   s.clustering        AS src_clustering
,   s.has_identity      AS src_has_identity
,   s.container_data    AS src_container_data
,   s.default_collation AS src_default_collation
,   s.external          AS src_external
,   t.tablespace_name   AS tgt_tablespace_name
,   t.cluster_name      AS tgt_cluster_name
,   t.iot_name          AS tgt_iot_name
,   t.status            AS tgt_status
,   t.logging           AS tgt_logging
,   t.degree            AS tgt_degree
,   t.partitioned       AS tgt_partitioned
,   t.iot_type          AS tgt_iot_type
,   t.temporary         AS tgt_temporary
,   t.nested            AS tgt_nested
,   t.duration          AS tgt_duration
,   t.cluster_owner     AS tgt_cluster_owner
,   t.compression       AS tgt_compression
,   t.compress_for      AS tgt_compress_for
,   t.dropped           AS tgt_dropped
,   t.read_only         AS tgt_read_only
,   t.clustering        AS tgt_clustering
,   t.has_identity      AS tgt_has_identity
,   t.container_data    AS tgt_container_data
,   t.default_collation AS tgt_default_collation
,   t.external          AS tgt_external
FROM src_tables s
JOIN tgt_tables t USING (table_name)
ORDER BY table_name
;
