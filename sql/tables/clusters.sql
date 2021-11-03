
CREATE TABLE src_clusters (
    cluster_name varchar(128) not null,
    tablespace_name varchar(30) not null,
    cluster_type varchar(5),
    clu_function varchar(15),       -- nom colonne d'origine : function
    hashkeys integer,
    degree varchar(10),
    cache varchar(5),
    single_table varchar(5),
    dependencies varchar(8),
    CONSTRAINT pk_src_clusters PRIMARY KEY (cluster_name)
);

CREATE TABLE tgt_clusters (
    cluster_name varchar(128) not null,
    tablespace_name varchar(30) not null,
    cluster_type varchar(5),
    clu_function varchar(15),       -- nom colonne d'origine : function
    hashkeys integer,
    degree varchar(10),
    cache varchar(5),
    single_table varchar(5),
    dependencies varchar(8),
    CONSTRAINT pk_tgt_clusters PRIMARY KEY (cluster_name)
);
