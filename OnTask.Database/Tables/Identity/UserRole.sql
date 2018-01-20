CREATE TABLE [dbo].[UserRole]
(
	[UserId]	NVARCHAR(450)	NOT NULL,
	[RoleId]	NVARCHAR(450)	NOT NULL,
	CONSTRAINT [PK_UserRole] PRIMARY KEY
	(
		[UserId] ASC,
		[RoleId] ASC
	),
	CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]),
	CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_UserRole_RoleId]
	ON [dbo].[UserRole] ([RoleId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_UserRole_UserId]
	ON [dbo].[UserRole] ([UserId] ASC)
GO
