
CREATE VIEW comp_mview_comments AS
SELECT
    mview_name
,   s.comments  AS src_comments
,   t.comments  AS tgt_comments
FROM src_mview_comments s
JOIN tgt_mview_comments t USING (mview_name)
ORDER BY mview_name
;
