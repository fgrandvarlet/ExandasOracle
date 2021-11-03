
CREATE TABLE src_col_comments (
    table_name varchar(128) not null,
    column_name varchar(128) not null,
    comments varchar(4000),
    CONSTRAINT pk_src_col_comments PRIMARY KEY (table_name, column_name)
);

CREATE TABLE tgt_col_comments (
    table_name varchar(128) not null,
    column_name varchar(128) not null,
    comments varchar(4000),
    CONSTRAINT pk_tgt_col_comments PRIMARY KEY (table_name, column_name)
);
