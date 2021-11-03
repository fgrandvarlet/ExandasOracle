
CREATE TABLE comparison_set
(
    uid char(36) not null,
    name varchar(64) not null,
    connection1_uid char(36) not null,
    connection2_uid char(36) not null,
    schema1 varchar(128) not null,
    schema2 varchar(128) not null,
    last_report_time timestamp,
    CONSTRAINT pk_comparison_set PRIMARY KEY (uid),
    CONSTRAINT comparison_set_ak1 UNIQUE(name),
    CONSTRAINT comparison_set_fk1 FOREIGN KEY (connection1_uid) REFERENCES connection_params(uid),
    CONSTRAINT comparison_set_fk2 FOREIGN KEY (connection2_uid) REFERENCES connection_params(uid)
);
