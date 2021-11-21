
CREATE VIEW common_objects AS
SELECT table_name FROM common_tables
UNION ALL
SELECT view_name FROM common_views
UNION ALL
SELECT source_name FROM common_source_synthesis
;
