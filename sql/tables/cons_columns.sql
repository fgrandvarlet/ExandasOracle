
CREATE TABLE src_cons_columns
(
    constraint_name varchar(128) not null,
    table_name varchar(128) not null,
    column_name varchar(128) not null,  -- limiter à varchar(128) au lieu de varchar(4000) cause limitation Firebird et tronquer dans la requête d'import à l'aide de substr(column_name, 1, 128)
    col_position integer,               -- nom colonne d'origine : position
    CONSTRAINT pk_src_cons_columns PRIMARY KEY (table_name, constraint_name, column_name)
);

CREATE TABLE tgt_cons_columns
(
    constraint_name varchar(128) not null,
    table_name varchar(128) not null,
    column_name varchar(128) not null,  -- limiter à varchar(128) au lieu de varchar(4000) cause limitation Firebird et tronquer dans la requête d'import à l'aide de substr(column_name, 1, 128)
    col_position integer,               -- nom colonne d'origine : position
    CONSTRAINT pk_tgt_cons_columns PRIMARY KEY (table_name, constraint_name, column_name)
);
