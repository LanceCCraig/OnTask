CREATE TABLE [dbo].[RoleClaim]
(
	[Id]			INT				IDENTITY(1, 1) NOT NULL,
	[ClaimType]		NVARCHAR(MAX)	NULL,
	[ClaimValue]	NVARCHAR(MAX)	NULL,
	[RoleId]		NVARCHAR(450)	NOT NULL,
	CONSTRAINT [PK_RoleClaim] PRIMARY KEY ([Id] ASC),
	CONSTRAINT [FK_RoleClaim_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_RoleClaim_RoleId]
	ON [dbo].[RoleClaim] ([RoleId] ASC)
GO
