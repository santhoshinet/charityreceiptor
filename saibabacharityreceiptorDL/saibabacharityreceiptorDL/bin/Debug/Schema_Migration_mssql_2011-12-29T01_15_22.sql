-- add column for field <DonationReceiver>k__BackingField
ALTER TABLE [receiptor] ADD [donation_receivers_id] INT NULL
go

-- add column for field <ModeOfPayment>k__BackingField
ALTER TABLE [receiptor] ADD [<_md_f_pymnt>k___backing_field] VARCHAR(255) NULL
go

