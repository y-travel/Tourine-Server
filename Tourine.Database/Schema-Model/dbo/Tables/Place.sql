CREATE TABLE [dbo].[Place]
(
[Id] [uniqueidentifier] NOT NULL ROWGUIDCOL CONSTRAINT [DF_Place_Id] DEFAULT (newid()),
[Name] [nvarchar] (50) NOT NULL
)
GO
ALTER TABLE [dbo].[Place] ADD CONSTRAINT [PK_Place_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
