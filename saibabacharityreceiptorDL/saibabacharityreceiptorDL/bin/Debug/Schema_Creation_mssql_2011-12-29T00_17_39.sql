-- saibabacharityreceiptorDL.Receiptor
CREATE TABLE [receiptor] (
    [receiptor_id] INT NOT NULL,            -- <internal-pk>
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_receiptor] PRIMARY KEY ([receiptor_id])
)
go

-- OpenAccessRuntime.Relational.sql.HighLowRelationalKeyGenerator
CREATE TABLE [voa_keygen] (
    [table_name] VARCHAR(64) NOT NULL,
    [last_used_id] INT NOT NULL,
    CONSTRAINT [pk_voa_keygen] PRIMARY KEY ([table_name])
)
go

