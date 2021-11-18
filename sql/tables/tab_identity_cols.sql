
CREATE TABLE src_tab_identity_cols
(
    table_name varchar(128) not null,
    column_name varchar(128) not null,
    generation_type varchar(10),
    sequence_name varchar(128) not null,
    identity_options varchar(298),
    CONSTRAINT pk_src_tab_identity_cols PRIMARY KEY (table_name, column_name)
);

CREATE TABLE tgt_tab_identity_cols
(
    table_name varchar(128) not null,
    column_name varchar(128) not null,
    generation_type varchar(10),
    sequence_name varchar(128) not null,
    identity_options varchar(298),
    CONSTRAINT pk_tgt_tab_identity_cols PRIMARY KEY (table_name, column_name)
);
