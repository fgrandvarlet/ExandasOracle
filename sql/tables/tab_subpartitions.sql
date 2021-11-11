
CREATE TABLE src_tab_subpartitions
(
    table_name varchar(128) not null,
    partition_name varchar(128) not null,
    subpartition_name varchar(128) not null,
    high_value BLOB SUB_TYPE TEXT,
    high_value_length integer not null,
    partition_position integer,
    subpartition_position integer,
    tablespace_name varchar(30),
    logging varchar(7),
    compression varchar(8),
    compress_for varchar(30),
    interval varchar(3),
    indexing varchar(4),
    read_only varchar(4),
    CONSTRAINT pk_src_tab_subpartitions PRIMARY KEY (table_name, partition_name, subpartition_name)
);

CREATE TABLE tgt_tab_subpartitions
(
    table_name varchar(128) not null,
    partition_name varchar(128) not null,
    subpartition_name varchar(128) not null,
    high_value BLOB SUB_TYPE TEXT,
    high_value_length integer not null,
    partition_position integer,
    subpartition_position integer,
    tablespace_name varchar(30),
    logging varchar(7),
    compression varchar(8),
    compress_for varchar(30),
    interval varchar(3),
    indexing varchar(4),
    read_only varchar(4),
    CONSTRAINT pk_tgt_tab_subpartitions PRIMARY KEY (table_name, partition_name, subpartition_name)
);
