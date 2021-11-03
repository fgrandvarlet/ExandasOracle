
CREATE VIEW comp_tables AS
SELECT
    table_name
,   s.tablespace_name AS src_tablespace_name
,   t.tablespace_name AS tgt_tablespace_name
FROM src_tables s
JOIN tgt_tables t USING(table_name)
ORDER BY table_name
;
