
CREATE TABLE src_source (
    source_name varchar(128) not null,
    source_type varchar(12) not null,
    line integer not null,
    text varchar(4000),
    CONSTRAINT pk_src_source PRIMARY KEY (source_name, source_type, line)
);

CREATE TABLE tgt_source (
    source_name varchar(128) not null,
    source_type varchar(12) not null,
    line integer not null,
    text varchar(4000),
    CONSTRAINT pk_tgt_source PRIMARY KEY (source_name, source_type, line)
);
