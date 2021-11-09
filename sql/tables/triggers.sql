
-- VERIFIER UNICITE trigger_name

CREATE TABLE src_triggers
(
    trigger_name varchar(128) not null,
    trigger_type varchar(16),
    triggering_event varchar(246),
    table_owner varchar(128),
    base_object_type varchar(18),
    table_name varchar(128),
    column_name varchar(128),           -- colonne origine varchar2(4000)
    referencing_names varchar(422),
    when_clause varchar(4000),
    status varchar(8),
    description varchar(4000),
    action_type varchar(11),
    trigger_body BLOB SUB_TYPE TEXT,
    before_statement varchar(3),
    before_row varchar(3),
    after_row varchar(3),
    after_statement varchar(3),
    instead_of_row varchar(3),
    fire_once varchar(3),
    apply_server_only varchar(3),
    CONSTRAINT pk_src_triggers PRIMARY KEY (trigger_name)
);

CREATE TABLE tgt_triggers
(
    trigger_name varchar(128) not null,
    trigger_type varchar(16),
    triggering_event varchar(246),
    table_owner varchar(128),
    base_object_type varchar(18),
    table_name varchar(128),
    column_name varchar(128),           -- colonne origine varchar2(4000)
    referencing_names varchar(422),
    when_clause varchar(4000),
    status varchar(8),
    description varchar(4000),
    action_type varchar(11),
    trigger_body BLOB SUB_TYPE TEXT,
    before_statement varchar(3),
    before_row varchar(3),
    after_row varchar(3),
    after_statement varchar(3),
    instead_of_row varchar(3),
    fire_once varchar(3),
    apply_server_only varchar(3),
    CONSTRAINT pk_tgt_triggers PRIMARY KEY (trigger_name)
);
