-- add column for field <HoursServed>k__BackingField
ALTER TABLE [receipt] ADD [<_hrs_served>k___backing_field] INT NULL
go
UPDATE [receipt] SET [<_hrs_served>k___backing_field] = 0
go
ALTER TABLE [receipt] ALTER COLUMN [<_hrs_served>k___backing_field] INT NOT NULL
go

