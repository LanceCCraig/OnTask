CREATE TABLE [dbo].[User]
(
    [UserId]    INT             IDENTITY(1, 1)  NOT NULL,
    [Email]     NVARCHAR(500)   NOT NULL,
    [FirstName] NVARCHAR(100)   NOT NULL,
    [LastName]  NVARCHAR(100)   NOT NULL,
    [CreatedOn] DATETIME        NOT NULL,
    [UpdatedOn] DATETIME        NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([UserId] ASC)
)
GO
