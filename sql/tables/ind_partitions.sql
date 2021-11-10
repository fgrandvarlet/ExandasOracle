
CREATE TABLE src_ind_partitions
(
    index_name varchar(128) not null,
    composite varchar(3),
    partition_name varchar(128) not null,
    subpartition_count integer,
    high_value BLOB SUB_TYPE TEXT,
    high_value_length integer,
    partition_position integer,
    status varchar(8),
    tablespace_name varchar(30),
    logging varchar(7),
    compression varchar(13),
    parameters varchar(1000),
    interval varchar(3),
    CONSTRAINT pk_src_ind_partitions PRIMARY KEY (index_name, partition_name)
);

CREATE TABLE tgt_ind_partitions
(
    index_name varchar(128) not null,
    composite varchar(3),
    partition_name varchar(128) not null,
    subpartition_count integer,
    high_value BLOB SUB_TYPE TEXT,
    high_value_length integer,
    partition_position integer,
    status varchar(8),
    tablespace_name varchar(30),
    logging varchar(7),
    compression varchar(13),
    parameters varchar(1000),
    interval varchar(3),
    CONSTRAINT pk_tgt_ind_partitions PRIMARY KEY (index_name, partition_name)
);
