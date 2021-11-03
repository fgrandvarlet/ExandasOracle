
-- A COMPLETER DANS UN DEUXIEME LOT

CREATE TABLE src_types
(
    type_name varchar(128) not null,
    CONSTRAINT pk_src_types PRIMARY KEY (type_name)
);

CREATE TABLE tgt_types
(
    type_name varchar(128) not null,
    CONSTRAINT pk_tgt_types PRIMARY KEY (type_name)
);
