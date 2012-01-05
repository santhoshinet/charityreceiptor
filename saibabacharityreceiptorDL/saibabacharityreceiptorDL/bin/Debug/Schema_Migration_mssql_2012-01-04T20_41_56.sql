-- saibabacharityreceiptorDL.UserLogOnFailure
CREATE TABLE [user_log_on_failure] (
    [user_log_on_failure_id] INT NOT NULL,  -- <internal-pk>
    [<_failcount>k___backing_field] INT NOT NULL, -- <Failcount>k__BackingField
    [<_username>k___backing_field] VARCHAR(255) NULL, -- <Username>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_user_log_on_failure] PRIMARY KEY ([user_log_on_failure_id])
)
go

