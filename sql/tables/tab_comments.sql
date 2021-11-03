
CREATE TABLE src_tab_comments (
    table_name varchar(128) not null,
    comments varchar(4000),
    CONSTRAINT pk_src_tab_comments PRIMARY KEY (table_name)
);

CREATE TABLE tgt_tab_comments (
    table_name varchar(128) not null,
    comments varchar(4000),
    CONSTRAINT pk_tgt_tab_comments PRIMARY KEY (table_name)
);
