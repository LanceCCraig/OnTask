CREATE TABLE [dbo].[EventType]
(
    [EventTypeId]   INT             IDENTITY(1, 1)  NOT NULL,
    [UserId]        INT             NOT NULL,
    [Name]          NVARCHAR(500)   NOT NULL,
    [DisplayName]   NVARCHAR(500)   NOT NULL,
    [CreatedOn]     DATETIME        NOT NULL,
    [UpdatedOn]     DATETIME        NULL,
    CONSTRAINT [PK_EventType] PRIMARY KEY ([EventTypeId] ASC),
    CONSTRAINT [FK_EventType_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]
)
GO
