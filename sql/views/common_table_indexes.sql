
CREATE VIEW common_table_indexes AS
SELECT index_name, table_name
FROM src_table_indexes JOIN tgt_table_indexes USING(index_name, table_name)
;
