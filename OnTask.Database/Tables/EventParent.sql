CREATE TABLE [dbo].[EventParent]
(
    [EventParentId] INT             IDENTITY(1, 1)  NOT NULL,
    [UserId]        INT             NOT NULL,
    [Name]          NVARCHAR(500)   NOT NULL,
    [DisplayName]   NVARCHAR(500)   NOT NULL,
    [CreatedOn]     DATETIME        NOT NULL,
    [UpdatedOn]     DATETIME        NULL,
    CONSTRAINT [PK_EventParent] PRIMARY KEY ([EventParentId] ASC),
    CONSTRAINT [FK_EventParent_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]
)
GO
