
CREATE TABLE src_cluster_cols
(
    table_name varchar(128) not null,
    column_name varchar(128) not null,
    data_type varchar(128),
    data_type_mod varchar(3),
    data_type_owner varchar(128),
    data_length integer not null,
    data_precision integer,
    data_scale integer,
    nullable varchar(1),
    column_id integer,
    default_length integer,
    data_default BLOB SUB_TYPE TEXT,
    col_char_length integer,    -- original name char_length
    char_used varchar(1),
    hidden_column varchar(3),
    virtual_column varchar(3),
    default_on_null varchar(3),
    identity_column varchar(3),
    collation varchar(100),
    CONSTRAINT pk_src_cluster_cols PRIMARY KEY (table_name, column_name)
);

CREATE TABLE tgt_cluster_cols
(
    table_name varchar(128) not null,
    column_name varchar(128) not null,
    data_type varchar(128),
    data_type_mod varchar(3),
    data_type_owner varchar(128),
    data_length integer not null,
    data_precision integer,
    data_scale integer,
    nullable varchar(1),
    column_id integer,
    default_length integer,
    data_default BLOB SUB_TYPE TEXT,
    col_char_length integer,    -- original name char_length
    char_used varchar(1),
    hidden_column varchar(3),
    virtual_column varchar(3),
    default_on_null varchar(3),
    identity_column varchar(3),
    collation varchar(100),
    CONSTRAINT pk_tgt_cluster_cols PRIMARY KEY (table_name, column_name)
);
