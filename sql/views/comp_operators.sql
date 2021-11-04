
CREATE VIEW comp_operators AS
SELECT
    operator_name
,   s.number_of_binds   AS src_number_of_binds
,   t.number_of_binds   AS tgt_number_of_binds
FROM src_operators s
JOIN tgt_operators t USING (operator_name)
ORDER BY operator_name
;
