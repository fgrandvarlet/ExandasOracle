
CREATE VIEW comp_sequences AS
SELECT
    sequence_name
,   s.min_value     AS src_min_value
,   s.max_value     AS src_max_value
,   s.increment_by  AS src_increment_by
,   s.cycle_flag    AS src_cycle_flag
,   s.order_flag    AS src_order_flag
,   s.cache_size    AS src_cache_size
,   s.scale_flag    AS src_scale_flag
,   s.extend_flag   AS src_extend_flag
,   s.sharded_flag  AS src_sharded_flag
,   s.session_flag  AS src_session_flag
,   s.keep_value    AS src_keep_value
,   t.min_value     AS tgt_min_value
,   t.max_value     AS tgt_max_value
,   t.increment_by  AS tgt_increment_by
,   t.cycle_flag    AS tgt_cycle_flag
,   t.order_flag    AS tgt_order_flag
,   t.cache_size    AS tgt_cache_size
,   t.scale_flag    AS tgt_scale_flag
,   t.extend_flag   AS tgt_extend_flag
,   t.sharded_flag  AS tgt_sharded_flag
,   t.session_flag  AS tgt_session_flag
,   t.keep_value    AS tgt_keep_value
FROM src_sequences s
JOIN tgt_sequences t USING(sequence_name)
ORDER BY sequence_name
;
