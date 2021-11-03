
CREATE VIEW common_tables AS
SELECT table_name
FROM src_tables JOIN tgt_tables USING(table_name)
;
