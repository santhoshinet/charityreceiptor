-- System.Collections.Generic.IList`1[[System.DateTime, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]] saibabacharityreceiptorDL.Receipt.<RecurringDates>k__BackingField
CREATE TABLE [rcpt_<_rcrrng_dts>k___bckng_fl] (
    [receipt_id] INT NOT NULL,
    [seq] INT NOT NULL,                     -- <sequence>
    [val] DATETIME NULL,                    -- <value>
    CONSTRAINT [pk_rcpt_<_rcrrng_dts>k___bckng] PRIMARY KEY ([receipt_id], [seq])
)
go

-- saibabacharityreceiptorDL.Receipt
CREATE TABLE [receipt] (
    [receipt_id] INT NOT NULL,              -- <internal-pk>
    [<_address2>k___backing_field] VARCHAR(255) NULL, -- <Address2>k__BackingField
    [<_address>k___backing_field] VARCHAR(255) NULL, -- <Address>k__BackingField
    [<_city>k___backing_field] VARCHAR(255) NULL, -- <City>k__BackingField
    [<_contact>k___backing_field] VARCHAR(255) NULL, -- <Contact>k__BackingField
    [<_dt_rceived>k___backing_field] DATETIME NOT NULL, -- <DateReceived>k__BackingField
    [<_dntn_mount>k___backing_field] VARCHAR(255) NULL, -- <DonationAmount>k__BackingField
    [<_dntn_mntn_wrds>k___bckng_fld] VARCHAR(255) NULL, -- <DonationAmountinWords>k__BackingField
    [usr_id] INT NULL,                      -- <DonationReceiver>k__BackingField
    [<_email>k___backing_field] VARCHAR(255) NULL, -- <Email>k__BackingField
    [<_first_name>k___backing_field] VARCHAR(255) NULL, -- <FirstName>k__BackingField
    [<_fmv_value>k___backing_field] VARCHAR(255) NULL, -- <FmvValue>k__BackingField
    [<_group_id>k___backing_field] VARCHAR(255) NULL, -- <GroupId>k__BackingField
    [<_hrs_served>k___backing_field] INT NOT NULL, -- <HoursServed>k__BackingField
    [<_ssued_date>k___backing_field] DATETIME NOT NULL, -- <IssuedDate>k__BackingField
    [<_last_name>k___backing_field] VARCHAR(255) NULL, -- <LastName>k__BackingField
    [<_mrchnds_tm>k___backing_field] VARCHAR(255) NULL, -- <MerchandiseItem>k__BackingField
    [<_mi>k___backing_field] VARCHAR(255) NULL, -- <Mi>k__BackingField
    [<_md_f_pymnt>k___backing_field] INT NOT NULL, -- <ModeOfPayment>k__BackingField
    [<_quantity>k___backing_field] VARCHAR(255) NULL, -- <Quantity>k__BackingField
    [<_rt_pr_hr_r_dy>k___bckng_feld] VARCHAR(255) NULL, -- <RatePerHrOrDay>k__BackingField
    [<_rcpt_nmber>k___backing_field] VARCHAR(255) NULL, -- <ReceiptNumber>k__BackingField
    [<_rcipt_type>k___backing_field] INT NOT NULL, -- <ReceiptType>k__BackingField
    [<_srvce_type>k___backing_field] VARCHAR(255) NULL, -- <ServiceType>k__BackingField
    [<_state>k___backing_field] VARCHAR(255) NULL, -- <State>k__BackingField
    [<_zip_code>k___backing_field] VARCHAR(255) NULL, -- <ZipCode>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_receipt] PRIMARY KEY ([receipt_id])
)
go

-- saibabacharityreceiptorDL.User
CREATE TABLE [usr] (
    [usr_id] INT NOT NULL,                  -- <internal-pk>
    [<_email>k___backing_field] VARCHAR(255) NULL, -- <Email>k__BackingField
    [<_failcount>k___backing_field] INT NOT NULL, -- <Failcount>k__BackingField
    [<_id>k___backing_field] VARCHAR(255) NULL, -- <Id>k__BackingField
    [<_ishe_admin>k___backing_field] tinyint NOT NULL, -- <IsheAdmin>k__BackingField
    [<_sh_dntn_rcvr>k___bckng_field] tinyint NOT NULL, -- <IsheDonationReceiver>k__BackingField
    [<_lsttrdtime>k___backing_field] DATETIME NOT NULL, -- <Lasttriedtime>k__BackingField
    [<_username>k___backing_field] VARCHAR(255) NULL, -- <Username>k__BackingField
    [voa_version] SMALLINT NOT NULL,        -- <internal-version>
    CONSTRAINT [pk_usr] PRIMARY KEY ([usr_id])
)
go

-- OpenAccessRuntime.Relational.sql.HighLowRelationalKeyGenerator
CREATE TABLE [voa_keygen] (
    [table_name] VARCHAR(64) NOT NULL,
    [last_used_id] INT NOT NULL,
    CONSTRAINT [pk_voa_keygen] PRIMARY KEY ([table_name])
)
go

ALTER TABLE [rcpt_<_rcrrng_dts>k___bckng_fl] ADD CONSTRAINT [ref_rcpt_<_rcrrng_dts>k___bckn] FOREIGN KEY ([receipt_id]) REFERENCES [receipt]([receipt_id])
go

