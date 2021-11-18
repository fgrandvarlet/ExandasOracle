
CREATE TABLE src_types
(
    type_name varchar(128) not null,
    typecode varchar(128),
    attributes integer,
    methods integer,
    predefined varchar(3),
    incomplete varchar(3),
    final varchar(3),
    instantiable varchar(3),
    persistable varchar(3),
    supertype_owner varchar(128),
    supertype_name varchar(128),
    local_attributes integer,
    local_methods integer,
    CONSTRAINT pk_src_types PRIMARY KEY (type_name)
);

CREATE TABLE tgt_types
(
    type_name varchar(128) not null,
    typecode varchar(128),
    attributes integer,
    methods integer,
    predefined varchar(3),
    incomplete varchar(3),
    final varchar(3),
    instantiable varchar(3),
    persistable varchar(3),
    supertype_owner varchar(128),
    supertype_name varchar(128),
    local_attributes integer,
    local_methods integer,
    CONSTRAINT pk_tgt_types PRIMARY KEY (type_name)
);
