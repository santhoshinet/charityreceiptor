-- System.Collections.Generic.IList`1[[System.DateTime, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] saibabacharityreceiptorDL.Receipt.<RecurringDates>k__BackingField
CREATE TABLE [rcpt_<_rcrrng_dts>k___bckng_fl] (
    [receipt_id] INT NOT NULL,
    [seq] INT NOT NULL,                     -- <sequence>
    [val] DATETIME NULL,                    -- <value>
    CONSTRAINT [pk_rcpt_<_rcrrng_dts>k___bckng] PRIMARY KEY ([receipt_id], [seq])
)
go

-- saibabacharityreceiptorDL.RecurringDetails
CREATE TABLE [recurring_details] (
    [recurring_details_id] INT NOT NULL,    -- <internal-pk>
    [<_rcrrng_dte>k___backing_field] DATETIME NOT NULL, -- <RecurringDate>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_recurring_details] PRIMARY KEY ([recurring_details_id])
)
go

ALTER TABLE [rcpt_<_rcrrng_dts>k___bckng_fl] ADD CONSTRAINT [ref_rcpt_<_rcrrng_dts>k___bckn] FOREIGN KEY ([receipt_id]) REFERENCES [receipt]([receipt_id])
go

