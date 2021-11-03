
CREATE VIEW comp_source_synthesis AS
SELECT
    source_name
,   source_type
,   s.text  AS src_text
,   t.text  AS tgt_text
FROM src_source_synthesis s
JOIN tgt_source_synthesis t USING(source_name, source_type)
ORDER BY source_name, source_type
;
