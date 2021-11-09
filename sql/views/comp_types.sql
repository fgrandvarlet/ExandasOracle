
CREATE VIEW comp_types AS
SELECT
    type_name
,   s.typecode          AS src_typecode
,   s.attributes        AS src_attributes
,   s.methods           AS src_methods
,   s.predefined        AS src_predefined
,   s.incomplete        AS src_incomplete
,   s.final             AS src_final
,   s.instantiable      AS src_instantiable
,   s.persistable       AS src_persistable
,   s.supertype_owner   AS src_supertype_owner
,   s.supertype_name    AS src_supertype_name
,   s.local_attributes  AS src_local_attributes
,   s.local_methods     AS src_local_methods
,   t.typecode          AS tgt_typecode
,   t.attributes        AS tgt_attributes
,   t.methods           AS tgt_methods
,   t.predefined        AS tgt_predefined
,   t.incomplete        AS tgt_incomplete
,   t.final             AS tgt_final
,   t.instantiable      AS tgt_instantiable
,   t.persistable       AS tgt_persistable
,   t.supertype_owner   AS tgt_supertype_owner
,   t.supertype_name    AS tgt_supertype_name
,   t.local_attributes  AS tgt_local_attributes
,   t.local_methods     AS tgt_local_methods
FROM src_types s
JOIN tgt_types t USING (type_name)
ORDER BY type_name
;
