
CREATE VIEW common_tables_views AS
SELECT table_name FROM common_tables
UNION ALL
SELECT view_name FROM common_views
;
