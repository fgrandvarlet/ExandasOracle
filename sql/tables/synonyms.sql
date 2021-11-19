
CREATE TABLE src_synonyms
(
    owner varchar(128) not null,
    synonym_name varchar(128) not null,
    table_owner varchar(128),
    table_name varchar(128),
    db_link varchar(128),
    CONSTRAINT pk_src_synonyms PRIMARY KEY (owner, synonym_name)
);

CREATE TABLE tgt_synonyms
(
    owner varchar(128) not null,
    synonym_name varchar(128) not null,
    table_owner varchar(128),
    table_name varchar(128),
    db_link varchar(128),
    CONSTRAINT pk_tgt_synonyms PRIMARY KEY (owner, synonym_name)
);
