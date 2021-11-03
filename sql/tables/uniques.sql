
CREATE TABLE src_uniques
(
    constraint_name varchar(128) not null,
    -- constraint_type varchar(1),
    table_name varchar(128) not null,
    -- search_condition BLOB SUB_TYPE TEXT,     -- valorisé uniquement pour C et F
    -- search_condition_vc varchar(4000),       -- valorisé uniquement pour C et F
    -- r_owner varchar(128),                    -- valorisé uniquement pour R
    -- r_constraint_name varchar(128),          -- valorisé uniquement pour R
    -- delete_rule varchar(9),                  -- valorisé uniquement pour R
    status varchar(8),
    deferrable varchar(14),
    deferred varchar(9),
    validated varchar(13),
    rely varchar(4),                            -- valorisé uniquement pour U (ajouter P)
    index_owner varchar(128),                   -- valorisé uniquement pour P et U
    index_name varchar(128),                    -- valorisé uniquement pour P et U
    invalid varchar(7),
    view_related varchar(14),
    CONSTRAINT pk_src_uniques PRIMARY KEY (table_name, constraint_name)
);

-- filtrer sur constraint_type = 'U' et generated = 'USER NAME'

CREATE TABLE tgt_uniques
(
    constraint_name varchar(128) not null,
    -- constraint_type varchar(1),
    table_name varchar(128) not null,
    -- search_condition BLOB SUB_TYPE TEXT,     -- valorisé uniquement pour C et F
    -- search_condition_vc varchar(4000),       -- valorisé uniquement pour C et F
    -- r_owner varchar(128),                    -- valorisé uniquement pour R
    -- r_constraint_name varchar(128),          -- valorisé uniquement pour R
    -- delete_rule varchar(9),                  -- valorisé uniquement pour R
    status varchar(8),
    deferrable varchar(14),
    deferred varchar(9),
    validated varchar(13),
    rely varchar(4),                            -- valorisé uniquement pour U (ajouter P)
    index_owner varchar(128),                   -- valorisé uniquement pour P et U
    index_name varchar(128),                    -- valorisé uniquement pour P et U
    invalid varchar(7),
    view_related varchar(14),
    CONSTRAINT pk_tgt_uniques PRIMARY KEY (table_name, constraint_name)
);
