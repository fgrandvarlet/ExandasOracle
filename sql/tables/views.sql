
CREATE TABLE src_views
(
    view_name varchar(128) not null,
    text_length integer,
    text BLOB SUB_TYPE TEXT,
    text_vc varchar(4000),
    type_text varchar(128),         -- limiter à varchar(128) au lieu de varchar(4000) cause limitation Firebird et tronquer dans la requête d'import à l'aide de substr(column_name, 1, 128)
    oid_text varchar(128),          -- limiter à varchar(128) au lieu de varchar(4000) cause limitation Firebird et tronquer dans la requête d'import à l'aide de substr(column_name, 1, 128)
    view_type_owner varchar(128),
    view_type varchar(128),
    superview_name varchar(128),
    read_only varchar(1),
    bequeath varchar(12),
    default_collation varchar(100),
    CONSTRAINT pk_src_views PRIMARY KEY (view_name)
);

CREATE TABLE tgt_views
(
    view_name varchar(128) not null,
    text_length integer,
    text BLOB SUB_TYPE TEXT,
    text_vc varchar(4000),
    type_text varchar(128),         -- limiter à varchar(128) au lieu de varchar(4000) cause limitation Firebird et tronquer dans la requête d'import à l'aide de substr(column_name, 1, 128)
    oid_text varchar(128),          -- limiter à varchar(128) au lieu de varchar(4000) cause limitation Firebird et tronquer dans la requête d'import à l'aide de substr(column_name, 1, 128)
    view_type_owner varchar(128),
    view_type varchar(128),
    superview_name varchar(128),
    read_only varchar(1),
    bequeath varchar(12),
    default_collation varchar(100),
    CONSTRAINT pk_tgt_views PRIMARY KEY (view_name)
);
