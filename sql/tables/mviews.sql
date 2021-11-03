
CREATE TABLE src_mviews (
    mview_name varchar(128) not null,
    container_name varchar(128) not null,
    query BLOB SUB_TYPE TEXT,
    query_len integer,
    updatable varchar(1),
    update_log varchar(128),
    master_rollback_seg varchar(128),
    master_link varchar(128),
    rewrite_enabled varchar(1),
    rewrite_capability varchar(9),
    refresh_mode varchar(6),
    refresh_method varchar(8),
    build_mode varchar(9),
    fast_refreshable varchar(18),
    use_no_index varchar(1),
    default_collation varchar(100),
    CONSTRAINT pk_src_mviews PRIMARY KEY (mview_name)
);

CREATE TABLE tgt_mviews (
    mview_name varchar(128) not null,
    container_name varchar(128) not null,
    query BLOB SUB_TYPE TEXT,
    query_len integer,
    updatable varchar(1),
    update_log varchar(128),
    master_rollback_seg varchar(128),
    master_link varchar(128),
    rewrite_enabled varchar(1),
    rewrite_capability varchar(9),
    refresh_mode varchar(6),
    refresh_method varchar(8),
    build_mode varchar(9),
    fast_refreshable varchar(18),
    use_no_index varchar(1),
    default_collation varchar(100),
    CONSTRAINT pk_tgt_mviews PRIMARY KEY (mview_name)
);
