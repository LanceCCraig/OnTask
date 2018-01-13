CREATE TABLE [dbo].[Event]
(
    [EventId]       INT             IDENTITY(1, 1)  NOT NULL,
    [EventParentId] INT             NOT NULL,
    [EventTypeId]   INT             NOT NULL,
    [UserId]        INT             NOT NULL,
    [Name]          NVARCHAR(500)   NOT NULL,
    [Description]   NVARCHAR(MAX)   NULL,
    [StartDate]     DATETIME        NOT NULL,
    [EndDate]       DATETIME        NULL,
    [CreatedOn]     DATETIME        NOT NULL,
    [UpdatedOn]     DATETIME        NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY ([EventId] ASC),
    CONSTRAINT [FK_Event_EventParent] FOREIGN KEY ([EventParentId]) REFERENCES [dbo].[EventParent],
    CONSTRAINT [FK_Event_EventType] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[EventType],
    CONSTRAINT [FK_Event_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]
)
GO
