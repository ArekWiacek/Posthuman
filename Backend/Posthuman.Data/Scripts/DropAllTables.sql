USE [PosthumanaeArchivae2]
GO
ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_Users_Roles_RoleId]
GO
ALTER TABLE [dbo].[TodoItemCycles] DROP CONSTRAINT [FK_TodoItemCycles_TodoItem_TodoItemId]
GO
ALTER TABLE [dbo].[TodoItem] DROP CONSTRAINT [FK_TodoItem_TodoItem_ParentId]
GO
ALTER TABLE [dbo].[TodoItem] DROP CONSTRAINT [FK_TodoItem_Projects_ProjectId]
GO
ALTER TABLE [dbo].[TodoItem] DROP CONSTRAINT [FK_TodoItem_Avatars_AvatarId]
GO
ALTER TABLE [dbo].[TechnologyCardsDiscoveries] DROP CONSTRAINT [FK_TechnologyCardsDiscoveries_TechnologyCards_RewardCardId]
GO
ALTER TABLE [dbo].[TechnologyCardsDiscoveries] DROP CONSTRAINT [FK_TechnologyCardsDiscoveries_Avatars_AvatarId]
GO
ALTER TABLE [dbo].[Requirements] DROP CONSTRAINT [FK_Requirements_TechnologyCardsDiscoveries_TechnologyCardDiscoveryId]
GO
ALTER TABLE [dbo].[Projects] DROP CONSTRAINT [FK_Projects_Avatars_AvatarId]
GO
ALTER TABLE [dbo].[Avatars] DROP CONSTRAINT [FK_Avatars_Users_UserId]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 17.04.2022 20:45:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Users]') AND type in (N'U'))
DROP TABLE [dbo].[Users]
GO
/****** Object:  Table [dbo].[TodoItemCycles]    Script Date: 17.04.2022 20:45:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TodoItemCycles]') AND type in (N'U'))
DROP TABLE [dbo].[TodoItemCycles]
GO
/****** Object:  Table [dbo].[TodoItem]    Script Date: 17.04.2022 20:45:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TodoItem]') AND type in (N'U'))
DROP TABLE [dbo].[TodoItem]
GO
/****** Object:  Table [dbo].[TechnologyCardsDiscoveries]    Script Date: 17.04.2022 20:45:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TechnologyCardsDiscoveries]') AND type in (N'U'))
DROP TABLE [dbo].[TechnologyCardsDiscoveries]
GO
/****** Object:  Table [dbo].[TechnologyCards]    Script Date: 17.04.2022 20:45:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TechnologyCards]') AND type in (N'U'))
DROP TABLE [dbo].[TechnologyCards]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 17.04.2022 20:45:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Roles]') AND type in (N'U'))
DROP TABLE [dbo].[Roles]
GO
/****** Object:  Table [dbo].[Requirements]    Script Date: 17.04.2022 20:45:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Requirements]') AND type in (N'U'))
DROP TABLE [dbo].[Requirements]
GO
/****** Object:  Table [dbo].[Projects]    Script Date: 17.04.2022 20:45:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projects]') AND type in (N'U'))
DROP TABLE [dbo].[Projects]
GO
/****** Object:  Table [dbo].[EventItems]    Script Date: 17.04.2022 20:45:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventItems]') AND type in (N'U'))
DROP TABLE [dbo].[EventItems]
GO
/****** Object:  Table [dbo].[Avatars]    Script Date: 17.04.2022 20:45:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Avatars]') AND type in (N'U'))
DROP TABLE [dbo].[Avatars]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 17.04.2022 20:45:16 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[__EFMigrationsHistory]') AND type in (N'U'))
DROP TABLE [dbo].[__EFMigrationsHistory]
GO
