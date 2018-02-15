CREATE TABLE [dbo].[AgencyCustomer]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_AgencyCustomer_Id] DEFAULT (newid()),
[AgencyId] [uniqueidentifier] NOT NULL,
[CustomerId] [uniqueidentifier] NOT NULL
)
GO
ALTER TABLE [dbo].[AgencyCustomer] ADD CONSTRAINT [PK_AgencyCustomer] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[AgencyCustomer] ADD CONSTRAINT [FK_AgencyCustomer_Agency_Id] FOREIGN KEY ([AgencyId]) REFERENCES [dbo].[Agency] ([Id])
GO
ALTER TABLE [dbo].[AgencyCustomer] ADD CONSTRAINT [FK_AgencyCustomer_Customer_Id] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([Id])
GO
