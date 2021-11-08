
CREATE TABLE src_ind_columns
(
    index_name varchar(128) not null,
    table_owner varchar(128) not null,
    table_name varchar(128) not null,
    column_name varchar(128) not null,  -- colonne origine varchar2(4000) et nullable
    column_position integer not null,
    column_length integer not null,
    col_char_length integer,            -- nom colonne d'origine : char_length
    descend varchar(4),
    collated_column_id integer,
    CONSTRAINT pk_src_ind_columns PRIMARY KEY (index_name, column_name)
);

CREATE TABLE tgt_ind_columns
(
    index_name varchar(128) not null,
    table_owner varchar(128) not null,
    table_name varchar(128) not null,
    column_name varchar(128) not null,  -- colonne origine varchar2(4000) et nullable
    column_position integer not null,
    column_length integer not null,
    col_char_length integer,            -- nom colonne d'origine : char_length
    descend varchar(4),
    collated_column_id integer,
    CONSTRAINT pk_tgt_ind_columns PRIMARY KEY (index_name, column_name)
);
