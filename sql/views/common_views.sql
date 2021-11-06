
CREATE VIEW common_views AS
SELECT view_name
FROM src_views
JOIN tgt_views USING(view_name)
;
