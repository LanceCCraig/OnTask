CREATE TABLE [dbo].[Event]
(
    [Id]			INT             IDENTITY(1, 1)  NOT NULL,
    [EventGroupId]  INT             NOT NULL,
    [EventParentId] INT             NOT NULL,
    [EventTypeId]   INT             NOT NULL,
    [UserId]        NVARCHAR(450)   NOT NULL,
    [Name]          NVARCHAR(500)   NOT NULL,
    [Description]   NVARCHAR(MAX)   NULL,
    [StartDate]     DATE            NOT NULL,
    [StartTime]     TIME            NULL,
    [EndDate]       DATE            NOT NULL,
    [EndTime]       TIME            NULL,
    [IsAllDay]      BIT             NOT NULL,
    [Weight]        INT             NULL,
    [CreatedOn]     DATETIME        NOT NULL,
    [UpdatedOn]     DATETIME        NULL,
    CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Event_EventGroup_EventGroupId] FOREIGN KEY ([EventGroupId]) REFERENCES [dbo].[EventGroup] ([Id]),
    CONSTRAINT [FK_Event_EventParent_EventParentId] FOREIGN KEY ([EventParentId]) REFERENCES [dbo].[EventParent] ([Id]),
    CONSTRAINT [FK_Event_EventType_EventTypeId] FOREIGN KEY ([EventTypeId]) REFERENCES [dbo].[EventType] ([Id]),
    CONSTRAINT [FK_Event_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_Event_EventGroupId]
    ON [dbo].[Event] ([EventGroupId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_Event_EventParentId]
    ON [dbo].[Event] ([EventParentId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_Event_EventTypeId]
    ON [dbo].[Event] ([EventTypeId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_Event_UserId]
    ON [dbo].[Event] ([UserId] ASC)
GO
