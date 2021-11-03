
-- TODO vue common_columns (UNION entre tab_columns et view_columns ? et clu_columns ?)

CREATE VIEW comp_col_comments AS
SELECT
    table_name
,   column_name
,   s.comments  AS src_comments
,   t.comments  AS tgt_comments
FROM src_col_comments s
JOIN tgt_col_comments t USING (table_name, column_name)
JOIN common_columns USING (table_name, column_name)
ORDER BY table_name, column_name
;
