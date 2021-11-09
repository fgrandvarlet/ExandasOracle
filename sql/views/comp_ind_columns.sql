
CREATE VIEW comp_ind_columns AS
SELECT
    table_name
,   index_name
,   column_name
,   s.table_owner           AS src_table_owner
,   s.column_position       AS src_column_position
,   s.column_length         AS src_column_length
,   s.col_char_length       AS src_col_char_length
,   s.descend               AS src_descend
,   s.collated_column_id    AS src_collated_column_id
,   t.table_owner           AS tgt_table_owner
,   t.column_position       AS tgt_column_position
,   t.column_length         AS tgt_column_length
,   t.col_char_length       AS tgt_col_char_length
,   t.descend               AS tgt_descend
,   t.collated_column_id    AS tgt_collated_column_id
FROM src_ind_columns s
JOIN tgt_ind_columns t USING (table_name, index_name, column_name)
ORDER BY table_name, index_name, column_name
;
