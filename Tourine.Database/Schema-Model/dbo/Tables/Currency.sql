CREATE TABLE [dbo].[Currency]
(
[Id] [int] NOT NULL,
[Name] [nvarchar] (50) NOT NULL,
[Factor] [float] NULL
)
GO
ALTER TABLE [dbo].[Currency] ADD CONSTRAINT [PK_Currency_Id] PRIMARY KEY CLUSTERED  ([Id])
GO
