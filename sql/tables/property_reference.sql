
CREATE TABLE property_reference
(
    entity varchar(30) not null,
    property varchar(128) not null,
    CONSTRAINT pk_property_reference PRIMARY KEY(entity, property),
    CONSTRAINT property_reference_fk1 FOREIGN KEY (entity) REFERENCES entity_reference(entity)
);
