
-- A COMPLETER DANS UN SECOND LOT

CREATE TABLE src_trigger_cols
(
    trigger_name varchar(128) not null,
    CONSTRAINT pk_src_trigger_cols PRIMARY KEY (trigger_name)
);

CREATE TABLE tgt_trigger_cols
(
    trigger_name varchar(128) not null,
    CONSTRAINT pk_tgt_trigger_cols PRIMARY KEY (trigger_name)
);
