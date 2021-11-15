
CREATE TABLE src_clu_columns
(
    cluster_name varchar(128) not null,
    clu_column_name varchar(128) not null,
    table_name varchar(128) not null,
    tab_column_name varchar(4000),
    CONSTRAINT pk_src_clu_columns PRIMARY KEY (cluster_name, clu_column_name, table_name)
);

CREATE TABLE tgt_clu_columns
(
    cluster_name varchar(128) not null,
    clu_column_name varchar(128) not null,
    table_name varchar(128) not null,
    tab_column_name varchar(4000),
    CONSTRAINT pk_tgt_clu_columns PRIMARY KEY (cluster_name, clu_column_name, table_name)
);
