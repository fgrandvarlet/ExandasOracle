
-- A COMPLETER

CREATE TABLE src_tables
(
    table_name varchar(128) not null,
    tablespace_name varchar(30),
    CONSTRAINT pk_src_tables PRIMARY KEY (table_name)
);

CREATE TABLE tgt_tables
(
    table_name varchar(128) not null,
    tablespace_name varchar(30),
    CONSTRAINT pk_tgt_tables PRIMARY KEY (table_name)
);
