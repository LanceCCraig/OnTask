CREATE TABLE [dbo].[EventType]
(
    [Id]			INT             IDENTITY(1, 1)  NOT NULL,
    [UserId]        NVARCHAR(450)	NOT NULL,
    [Name]          NVARCHAR(500)   NOT NULL,
    [DisplayName]   NVARCHAR(500)   NOT NULL,
    [CreatedOn]     DATETIME        NOT NULL,
    [UpdatedOn]     DATETIME        NULL,
    CONSTRAINT [PK_EventType] PRIMARY KEY ([Id] ASC),
    CONSTRAINT [FK_EventType_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_EventType_UserId]
    ON [dbo].[EventType] ([UserId] ASC)
GO
