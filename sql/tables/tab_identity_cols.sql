
-- A COMPLETER DANS UN SECOND LOT

CREATE TABLE src_tab_identity_cols
(
    table_name varchar(128) not null,
    tablespace_name varchar(30),
    CONSTRAINT pk_src_tab_identity_cols PRIMARY KEY (table_name)
);

CREATE TABLE tgt_tab_identity_cols
(
    table_name varchar(128) not null,
    tablespace_name varchar(30),
    CONSTRAINT pk_tgt_tab_identity_cols PRIMARY KEY (table_name)
);
