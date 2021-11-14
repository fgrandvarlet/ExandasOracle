
CREATE TABLE src_tab_privs
(
    grantee varchar(128) not null,
    table_schema varchar(128) not null,
    table_name varchar(128) not null,
    privilege varchar(40) not null,
    grantable varchar(3),
    hierarchy varchar(3),
    common varchar(3),
    type varchar(24),
    inherited varchar(3) not null,
    CONSTRAINT pk_src_tab_privs PRIMARY KEY (grantee, table_name, privilege, inherited)
);

CREATE TABLE tgt_tab_privs
(
    grantee varchar(128) not null,
    table_schema varchar(128) not null,
    table_name varchar(128) not null,
    privilege varchar(40) not null,
    grantable varchar(3),
    hierarchy varchar(3),
    common varchar(3),
    type varchar(24),
    inherited varchar(3) not null,
    CONSTRAINT pk_tgt_tab_privs PRIMARY KEY (grantee, table_name, privilege, inherited)
);
