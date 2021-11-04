
CREATE TABLE src_tables
(
    table_name varchar(128) not null,
    tablespace_name varchar(30),
    cluster_name varchar(128),
    iot_name varchar(128),
    status varchar(8),
    logging varchar(3),
    degree varchar(10),
    partitioned varchar(3),
    iot_type varchar(12),
    temporary varchar(1),
    nested varchar(3),
    duration varchar(15),
    cluster_owner varchar(128), 
    compression varchar(8),
    compress_for varchar(30),
    dropped varchar(3),
    read_only varchar(3),
    clustering varchar(3),
    has_identity varchar(3),
    container_data varchar(3),
    default_collation varchar(100),
    external varchar(3),
    CONSTRAINT pk_src_tables PRIMARY KEY (table_name)
);

CREATE TABLE tgt_tables
(
    table_name varchar(128) not null,
    tablespace_name varchar(30),
    cluster_name varchar(128),
    iot_name varchar(128),
    status varchar(8),
    logging varchar(3),
    degree varchar(10),
    partitioned varchar(3),
    iot_type varchar(12),
    temporary varchar(1),
    nested varchar(3),
    duration varchar(15),
    cluster_owner varchar(128), 
    compression varchar(8),
    compress_for varchar(30),
    dropped varchar(3),
    read_only varchar(3),
    clustering varchar(3),
    has_identity varchar(3),
    container_data varchar(3),
    default_collation varchar(100),
    external varchar(3),
    CONSTRAINT pk_tgt_tables PRIMARY KEY (table_name)
);
