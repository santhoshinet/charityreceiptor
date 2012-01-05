-- add column for field <UserLog>k__BackingField
ALTER TABLE [receiptor] ADD [user_log_id] INT NULL
go

-- dropping unknown column [donation_receivers_id]
ALTER TABLE [receiptor] DROP COLUMN [donation_receivers_id]
go

-- add column for field <DonationReceiver>k__BackingField
ALTER TABLE [user_log] ADD [usr_id] INT NULL
go

-- dropping unknown column [donation_receivers_id]
ALTER TABLE [user_log] DROP COLUMN [donation_receivers_id]
go

-- saibabacharityreceiptorDL.User
CREATE TABLE [usr] (
    [usr_id] INT NOT NULL,                  -- <internal-pk>
    [<_failcount>k___backing_field] INT NOT NULL, -- <Failcount>k__BackingField
    [<_sh_dntn_rcvr>k___bckng_field] tinyint NOT NULL, -- <IsheDonationReceiver>k__BackingField
    [<_lsttrdtime>k___backing_field] DATETIME NOT NULL, -- <Lasttriedtime>k__BackingField
    [<_username>k___backing_field] VARCHAR(255) NULL, -- <Username>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_usr] PRIMARY KEY ([usr_id])
)
go

