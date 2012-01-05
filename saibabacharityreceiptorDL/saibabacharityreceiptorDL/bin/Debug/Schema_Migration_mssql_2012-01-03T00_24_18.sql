-- modify column for field <ModeOfPayment>k__BackingField
UPDATE [receiptor]
   SET [<_md_f_pymnt>k___backing_field] = 0 -- Add your own default value here, for when [<_md_f_pymnt>k___backing_field] is null.
 WHERE [<_md_f_pymnt>k___backing_field] IS NULL
go
ALTER TABLE [receiptor] ALTER COLUMN [<_md_f_pymnt>k___backing_field] INT NOT NULL
go

