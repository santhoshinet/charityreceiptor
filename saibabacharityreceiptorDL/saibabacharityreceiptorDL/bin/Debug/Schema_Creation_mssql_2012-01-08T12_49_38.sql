-- saibabacharityreceiptorDL.Receiptor
CREATE TABLE [receiptor] (
    [receiptor_id] INT NOT NULL,            -- <internal-pk>
    [<_address>k___backing_field] VARCHAR(255) NULL, -- <Address>k__BackingField
    [<_contact>k___backing_field] VARCHAR(255) NULL, -- <Contact>k__BackingField
    [<_dt_rceived>k___backing_field] VARCHAR(255) NULL, -- <DateReceived>k__BackingField
    [<_dntn_mount>k___backing_field] VARCHAR(255) NULL, -- <DonationAmount>k__BackingField
    [<_dntn_mntn_wrds>k___bckng_fld] VARCHAR(255) NULL, -- <DonationAmountinWords>k__BackingField
    [<_email>k___backing_field] VARCHAR(255) NULL, -- <Email>k__BackingField
    [<_md_f_pymnt>k___backing_field] INT NOT NULL, -- <ModeOfPayment>k__BackingField
    [<_name>k___backing_field] VARCHAR(255) NULL, -- <Name>k__BackingField
    [<_rcpt_nmber>k___backing_field] VARCHAR(255) NULL, -- <ReceiptNumber>k__BackingField
    [user_log_id] INT NULL,                 -- <UserLog>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_receiptor] PRIMARY KEY ([receiptor_id])
)
go

-- saibabacharityreceiptorDL.UserLog
CREATE TABLE [user_log] (
    [user_log_id] INT NOT NULL,             -- <internal-pk>
    [usr_id] INT NULL,                      -- <DonationReceiver>k__BackingField
    [<_n_dte_time>k___backing_field] DATETIME NOT NULL, -- <OnDateTime>k__BackingField
    [<_trnsctn_typ>k___bcking_field] VARCHAR(255) NULL, -- <TransactionType>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_user_log] PRIMARY KEY ([user_log_id])
)
go

-- saibabacharityreceiptorDL.User
CREATE TABLE [usr] (
    [usr_id] INT NOT NULL,                  -- <internal-pk>
    [<_email>k___backing_field] VARCHAR(255) NULL, -- <Email>k__BackingField
    [<_failcount>k___backing_field] INT NOT NULL, -- <Failcount>k__BackingField
    [<_id>k___backing_field] VARCHAR(255) NULL, -- <Id>k__BackingField
    [<_ishe_admin>k___backing_field] tinyint NOT NULL, -- <IsheAdmin>k__BackingField
    [<_sh_dntn_rcvr>k___bckng_field] tinyint NOT NULL, -- <IsheDonationReceiver>k__BackingField
    [<_lsttrdtime>k___backing_field] DATETIME NOT NULL, -- <Lasttriedtime>k__BackingField
    [<_username>k___backing_field] VARCHAR(255) NULL, -- <Username>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_usr] PRIMARY KEY ([usr_id])
)
go

-- OpenAccessRuntime.Relational.sql.HighLowRelationalKeyGenerator
CREATE TABLE [voa_keygen] (
    [table_name] VARCHAR(64) NOT NULL,
    [last_used_id] INT NOT NULL,
    CONSTRAINT [pk_voa_keygen] PRIMARY KEY ([table_name])
)
go

