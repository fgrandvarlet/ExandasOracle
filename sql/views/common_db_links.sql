
CREATE VIEW common_db_links AS
SELECT db_link
FROM src_db_links
JOIN tgt_db_links USING(db_link)
;
