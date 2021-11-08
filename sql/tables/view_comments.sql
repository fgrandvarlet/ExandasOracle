
CREATE TABLE src_view_comments
(
    view_name varchar(128) not null,
    comments varchar(4000),
    CONSTRAINT pk_src_view_comments PRIMARY KEY (view_name)
);

CREATE TABLE tgt_view_comments
(
    view_name varchar(128) not null,
    comments varchar(4000),
    CONSTRAINT pk_tgt_view_comments PRIMARY KEY (view_name)
);
