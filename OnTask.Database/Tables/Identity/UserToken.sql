CREATE TABLE [dbo].[UserToken]
(
	[UserId]		NVARCHAR(450)	NOT NULL,
	[LoginProvider]	NVARCHAR(450)	NOT NULL,
	[Name]			NVARCHAR(450)	NOT NULL,
	[Value]			NVARCHAR(MAX)	NULL,
	CONSTRAINT [PK_UserToken] PRIMARY KEY
	(
		[UserId] ASC,
		[LoginProvider] ASC,
		[Name] ASC
	)
)
GO
