
CREATE TABLE src_source_synthesis
(
    source_name varchar(128) not null,
    source_type varchar(12) not null,
    text BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_src_source_synthesis PRIMARY KEY (source_name, source_type)
);

CREATE TABLE tgt_source_synthesis
(
    source_name varchar(128) not null,
    source_type varchar(12) not null,
    text BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_tgt_source_synthesis PRIMARY KEY (source_name, source_type)
);
