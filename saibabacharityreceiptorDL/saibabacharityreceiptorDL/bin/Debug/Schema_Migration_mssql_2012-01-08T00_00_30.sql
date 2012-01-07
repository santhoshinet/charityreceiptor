-- saibabacharityreceiptorDL.Receipt
CREATE TABLE [receipt] (
    [receipt_id] INT NOT NULL,              -- <internal-pk>
    [<_address>k___backing_field] VARCHAR(255) NULL, -- <Address>k__BackingField
    [<_contact>k___backing_field] VARCHAR(255) NULL, -- <Contact>k__BackingField
    [<_dntn_mount>k___backing_field] VARCHAR(255) NULL, -- <DonationAmount>k__BackingField
    [<_dntn_mntn_wrds>k___bckng_fld] VARCHAR(255) NULL, -- <DonationAmountinWords>k__BackingField
    [usr_id] INT NULL,                      -- <DonationReceiver>k__BackingField
    [<_email>k___backing_field] VARCHAR(255) NULL, -- <Email>k__BackingField
    [<_md_f_pymnt>k___backing_field] INT NOT NULL, -- <ModeOfPayment>k__BackingField
    [<_name>k___backing_field] VARCHAR(255) NULL, -- <Name>k__BackingField
    [<_n_dte_time>k___backing_field] DATETIME NOT NULL, -- <OnDateTime>k__BackingField
    [<_rcpt_nmber>k___backing_field] VARCHAR(255) NULL, -- <ReceiptNumber>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_receipt] PRIMARY KEY ([receipt_id])
)
go

