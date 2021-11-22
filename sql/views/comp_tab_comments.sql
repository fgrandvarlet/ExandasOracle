
CREATE VIEW comp_tab_comments AS
SELECT
    table_name
,   s.comments  AS src_comments
,   t.comments  AS tgt_comments
FROM src_tab_comments s
JOIN tgt_tab_comments t USING (table_name)
ORDER BY table_name
;
