-- add column for field <DateReceived>k__BackingField
ALTER TABLE [receipt] ADD [<_dt_rceived>k___backing_field] DATETIME NULL
go
UPDATE [receipt] SET [<_dt_rceived>k___backing_field] = getdate()
go
ALTER TABLE [receipt] ALTER COLUMN [<_dt_rceived>k___backing_field] DATETIME NOT NULL
go

-- dropping unknown column [<_n_dte_time>k___backing_field]
ALTER TABLE [receipt] DROP COLUMN [<_n_dte_time>k___backing_field]
go

