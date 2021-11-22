
CREATE VIEW comp_view_comments AS
SELECT
    view_name
,   s.comments  AS src_comments
,   t.comments  AS tgt_comments
FROM src_view_comments s
JOIN tgt_view_comments t USING (view_name)
ORDER BY view_name
;
