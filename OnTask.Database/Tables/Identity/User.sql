CREATE TABLE [dbo].[User]
(
	[Id]					NVARCHAR(450)		NOT NULL,
	[AccessFailedCount]		INT					NOT NULL,
	[ConcurrencyStamp]		NVARCHAR(MAX)		NULL,
	[Email]					NVARCHAR(256)		NULL,
	[EmailConfirmed]		BIT					NOT NULL,
	[LockoutEnabled]		BIT					NOT NULL,
	[LockoutEnd]			DATETIMEOFFSET(7)	NULL,
	[NormalizedEmail]		NVARCHAR(256)		NULL,
	[NormalizedUserName]	NVARCHAR(256)		NULL,
	[PasswordHash]			NVARCHAR(MAX)		NULL,
	[PhoneNumber]			NVARCHAR(MAX)		NULL,
	[PhoneNumberConfirmed]	BIT					NOT NULL,
	[SecurityStamp]			NVARCHAR(MAX)		NULL,
	[TwoFactorEnabled]		BIT					NOT NULL,
	[UserName]				NVARCHAR(256)		NULL,
	CONSTRAINT [PK_User] PRIMARY KEY ([Id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX_User_NormalizedEmail]
	ON [dbo].[User] ([NormalizedEmail] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_User_NormalizedUserName]
	ON [dbo].[User] ([NormalizedUserName] ASC)
GO
