
CREATE VIEW common_mviews AS
SELECT mview_name
FROM src_mviews
JOIN tgt_mviews USING(mview_name)
;
