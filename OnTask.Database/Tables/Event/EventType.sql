CREATE TABLE [dbo].[EventType]
(
    [Id]			INT             IDENTITY(1, 1)  NOT NULL,
    [EventGroupId]  INT             NOT NULL,
    [EventParentId]	INT				NOT NULL,
    [UserId]        NVARCHAR(450)	NOT NULL,
    [Name]          NVARCHAR(500)   NOT NULL,
    [Description]   NVARCHAR(MAX)   NULL,
    [CreatedOn]     DATETIME        NOT NULL,
    [UpdatedOn]     DATETIME        NULL,
    CONSTRAINT [PK_EventType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EventType_EventGroup_EventGroupId] FOREIGN KEY ([EventGroupId]) REFERENCES [dbo].[EventGroup] ([Id]),
    CONSTRAINT [FK_EventType_EventParent_EventParentId] FOREIGN KEY ([EventParentId]) REFERENCES [dbo].[EventParent] ([Id]),
    CONSTRAINT [FK_EventType_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_EventType_EventGroupId]
    ON [dbo].[EventType] ([EventGroupId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_EventType_EventParentId]
    ON [dbo].[EventType] ([EventParentId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_EventType_UserId]
    ON [dbo].[EventType] ([UserId] ASC)
GO
