CREATE TABLE [dbo].[UserPassword]
(
    [UserPasswordId]    INT             IDENTITY(1, 1)  NOT NULL,
    [UserId]            INT             NOT NULL,
    [PasswordHash]      NVARCHAR(60)    NOT NULL,
    [CreatedOn]         DATETIME        NOT NULL,
    [UpdatedOn]         DATETIME        NULL,
    CONSTRAINT [PK_UserPassword] PRIMARY KEY ([UserPasswordId] ASC),
    CONSTRAINT [FK_UserPassword_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]
)
GO
