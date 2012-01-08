-- add column for field <DonationReceiver>k__BackingField
ALTER TABLE [receipt] ADD [usr_id] INT NULL
go

-- add column for field <OnDateTime>k__BackingField
ALTER TABLE [receipt] ADD [<_n_dte_time>k___backing_field] DATETIME NULL
go
UPDATE [receipt] SET [<_n_dte_time>k___backing_field] = getdate()
go
ALTER TABLE [receipt] ALTER COLUMN [<_n_dte_time>k___backing_field] DATETIME NOT NULL
go

-- dropping unknown column [user_log_id]
ALTER TABLE [receipt] DROP COLUMN [user_log_id]
go

