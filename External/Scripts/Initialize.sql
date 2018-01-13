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
        [Object_Id] = OBJECT_ID(N'[dbo].[UserPassword]') AND
        [Type] = N'U'
)
    DROP TABLE [dbo].[UserPassword]
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

CREATE TABLE [dbo].[UserPassword]
(
    [UserPasswordId]    INT             IDENTITY(1, 1)  NOT NULL,
    [UserId]            INT             NOT NULL,
    [PasswordHash]      NVARCHAR(60)    NOT NULL,
    [CreatedOn]         DATETIME        NOT NULL,
    [UpdatedOn]         DATETIME        NULL,
    CONSTRAINT [PK_UserPassword] PRIMARY KEY ([UserPasswordId] ASC),
    CONSTRAINT [FK_UserPassword_User] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User]
)
GO

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
