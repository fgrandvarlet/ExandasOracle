
CREATE VIEW common_objects AS
SELECT table_name FROM common_tables
UNION
SELECT view_name FROM common_views
UNION
SELECT mview_name FROM common_mviews
UNION
SELECT sequence_name FROM common_sequences
UNION
SELECT source_name FROM common_source_synthesis
UNION
SELECT type_name FROM common_types
UNION
SELECT db_link FROM common_db_links
;
