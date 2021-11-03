
-- A COMPLETER

CREATE TABLE src_tab_privs
(
    table_name varchar(128) not null,
    tablespace_name varchar(30),
    CONSTRAINT pk_src_tab_privs PRIMARY KEY (table_name)
);

CREATE TABLE tgt_tab_privs
(
    table_name varchar(128) not null,
    tablespace_name varchar(30),
    CONSTRAINT pk_tgt_tab_privs PRIMARY KEY (table_name)
);
