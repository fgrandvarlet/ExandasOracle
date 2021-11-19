
CREATE VIEW comp_ind_expressions AS
SELECT
    index_name
,   column_position
,   s.table_owner       AS src_table_owner
,   s.table_name        AS src_table_name
,   s.column_expression AS src_column_expression
,   t.table_owner       AS tgt_table_owner
,   t.table_name        AS tgt_table_name
,   t.column_expression AS tgt_column_expression
FROM src_ind_expressions s
JOIN tgt_ind_expressions t USING (index_name, column_position)
JOIN common_indexes USING (index_name)
ORDER BY index_name, column_position
;
