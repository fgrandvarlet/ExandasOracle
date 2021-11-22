
CREATE VIEW common_sequences AS
SELECT sequence_name
FROM src_sequences
JOIN tgt_sequences USING(sequence_name)
;
