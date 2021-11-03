
CREATE TABLE src_mview_comments (
    mview_name varchar(128) not null,
    comments varchar(4000),
    CONSTRAINT pk_src_mview_comments PRIMARY KEY (mview_name)
);

CREATE TABLE tgt_mview_comments (
    mview_name varchar(128) not null,
    comments varchar(4000),
    CONSTRAINT pk_tgt_mview_comments PRIMARY KEY (mview_name)
);
