
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/22/2014 22:40:08
-- Generated from EDMX file: C:\dev\ShortHornTodo\ShortHorn\ShortHorn\Models\Persistence.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [shorthorn];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserLoginToken]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LoginTokens] DROP CONSTRAINT [FK_UserLoginToken];
GO
IF OBJECT_ID(N'[dbo].[FK_TodoListTodoItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TodoItems] DROP CONSTRAINT [FK_TodoListTodoItem];
GO
IF OBJECT_ID(N'[dbo].[FK_UserTodoList]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TodoLists] DROP CONSTRAINT [FK_UserTodoList];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[LoginTokens]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoginTokens];
GO
IF OBJECT_ID(N'[dbo].[TodoLists]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TodoLists];
GO
IF OBJECT_ID(N'[dbo].[TodoItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TodoItems];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(128)  NOT NULL,
    [Email] nvarchar(128)  NOT NULL,
    [Password] nvarchar(128)  NOT NULL,
    [Active] bit  NOT NULL,
    [ActivationToken] nvarchar(128)  NOT NULL,
    [PrivilegeLevel] int  NOT NULL,
    [DateRegistered] datetime  NOT NULL,
    [DateActivated] datetime  NULL,
    [DateLastLogin] datetime  NULL
);
GO

-- Creating table 'LoginTokens'
CREATE TABLE [dbo].[LoginTokens] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Token] nvarchar(128)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'TodoLists'
CREATE TABLE [dbo].[TodoLists] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(128)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [IsFavourite] bit  NOT NULL,
    [UserId] int  NOT NULL
);
GO

-- Creating table 'TodoItems'
CREATE TABLE [dbo].[TodoItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(128)  NOT NULL,
    [Details] nvarchar(max)  NOT NULL,
    [DateFinish] datetime  NOT NULL,
    [IsFinished] bit  NOT NULL,
    [IsFavourite] bit  NOT NULL,
    [TodoListId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LoginTokens'
ALTER TABLE [dbo].[LoginTokens]
ADD CONSTRAINT [PK_LoginTokens]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TodoLists'
ALTER TABLE [dbo].[TodoLists]
ADD CONSTRAINT [PK_TodoLists]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TodoItems'
ALTER TABLE [dbo].[TodoItems]
ADD CONSTRAINT [PK_TodoItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [UserId] in table 'LoginTokens'
ALTER TABLE [dbo].[LoginTokens]
ADD CONSTRAINT [FK_UserLoginToken]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserLoginToken'
CREATE INDEX [IX_FK_UserLoginToken]
ON [dbo].[LoginTokens]
    ([UserId]);
GO

-- Creating foreign key on [TodoListId] in table 'TodoItems'
ALTER TABLE [dbo].[TodoItems]
ADD CONSTRAINT [FK_TodoListTodoItem]
    FOREIGN KEY ([TodoListId])
    REFERENCES [dbo].[TodoLists]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_TodoListTodoItem'
CREATE INDEX [IX_FK_TodoListTodoItem]
ON [dbo].[TodoItems]
    ([TodoListId]);
GO

-- Creating foreign key on [UserId] in table 'TodoLists'
ALTER TABLE [dbo].[TodoLists]
ADD CONSTRAINT [FK_UserTodoList]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserTodoList'
CREATE INDEX [IX_FK_UserTodoList]
ON [dbo].[TodoLists]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------