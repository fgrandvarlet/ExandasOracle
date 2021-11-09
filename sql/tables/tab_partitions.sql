
-- ETUDE CLASSE ABSTRAITE Partition dont hériteraient IndexPartition et TablePartition

CREATE TABLE src_tab_partitions
(
    table_name varchar(128) not null,
    composite varchar(3),
    partition_name varchar(128),
    subpartition_count integer,
    high_value BLOB SUB_TYPE TEXT,
    high_value_length integer,
    partition_position integer,
    tablespace_name varchar(30),
    logging varchar(7),
    compression varchar(8),
    compress_for varchar(30),
    is_nested varchar(3),
    parent_table_partition varchar(128),
    interval varchar(3),
    indexing varchar(4),
    read_only varchar(4),
    CONSTRAINT pk_src_tab_partitions PRIMARY KEY (table_name, partition_name)
);

CREATE TABLE tgt_tab_partitions
(
    table_name varchar(128) not null,
    composite varchar(3),
    partition_name varchar(128),
    subpartition_count integer,
    high_value BLOB SUB_TYPE TEXT,
    high_value_length integer,
    partition_position integer,
    tablespace_name varchar(30),
    logging varchar(7),
    compression varchar(8),
    compress_for varchar(30),
    is_nested varchar(3),
    parent_table_partition varchar(128),
    interval varchar(3),
    indexing varchar(4),
    read_only varchar(4),
    CONSTRAINT pk_tgt_tab_partitions PRIMARY KEY (table_name, partition_name)
);
