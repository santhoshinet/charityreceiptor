-- System.Collections.Generic.IList`1[[System.DateTime, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] saibabacharityreceiptorDL.Receipt.<RecurringDates>k__BackingField
CREATE TABLE [rcpt_<_rcrrng_dts>k___bckng_fl] (
    [receipt_id] INT NOT NULL,
    [seq] INT NOT NULL,                     -- <sequence>
    [val] DATETIME NULL,                    -- <value>
    CONSTRAINT [pk_rcpt_<_rcrrng_dts>k___bckng] PRIMARY KEY ([receipt_id], [seq])
)
go

-- add column for field <GroupId>k__BackingField
ALTER TABLE [receipt] ADD [<_group_id>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <HoursServed>k__BackingField
ALTER TABLE [receipt] ADD [<_hrs_served>k___backing_field] INT NULL
go
UPDATE [receipt] SET [<_hrs_served>k___backing_field] = 0
go
ALTER TABLE [receipt] ALTER COLUMN [<_hrs_served>k___backing_field] INT NOT NULL
go

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

ALTER TABLE [rcpt_<_rcrrng_dts>k___bckng_fl] ADD CONSTRAINT [ref_rcpt_<_rcrrng_dts>k___bckn] FOREIGN KEY ([receipt_id]) REFERENCES [receipt]([receipt_id])
go

