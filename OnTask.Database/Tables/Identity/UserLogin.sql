CREATE TABLE [dbo].[UserLogin]
(
	[LoginProvider]			NVARCHAR(450)	NOT NULL,
	[ProviderKey]			NVARCHAR(450)	NOT NULL,
	[ProviderDisplayName]	NVARCHAR(MAX)	NULL,
	[UserId]				NVARCHAR(450)	NOT NULL,
	CONSTRAINT [PK_UserLogin] PRIMARY KEY
	(
		[LoginProvider] ASC,
		[ProviderKey] ASC
	),
	CONSTRAINT [FK_UserLogin_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_UserLogin_UserId]
	ON [dbo].[UserLogin] ([UserId] ASC)
GO
