
CREATE VIEW comp_views AS
SELECT
    view_name
,   s.text_length       AS src_text_length
,   s.text              AS src_text
,   s.text_vc           AS src_text_vc
,   s.type_text         AS src_type_text
,   s.oid_text          AS src_oid_text
,   s.view_type_owner   AS src_view_type_owner
,   s.view_type         AS src_view_type
,   s.superview_name    AS src_superview_name
,   s.read_only         AS src_read_only
,   s.bequeath          AS src_bequeath
,   s.default_collation AS src_default_collation
,   t.text_length       AS tgt_text_length
,   t.text              AS tgt_text
,   t.text_vc           AS tgt_text_vc
,   t.type_text         AS tgt_type_text
,   t.oid_text          AS tgt_oid_text
,   t.view_type_owner   AS tgt_view_type_owner
,   t.view_type         AS tgt_view_type
,   t.superview_name    AS tgt_superview_name
,   t.read_only         AS tgt_read_only
,   t.bequeath          AS tgt_bequeath
,   t.default_collation AS tgt_default_collation
FROM src_views s
JOIN tgt_views t USING (view_name)
ORDER BY view_name
;
