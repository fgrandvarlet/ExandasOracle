
CREATE TABLE src_part_tables (
    table_name varchar(128) not null,
    CONSTRAINT pk_src_part_tables PRIMARY KEY (table_name)
);

CREATE TABLE tgt_part_tables (
    table_name varchar(128) not null,
    CONSTRAINT pk_tgt_part_tables PRIMARY KEY (table_name)
);
