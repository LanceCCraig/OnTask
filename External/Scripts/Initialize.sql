/*==================================================================================================
Description:    Initialize the OnTask database and tables.
Created:        13-Jan-2018 Lance Craig
Updated:        
==================================================================================================*/

--Drop/Create Database------------------------------------------------------------------------------
USE [master]
GO

IF EXISTS
(
	SELECT *
	FROM SYS.DATABASES
	WHERE [Name] = 'OnTask'
)
	DROP DATABASE [OnTask]
GO

CREATE DATABASE [OnTask]
GO

--Drop Tables---------------------------------------------------------------------------------------
USE [OnTask]
GO

IF EXISTS
(
    SELECT *
    FROM SYS.OBJECTS
    WHERE
        [Object_Id] = OBJECT_ID(N'[dbo].[Event]') AND
        [Type] = N'U'
)
    DROP TABLE [dbo].[Event]
GO

IF EXISTS
(
    SELECT *
    FROM SYS.OBJECTS
    WHERE
        [Object_Id] = OBJECT_ID(N'[dbo].[EventType]') AND
        [Type] = N'U'
)
    DROP TABLE [dbo].[EventType]
GO

IF EXISTS
(
    SELECT *
    FROM SYS.OBJECTS
    WHERE
        [Object_Id] = OBJECT_ID(N'[dbo].[EventGroup]') AND
        [Type] = N'U'
)
    DROP TABLE [dbo].[EventGroup]
GO

IF EXISTS
(
    SELECT *
    FROM SYS.OBJECTS
    WHERE
        [Object_Id] = OBJECT_ID(N'[dbo].[EventParent]') AND
        [Type] = N'U'
)
    DROP TABLE [dbo].[EventParent]
GO

IF EXISTS
(
    SELECT *
    FROM SYS.OBJECTS
    WHERE
        [Object_Id] = OBJECT_ID(N'[dbo].[RoleClaim]') AND
        [Type] = N'U'
)
    DROP TABLE [dbo].[RoleClaim]
GO

IF EXISTS
(
    SELECT *
    FROM SYS.OBJECTS
    WHERE
        [Object_Id] = OBJECT_ID(N'[dbo].[UserClaim]') AND
        [Type] = N'U'
)
    DROP TABLE [dbo].[UserClaim]
GO  

IF EXISTS
(
    SELECT *
    FROM SYS.OBJECTS
    WHERE
        [Object_Id] = OBJECT_ID(N'[dbo].[UserLogin]') AND
        [Type] = N'U'
)
    DROP TABLE [dbo].[UserLogin]
GO

IF EXISTS
(
    SELECT *
    FROM SYS.OBJECTS
    WHERE
        [Object_Id] = OBJECT_ID(N'[dbo].[UserRole]') AND
        [Type] = N'U'
)
    DROP TABLE [dbo].[UserRole]
GO

IF EXISTS
(
    SELECT *
    FROM SYS.OBJECTS
    WHERE
        [Object_Id] = OBJECT_ID(N'[dbo].[UserToken]') AND
        [Type] = N'U'
)
    DROP TABLE [dbo].[UserToken]
GO

IF EXISTS
(
    SELECT *
    FROM SYS.OBJECTS
    WHERE
        [Object_Id] = OBJECT_ID(N'[dbo].[Role]') AND
        [Type] = N'U'
)
    DROP TABLE [dbo].[Role]
GO

IF EXISTS
(
    SELECT *
    FROM SYS.OBJECTS
    WHERE
        [Object_Id] = OBJECT_ID(N'[dbo].[User]') AND
        [Type] = N'U'
)
    DROP TABLE [dbo].[User]
GO

--Create Tables-------------------------------------------------------------------------------------
USE [OnTask]
GO

CREATE TABLE [dbo].[Role]
(
	[Id]				NVARCHAR(450)	NOT NULL,
	[ConcurrencyStamp]	NVARCHAR(MAX)	NULL,
	[Name]				NVARCHAR(256)	NULL,
	[NormalizedName]	NVARCHAR(256)	NULL,
	CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX_Role_NormalizedName]
	ON [dbo].[Role] ([NormalizedName] ASC)
GO

CREATE TABLE [dbo].[User]
(
	[Id]					NVARCHAR(450)		NOT NULL,
	[AccessFailedCount]		INT					NOT NULL,
	[ConcurrencyStamp]		NVARCHAR(MAX)		NULL,
	[Email]					NVARCHAR(256)		NULL,
	[EmailConfirmed]		BIT					NOT NULL,
	[LockoutEnabled]		BIT					NOT NULL,
	[LockoutEnd]			DATETIMEOFFSET(7)	NULL,
	[NormalizedEmail]		NVARCHAR(256)		NULL,
	[NormalizedUserName]	NVARCHAR(256)		NULL,
	[PasswordHash]			NVARCHAR(MAX)		NULL,
	[PhoneNumber]			NVARCHAR(MAX)		NULL,
	[PhoneNumberConfirmed]	BIT					NOT NULL,
	[SecurityStamp]			NVARCHAR(MAX)		NULL,
	[TwoFactorEnabled]		BIT					NOT NULL,
	[UserName]				NVARCHAR(256)		NULL,
	CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED ([Id] ASC)
)
GO

