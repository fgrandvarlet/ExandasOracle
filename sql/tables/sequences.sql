
CREATE TABLE src_sequences
(
    sequence_name varchar(128) not null,
    min_value bigint,
    max_value varchar(39),          -- stocker sous forme de varchar(39) id est valeur max d'un number + le signe
    increment_by integer not null,
    cycle_flag varchar(1),
    order_flag varchar(1),
    cache_size integer not null,
    scale_flag varchar(1),
    extend_flag varchar(1),
    sharded_flag varchar(1),
    session_flag varchar(1),
    keep_value varchar(1),
    CONSTRAINT pk_src_sequences PRIMARY KEY (sequence_name)
);

CREATE TABLE tgt_sequences
(
    sequence_name varchar(128) not null,
    min_value bigint,
    max_value varchar(39),          -- stocker sous forme de varchar(39) id est valeur max d'un number + le signe
    increment_by integer not null,
    cycle_flag varchar(1),
    order_flag varchar(1),
    cache_size integer not null,
    scale_flag varchar(1),
    extend_flag varchar(1),
    sharded_flag varchar(1),
    session_flag varchar(1),
    keep_value varchar(1),
    CONSTRAINT pk_tgt_sequences PRIMARY KEY (sequence_name)
);
