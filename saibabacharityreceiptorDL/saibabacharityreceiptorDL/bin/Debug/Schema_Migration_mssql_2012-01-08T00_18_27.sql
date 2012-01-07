-- add column for field <MerchandiseItem>k__BackingField
ALTER TABLE [receipt] ADD [<_mrchnds_tm>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <ReceiptType>k__BackingField
ALTER TABLE [receipt] ADD [<_rcipt_type>k___backing_field] INT NULL
go
UPDATE [receipt] SET [<_rcipt_type>k___backing_field] = 0
go
ALTER TABLE [receipt] ALTER COLUMN [<_rcipt_type>k___backing_field] INT NOT NULL
go

-- add column for field <Value>k__BackingField
ALTER TABLE [receipt] ADD [<_value>k___backing_field] VARCHAR(255) NULL
go

