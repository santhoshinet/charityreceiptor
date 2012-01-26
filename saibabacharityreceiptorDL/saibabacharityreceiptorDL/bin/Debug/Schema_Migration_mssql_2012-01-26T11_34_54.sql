-- add column for field <City>k__BackingField
ALTER TABLE [receipt] ADD [<_city>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <FirstName>k__BackingField
ALTER TABLE [receipt] ADD [<_first_name>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <FmvValue>k__BackingField
ALTER TABLE [receipt] ADD [<_fmv_value>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <IssuedDate>k__BackingField
ALTER TABLE [receipt] ADD [<_ssued_date>k___backing_field] DATETIME NULL
go
UPDATE [receipt] SET [<_ssued_date>k___backing_field] = getdate()
go
ALTER TABLE [receipt] ALTER COLUMN [<_ssued_date>k___backing_field] DATETIME NOT NULL
go

-- add column for field <LastName>k__BackingField
ALTER TABLE [receipt] ADD [<_last_name>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <Mi>k__BackingField
ALTER TABLE [receipt] ADD [<_mi>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <Quantity>k__BackingField
ALTER TABLE [receipt] ADD [<_quantity>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <RatePerHrOrDay>k__BackingField
ALTER TABLE [receipt] ADD [<_rt_pr_hr_r_dy>k___bckng_feld] VARCHAR(255) NULL
go

-- add column for field <ServiceType>k__BackingField
ALTER TABLE [receipt] ADD [<_srvce_type>k___backing_field] VARCHAR(255) NULL
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

-- dropping unknown column [<_value>k___backing_field]
ALTER TABLE [receipt] DROP COLUMN [<_value>k___backing_field]
go

