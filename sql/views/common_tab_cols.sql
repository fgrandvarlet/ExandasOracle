
CREATE VIEW common_tab_cols AS
SELECT table_name, column_name
FROM src_tab_cols
JOIN tgt_tab_cols USING(table_name, column_name)
;
