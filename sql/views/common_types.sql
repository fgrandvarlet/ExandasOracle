
CREATE VIEW common_types AS
SELECT type_name
FROM src_types
JOIN tgt_types USING(type_name)
;
