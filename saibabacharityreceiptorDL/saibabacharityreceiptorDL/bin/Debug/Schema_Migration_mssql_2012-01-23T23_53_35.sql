-- add column for field <City>k__BackingField
ALTER TABLE [receipt] ADD [<_city>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <FirstName>k__BackingField
ALTER TABLE [receipt] ADD [<_first_name>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <LastName>k__BackingField
ALTER TABLE [receipt] ADD [<_last_name>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <Mi>k__BackingField
ALTER TABLE [receipt] ADD [<_mi>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <State>k__BackingField
ALTER TABLE [receipt] ADD [<_state>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <ZipCode>k__BackingField
ALTER TABLE [receipt] ADD [<_zip_code>k___backing_field] VARCHAR(255) NULL
go

-- dropping unknown column [<_name>k___backing_field]
ALTER TABLE [receipt] DROP COLUMN [<_name>k___backing_field]
go

