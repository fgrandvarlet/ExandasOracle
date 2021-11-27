
CREATE SEQUENCE filter_setting_seq;

CREATE TABLE filter_setting
(
    id integer not null,
    comparison_set_uid char(36) not null,
    entity varchar(30) not null,
    label_id smallint,
    label varchar(64),
    property varchar(128),
    CONSTRAINT pk_filter_setting PRIMARY KEY(id),
    CONSTRAINT filter_setting_fk1 FOREIGN KEY (comparison_set_uid) REFERENCES comparison_set(uid) ON DELETE CASCADE
);

CREATE INDEX filter_setting_idx1 ON filter_setting(comparison_set_uid);


SET TERM ^ ;

CREATE TRIGGER filter_setting_trg for filter_setting
active before insert position 0
as
begin
    if (new.id is null) then
    begin
        new.id = NEXT VALUE FOR filter_setting_seq;
    end
end^

SET TERM ; ^
