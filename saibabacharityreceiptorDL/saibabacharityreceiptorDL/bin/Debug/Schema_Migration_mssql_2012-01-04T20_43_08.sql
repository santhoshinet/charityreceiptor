-- add column for field <Lasttriedtime>k__BackingField
ALTER TABLE [user_log_on_failure] ADD [<_lsttrdtime>k___backing_field] DATETIME NULL
go
UPDATE [user_log_on_failure] SET [<_lsttrdtime>k___backing_field] = getdate()
go
ALTER TABLE [user_log_on_failure] ALTER COLUMN [<_lsttrdtime>k___backing_field] DATETIME NOT NULL
go

