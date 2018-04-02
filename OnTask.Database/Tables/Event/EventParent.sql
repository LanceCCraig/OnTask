CREATE TABLE [dbo].[EventParent]
(
    [Id]			INT             IDENTITY(1, 1)  NOT NULL,
    [UserId]        NVARCHAR(450)	NOT NULL,
    [Name]          NVARCHAR(500)   NOT NULL,
    [Description]   NVARCHAR(MAX)   NULL,
    [Weight]        INT             NULL,
    [CreatedOn]     DATETIME        NULL,
    [UpdatedOn]     DATETIME        NULL,
    CONSTRAINT [PK_EventParent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EventParent_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_EventParent_UserId]
    ON [dbo].[EventParent] ([UserId] ASC)
GO
