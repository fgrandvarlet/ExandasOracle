
CREATE VIEW comp_cons_columns AS
SELECT
    constraint_name
,   table_name
,   column_name
,   s.col_position  AS src_col_position
,   t.col_position  AS tgt_col_position
FROM src_cons_columns s
JOIN tgt_cons_columns t USING (table_name, constraint_name, column_name)
ORDER BY table_name, constraint_name, column_name
;
