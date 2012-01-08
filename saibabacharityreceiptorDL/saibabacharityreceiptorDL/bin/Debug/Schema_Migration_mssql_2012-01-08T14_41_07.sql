-- modify column for field <DateReceived>k__BackingField
UPDATE [receipt]
   SET [<_dt_rceived>k___backing_field] = getdate() -- Add your own default value here, for when [<_dt_rceived>k___backing_field] is null.
 WHERE [<_dt_rceived>k___backing_field] IS NULL
go
ALTER TABLE [receipt] ALTER COLUMN [<_dt_rceived>k___backing_field] DATETIME NOT NULL
go

