
CREATE VIEW comp_tab_cols AS
SELECT
    table_name
,   column_name
,   s.data_type         AS src_data_type
,   s.data_type_mod     AS src_data_type_mod
,   s.data_type_owner   AS src_data_type_owner
,   s.data_length       AS src_data_length
,   s.data_precision    AS src_data_precision
,   s.data_scale        AS src_data_scale
,   s.nullable          AS src_nullable
,   s.column_id         AS src_column_id
,   s.default_length    AS src_default_length
,   s.data_default      AS src_data_default
,   s.col_char_length   AS src_col_char_length
,   s.char_used         AS src_char_used
,   s.hidden_column     AS src_hidden_column
,   s.virtual_column    AS src_virtual_column
,   s.default_on_null   AS src_default_on_null
,   s.identity_column   AS src_identity_column
,   s.collation         AS src_collation
,   t.data_type         AS tgt_data_type
,   t.data_type_mod     AS tgt_data_type_mod
,   t.data_type_owner   AS tgt_data_type_owner
,   t.data_length       AS tgt_data_length
,   t.data_precision    AS tgt_data_precision
,   t.data_scale        AS tgt_data_scale
,   t.nullable          AS tgt_nullable
,   t.column_id         AS tgt_column_id
,   t.default_length    AS tgt_default_length
,   t.data_default      AS tgt_data_default
,   t.col_char_length   AS tgt_col_char_length
,   t.char_used         AS tgt_char_used
,   t.hidden_column     AS tgt_hidden_column
,   t.virtual_column    AS tgt_virtual_column
,   t.default_on_null   AS tgt_default_on_null
,   t.identity_column   AS tgt_identity_column
,   t.collation         AS tgt_collation
FROM src_tab_cols s
JOIN tgt_tab_cols t USING(table_name, column_name)
ORDER BY table_name, column_name
;
