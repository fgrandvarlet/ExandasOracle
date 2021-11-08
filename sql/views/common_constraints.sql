
CREATE VIEW common_constraints AS
SELECT constraint_name, table_name FROM
(
    SELECT constraint_name, table_name FROM src_checks
    UNION ALL
    SELECT constraint_name, table_name FROM src_primary_keys
    UNION ALL
    SELECT constraint_name, table_name FROM src_uniques
    UNION ALL
    SELECT constraint_name, table_name FROM src_foreign_keys
    UNION ALL
    SELECT constraint_name, table_name FROM src_constraints
) AS src
JOIN
(
    SELECT constraint_name, table_name FROM tgt_checks
    UNION ALL
    SELECT constraint_name, table_name FROM tgt_primary_keys
    UNION ALL
    SELECT constraint_name, table_name FROM tgt_uniques
    UNION ALL
    SELECT constraint_name, table_name FROM tgt_foreign_keys
    UNION ALL
    SELECT constraint_name, table_name FROM tgt_constraints
) AS tgt
USING(constraint_name, table_name)
;
