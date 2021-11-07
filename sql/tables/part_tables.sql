
CREATE TABLE src_part_tables
(
    table_name varchar(128) not null,
    partitioning_type varchar(9),
    subpartitioning_type varchar(9),
    partition_count integer not null,
    def_subpartition_count integer,
    partitioning_key_count integer not null,
    subpartitioning_key_count integer,
    status varchar(8),
    def_tablespace_name varchar(30),
    def_logging varchar(7),
    def_compression varchar(8),
    def_compress_for varchar(30),
    ref_ptn_constraint_name varchar(128),
    interval varchar(1000),
    autolist varchar(3),
    interval_subpartition varchar(1000),
    autolist_subpartition varchar(3),
    is_nested varchar(3),
    def_indexing varchar(3),
    def_read_only varchar(3),
    CONSTRAINT pk_src_part_tables PRIMARY KEY (table_name)
);

CREATE TABLE tgt_part_tables
(
    table_name varchar(128) not null,
    partitioning_type varchar(9),
    subpartitioning_type varchar(9),
    partition_count integer not null,
    def_subpartition_count integer,
    partitioning_key_count integer not null,
    subpartitioning_key_count integer,
    status varchar(8),
    def_tablespace_name varchar(30),
    def_logging varchar(7),
    def_compression varchar(8),
    def_compress_for varchar(30),
    ref_ptn_constraint_name varchar(128),
    interval varchar(1000),
    autolist varchar(3),
    interval_subpartition varchar(1000),
    autolist_subpartition varchar(3),
    is_nested varchar(3),
    def_indexing varchar(3),
    def_read_only varchar(3),
    CONSTRAINT pk_tgt_part_tables PRIMARY KEY (table_name)
);

table_name, partitioning_type, subpartitioning_type, partition_count, def_subpartition_count, partitioning_key_count, subpartitioning_key_count,
status, def_tablespace_name, def_logging, def_compression, def_compress_for, ref_ptn_constraint_name, interval, autolist,
interval_subpartition, autolist_subpartition, is_nested, def_indexing, def_read_only



