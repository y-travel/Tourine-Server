CREATE TABLE [dbo].[PriceDetail]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_PriceDetail_ID] DEFAULT (newid()),
[Value] [bigint] NOT NULL,
[CurrencyId] [int] NOT NULL,
[TourId] [uniqueidentifier] NOT NULL,
[Title] [nvarchar] (100) NULL
)
GO
ALTER TABLE [dbo].[PriceDetail] ADD CONSTRAINT [PK_PriceDetail_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
ALTER TABLE [dbo].[PriceDetail] ADD CONSTRAINT [FK_PriceDetail_Currency_Id] FOREIGN KEY ([CurrencyId]) REFERENCES [dbo].[Currency] ([Id])
GO
ALTER TABLE [dbo].[PriceDetail] ADD CONSTRAINT [FK_PriceDetail_Tour_Id] FOREIGN KEY ([TourId]) REFERENCES [dbo].[Tour] ([Id]) ON DELETE CASCADE
GO
