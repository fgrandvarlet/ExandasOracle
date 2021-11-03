
CREATE TABLE src_ind_subpartitions (
    index_name varchar(128) not null,
    partition_name varchar(128) not null,
    subpartition_name varchar(128) not null,
    high_value BLOB SUB_TYPE TEXT,
    high_value_length integer not null,
    partition_position integer,
    subpartition_position integer,
    status varchar(8),
    tablespace_name varchar(30) not null,
    logging varchar(3),
    compression varchar(13),
    CONSTRAINT pk_src_ind_subpartitions PRIMARY KEY (index_name, partition_name, subpartition_name)
);

CREATE TABLE tgt_ind_subpartitions (
    index_name varchar(128) not null,
    partition_name varchar(128) not null,
    subpartition_name varchar(128) not null,
    high_value BLOB SUB_TYPE TEXT,
    high_value_length integer not null,
    partition_position integer,
    subpartition_position integer,
    status varchar(8),
    tablespace_name varchar(30) not null,
    logging varchar(3),
    compression varchar(13),
    CONSTRAINT pk_tgt_ind_subpartitions PRIMARY KEY (index_name, partition_name, subpartition_name)
);
