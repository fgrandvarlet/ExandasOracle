
CREATE TABLE src_db_links (
    db_link varchar(128) not null,
    username varchar(128),
    host varchar(2000),
    shard_internal varchar(3),
    valid varchar(3),
    CONSTRAINT pk_src_db_links PRIMARY KEY (db_link)
);

CREATE TABLE tgt_db_links (
    db_link varchar(128) not null,
    username varchar(128),
    host varchar(2000),
    shard_internal varchar(3),
    valid varchar(3),
    CONSTRAINT pk_tgt_db_links PRIMARY KEY (db_link)
);
