-- add column for field <IssuedDate>k__BackingField
ALTER TABLE [receipt] ADD [<_ssued_date>k___backing_field] DATETIME NULL
go
UPDATE [receipt] SET [<_ssued_date>k___backing_field] = getdate()
go
ALTER TABLE [receipt] ALTER COLUMN [<_ssued_date>k___backing_field] DATETIME NOT NULL
go

