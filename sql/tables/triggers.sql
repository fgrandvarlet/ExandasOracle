
-- A COMPLETER

CREATE TABLE src_triggers
(
    trigger_name varchar(128) not null,
    CONSTRAINT pk_src_triggers PRIMARY KEY (trigger_name)
);

CREATE TABLE tgt_triggers
(
    trigger_name varchar(128) not null,
    CONSTRAINT pk_tgt_triggers PRIMARY KEY (trigger_name)
);
