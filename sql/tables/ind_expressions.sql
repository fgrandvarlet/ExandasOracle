
CREATE TABLE src_ind_expressions
(
    index_name varchar(128) not null,
    table_owner varchar(128) not null,
    table_name varchar(128) not null,
    column_expression BLOB SUB_TYPE TEXT,
    column_position integer not null,
    CONSTRAINT pk_src_ind_expressions PRIMARY KEY (index_name, column_position)
);

CREATE TABLE tgt_ind_expressions
(
    index_name varchar(128) not null,
    table_owner varchar(128) not null,
    table_name varchar(128) not null,
    column_expression BLOB SUB_TYPE TEXT,    
    column_position integer not null,
    CONSTRAINT pk_tgt_ind_expressions PRIMARY KEY (index_name, column_position)
);
