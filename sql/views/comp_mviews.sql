
CREATE VIEW comp_mviews AS
SELECT
    mview_name
,   s.container_name        AS src_container_name
,   s.query                 AS src_query
,   s.query_len             AS src_query_len
,   s.updatable             AS src_updatable
,   s.update_log            AS src_update_log
,   s.master_rollback_seg   AS src_master_rollback_seg
,   s.master_link           AS src_master_link
,   s.rewrite_enabled       AS src_rewrite_enabled
,   s.rewrite_capability    AS src_rewrite_capability
,   s.refresh_mode          AS src_refresh_mode
,   s.refresh_method        AS src_refresh_method
,   s.build_mode            AS src_build_mode
,   s.fast_refreshable      AS src_fast_refreshable
,   s.use_no_index          AS src_use_no_index
,   s.default_collation     AS src_default_collation
,   t.container_name        AS tgt_container_name
,   t.query                 AS tgt_query
,   t.query_len             AS tgt_query_len
,   t.updatable             AS tgt_updatable
,   t.update_log            AS tgt_update_log
,   t.master_rollback_seg   AS tgt_master_rollback_seg
,   t.master_link           AS tgt_master_link
,   t.rewrite_enabled       AS tgt_rewrite_enabled
,   t.rewrite_capability    AS tgt_rewrite_capability
,   t.refresh_mode          AS tgt_refresh_mode
,   t.refresh_method        AS tgt_refresh_method
,   t.build_mode            AS tgt_build_mode
,   t.fast_refreshable      AS tgt_fast_refreshable
,   t.use_no_index          AS tgt_use_no_index
,   t.default_collation     AS tgt_default_collation
FROM src_mviews s
JOIN tgt_mviews t USING (mview_name)
ORDER BY mview_name
;
