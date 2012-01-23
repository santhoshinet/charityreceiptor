-- add column for field <FmvValue>k__BackingField
ALTER TABLE [receipt] ADD [<_fmv_value>k___backing_field] VARCHAR(255) NULL
go

-- add column for field <RatePerHrOrDay>k__BackingField
ALTER TABLE [receipt] ADD [<_rt_pr_hr_r_dy>k___bckng_feld] VARCHAR(255) NULL
go

-- add column for field <ServiceType>k__BackingField
ALTER TABLE [receipt] ADD [<_srvce_type>k___backing_field] VARCHAR(255) NULL
go

-- dropping unknown column [<_value>k___backing_field]
ALTER TABLE [receipt] DROP COLUMN [<_value>k___backing_field]
go

