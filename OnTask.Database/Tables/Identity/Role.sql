CREATE TABLE [dbo].[Role]
(
	[Id]				NVARCHAR(450)	NOT NULL,
	[ConcurrencyStamp]	NVARCHAR(MAX)	NULL,
	[Name]				NVARCHAR(256)	NULL,
	[NormalizedName]	NVARCHAR(256)	NULL,
	CONSTRAINT [PK_Role] PRIMARY KEY ([Id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX_Role_NormalizedName]
	ON [dbo].[Role] ([NormalizedName] ASC)
GO
