-- saibabacharityreceiptorDL.DonationReceivers
CREATE TABLE [donation_receivers] (
    [donation_receivers_id] INT NOT NULL,   -- <internal-pk>
    [<_name>k___backing_field] VARCHAR(255) NULL, -- <Name>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_donation_receivers] PRIMARY KEY ([donation_receivers_id])
)
go

