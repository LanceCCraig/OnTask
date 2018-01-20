CREATE TABLE [dbo].[UserClaim]
(
	[Id]			INT				IDENTITY(1, 1) NOT NULL,
	[ClaimType]		NVARCHAR(MAX)	NULL,
	[ClaimValue]	NVARCHAR(MAX)	NULL,
	[UserId]		NVARCHAR(450)	NOT NULL,
	CONSTRAINT [PK_UserClaim] PRIMARY KEY ([Id] ASC),
	CONSTRAINT [FK_UserClaim_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_UserClaim_UserId]
	ON [dbo].[UserClaim] ([UserId] ASC)
GO
