
CREATE VIEW common_source_synthesis AS
SELECT DISTINCT source_name
FROM src_source_synthesis
JOIN tgt_source_synthesis USING(source_name, source_type)
WHERE source_type IN ('PROCEDURE', 'FUNCTION', 'PACKAGE')
;
