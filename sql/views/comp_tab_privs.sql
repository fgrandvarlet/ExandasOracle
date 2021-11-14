
CREATE VIEW comp_tab_privs AS
SELECT
    grantee
,   table_name
,   privilege
,   inherited
,   s.table_schema  AS src_table_schema
,   s.grantable     AS src_grantable
,   s.hierarchy     AS src_hierarchy
,   s.common        AS src_common
,   s.type          AS src_type
,   t.table_schema  AS tgt_table_schema
,   t.grantable     AS tgt_grantable
,   t.hierarchy     AS tgt_hierarchy
,   t.common        AS tgt_common
,   t.type          AS tgt_type
FROM src_tab_privs s
JOIN tgt_tab_privs t USING(grantee, table_name, privilege, inherited)
ORDER BY grantee, table_name, privilege, inherited
;
