
CREATE SEQUENCE delta_report_seq;

CREATE TABLE delta_report
(
    id bigint not null,
    comparison_set_uid char(36) not null,
    entity varchar(30) not null,
    object varchar(128) not null,
    parent_object varchar(128),
    label_id smallint not null,
    label varchar(64) not null,
    property varchar(128),
    source BLOB SUB_TYPE TEXT,
    target BLOB SUB_TYPE TEXT,
    CONSTRAINT pk_delta_report PRIMARY KEY(id),
    CONSTRAINT delta_report_fk1 FOREIGN KEY (comparison_set_uid) REFERENCES comparison_set(uid) ON DELETE CASCADE
);

CREATE INDEX delta_report_idx1 ON delta_report(comparison_set_uid);


SET TERM ^ ;

CREATE TRIGGER delta_report_trg for delta_report
active before insert position 0
as
begin
    if (new.id is null) then
    begin
        new.id = NEXT VALUE FOR delta_report_seq;
    end
end^

SET TERM ; ^
