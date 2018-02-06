CREATE TABLE [dbo].[EventGroup]
(
    [Id]            INT             IDENTITY(1, 1)  NOT NULL,
    [EventParentId] INT             NOT NULL,
    [UserId]        NVARCHAR(450)   NOT NULL,
    [Name]          NVARCHAR(500)   NOT NULL,
    [Description]   NVARCHAR(MAX)   NULL,
    [CreatedOn]     DATETIME        NOT NULL,
    [UpdatedOn]     DATETIME        NULL,
    CONSTRAINT [PK_EventGroup] PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_EventGroup_EventParent_EventParentId] FOREIGN KEY ([EventParentId]) REFERENCES [dbo].[EventParent] ([Id]),
    CONSTRAINT [FK_EventGroup_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_EventGroup_EventParentId]
    ON [dbo].[EventGroup] ([EventParentId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_EventGroup_UserId]
    ON [dbo].[EventGroup] ([UserId] ASC)
GO
