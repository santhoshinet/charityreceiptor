-- saibabacharityreceiptorDL.UserLog
CREATE TABLE [user_log] (
    [user_log_id] INT NOT NULL,             -- <internal-pk>
    [donation_receivers_id] INT NULL,       -- <DonationReceiver>k__BackingField
    [<_n_dte_time>k___backing_field] DATETIME NOT NULL, -- <OnDateTime>k__BackingField
    [<_trnsctn_typ>k___bcking_field] VARCHAR(255) NULL, -- <TransactionType>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_user_log] PRIMARY KEY ([user_log_id])
)
go

