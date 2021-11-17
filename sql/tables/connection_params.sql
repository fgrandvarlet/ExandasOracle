
CREATE DOMAIN BOOLEAN AS SMALLINT DEFAULT 0
CHECK (VALUE BETWEEN 0 AND 1);

CREATE TABLE connection_params
(
    uid char(36) not null,
    name varchar(64) not null,
    username varchar(128) not null,
    password varchar(1024) not null,
    host varchar(255) not null,
    port integer not null,
    sid varchar(16),
    service varchar(128),
    dbaviews boolean not null,
    CONSTRAINT pk_connection_params PRIMARY KEY(uid),
    CONSTRAINT connection_params_ak1 UNIQUE(name)
);
