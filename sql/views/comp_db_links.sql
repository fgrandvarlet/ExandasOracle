
CREATE VIEW comp_db_links AS
SELECT
    db_link
,   s.username          AS src_username
,   s.host              AS src_host
,   s.shard_internal    AS src_shard_internal
,   s.valid             AS src_valid
,   t.username          AS tgt_username
,   t.host              AS tgt_host
,   t.shard_internal    AS tgt_shard_internal
,   t.valid             AS tgt_valid
FROM src_db_links s
JOIN tgt_db_links t USING (db_link)
ORDER BY db_link
;
