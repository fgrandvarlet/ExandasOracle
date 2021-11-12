
CREATE TABLE src_part_indexes
(
    index_name varchar(128) not null,
    table_name varchar(128) not null,
    partitioning_type varchar(9),
    subpartitioning_type varchar(9),
    partition_count integer not null,
    def_subpartition_count integer,
    partitioning_key_count integer not null,
    subpartitioning_key_count integer,
    locality varchar(6),
    alignment varchar(12),
    def_tablespace_name varchar(30),
    def_logging varchar(7),
    def_parameters varchar(1000),
    interval varchar(1000),
    autolist varchar(3),
    interval_subpartition varchar(1000),
    autolist_subpartition varchar(3),
    CONSTRAINT pk_src_part_indexes PRIMARY KEY (index_name)
);

CREATE TABLE tgt_part_indexes
(
    index_name varchar(128) not null,
    table_name varchar(128) not null,
    partitioning_type varchar(9),
    subpartitioning_type varchar(9),
    partition_count integer not null,
    def_subpartition_count integer,
    partitioning_key_count integer not null,
    subpartitioning_key_count integer,
    locality varchar(6),
    alignment varchar(12),
    def_tablespace_name varchar(30),
    def_logging varchar(7),
    def_parameters varchar(1000),
    interval varchar(1000),
    autolist varchar(3),
    interval_subpartition varchar(1000),
    autolist_subpartition varchar(3),
    CONSTRAINT pk_tgt_part_indexes PRIMARY KEY (index_name)
);
