
/*
    filtrer sur table_type = table ? autre possibilité : cluster
    filtrer sur generated = 'N'
*/

CREATE TABLE src_table_indexes
(
    index_name varchar(128) not null,
    index_type varchar(27),
    table_name varchar(128) not null,
    uniqueness varchar(9),
    compression varchar(13),
    prefix_length integer,
    tablespace_name varchar(30),
    include_column integer,
    logging varchar(3),
    status varchar(8),
    degree varchar(40),
    partitioned varchar(3),
    temporary varchar(1),
    duration varchar(15),
    CONSTRAINT pk_src_table_indexes PRIMARY KEY (index_name)
);

CREATE TABLE tgt_table_indexes
(
    index_name varchar(128) not null,
    index_type varchar(27),
    table_name varchar(128) not null,
    uniqueness varchar(9),
    compression varchar(13),
    prefix_length integer,
    tablespace_name varchar(30),
    include_column integer,
    logging varchar(3),
    status varchar(8),
    degree varchar(40),
    partitioned varchar(3),
    temporary varchar(1),
    duration varchar(15),
    CONSTRAINT pk_tgt_table_indexes PRIMARY KEY (index_name)
);