CREATE NONCLUSTERED INDEX [IX_User_NormalizedEmail]
	ON [dbo].[User] ([NormalizedEmail] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_User_NormalizedUserName]
	ON [dbo].[User] ([NormalizedUserName] ASC)
GO

CREATE TABLE [dbo].[RoleClaim]
(
	[Id]			INT				IDENTITY(1, 1) NOT NULL,
	[ClaimType]		NVARCHAR(MAX)	NULL,
	[ClaimValue]	NVARCHAR(MAX)	NULL,
	[RoleId]		NVARCHAR(450)	NOT NULL,
	CONSTRAINT [PK_RoleClaim] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_RoleClaim_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_RoleClaim_RoleId]
	ON [dbo].[RoleClaim] ([RoleId] ASC)
GO

CREATE TABLE [dbo].[UserClaim]
(
	[Id]			INT				IDENTITY(1, 1) NOT NULL,
	[ClaimType]		NVARCHAR(MAX)	NULL,
	[ClaimValue]	NVARCHAR(MAX)	NULL,
	[UserId]		NVARCHAR(450)	NOT NULL,
	CONSTRAINT [PK_UserClaim] PRIMARY KEY CLUSTERED ([Id] ASC),
	CONSTRAINT [FK_UserClaim_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_UserClaim_UserId]
	ON [dbo].[UserClaim] ([UserId] ASC)
GO

CREATE TABLE [dbo].[UserLogin]
(
	[LoginProvider]			NVARCHAR(450)	NOT NULL,
	[ProviderKey]			NVARCHAR(450)	NOT NULL,
	[ProviderDisplayName]	NVARCHAR(MAX)	NULL,
	[UserId]				NVARCHAR(450)	NOT NULL,
	CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED
	(
		[LoginProvider] ASC,
		[ProviderKey] ASC
	),
	CONSTRAINT [FK_UserLogin_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_UserLogin_UserId]
	ON [dbo].[UserLogin] ([UserId] ASC)
GO

CREATE TABLE [dbo].[UserRole]
(
	[UserId]	NVARCHAR(450)	NOT NULL,
	[RoleId]	NVARCHAR(450)	NOT NULL,
	CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED
	(
		[UserId] ASC,
		[RoleId] ASC
	),
	CONSTRAINT [FK_UserRole_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Role] ([Id]),
	CONSTRAINT [FK_UserRole_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_UserRole_RoleId]
	ON [dbo].[UserRole] ([RoleId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_UserRole_UserId]
	ON [dbo].[UserRole] ([UserId] ASC)
GO

CREATE TABLE [dbo].[UserToken]
(
	[UserId]		NVARCHAR(450)	NOT NULL,
	[LoginProvider]	NVARCHAR(450)	NOT NULL,
	[Name]			NVARCHAR(450)	NOT NULL,
	[Value]			NVARCHAR(MAX)	NULL,
	CONSTRAINT [PK_UserToken] PRIMARY KEY CLUSTERED
	(
		[UserId] ASC,
		[LoginProvider] ASC,
		[Name] ASC
	)
)
GO

CREATE TABLE [dbo].[EventParent]
(
    [Id]			INT             IDENTITY(1, 1)  NOT NULL,
    [UserId]        NVARCHAR(450)	NOT NULL,
    [Name]          NVARCHAR(500)   NOT NULL,
    [Description]   NVARCHAR(MAX)   NOT NULL,
    [CreatedOn]     DATETIME        NULL,
    [UpdatedOn]     DATETIME        NULL,
    CONSTRAINT [PK_EventParent] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EventParent_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_EventParent_UserId]
    ON [dbo].[EventParent] ([UserId] ASC)
GO

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

CREATE TABLE [dbo].[EventType]
(
    [Id]			INT             IDENTITY(1, 1)  NOT NULL,
    [EventGroupId]  INT             NOT NULL,
    [UserId]        NVARCHAR(450)	NOT NULL,
    [Name]          NVARCHAR(500)   NOT NULL,
    [Description]   NVARCHAR(MAX)   NULL,
    [CreatedOn]     DATETIME        NOT NULL,
    [UpdatedOn]     DATETIME        NULL,
    CONSTRAINT [PK_EventType] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_EventType_EventGroup_EventGroupId] FOREIGN KEY ([EventGroupId]) REFERENCES [dbo].[EventGroup] ([Id]),
    CONSTRAINT [FK_EventType_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id])
)
GO

CREATE NONCLUSTERED INDEX [IX_EventType_EventGroupId]
    ON [dbo].[EventType] ([EventGroupId] ASC)
GO

CREATE NONCLUSTERED INDEX [IX_EventType_UserId]
    ON [dbo].[EventType] ([UserId] ASC)
GO

CREATE TABLE [dbo].[Event]
(
    [Id]			INT             IDENTITY(1, 1)  NOT NULL,
    [EventGroupId]  INT             NOT NULL,
    [EventParentId] INT             NOT NULL,
    [EventTypeId]   INT             NOT NULL,
    [UserId]        NVARCHAR(450)   NOT NULL,
    [Name]          NVARCHAR(500)   NOT NULL,
    [Description]   NVARCHAR(MAX)   NULL,
    [StartDate]     DATETIME        NOT NULL,
    [EndDate]       DATETIME        NULL,
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
