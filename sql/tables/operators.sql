
CREATE TABLE src_operators
(
    operator_name varchar(128) not null,
    number_of_binds integer,
    CONSTRAINT pk_src_operators PRIMARY KEY (operator_name)
);

CREATE TABLE tgt_operators
(
    operator_name varchar(128) not null,
    number_of_binds integer,
    CONSTRAINT pk_tgt_operators PRIMARY KEY (operator_name)
);
