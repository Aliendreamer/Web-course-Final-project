USE [master]
GO
/****** Object:  Database [FanFictionAppDb]    Script Date: 26.4.2019 г. 18:51:24 ******/
CREATE DATABASE [FanFictionAppDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FanFictionAppDb', FILENAME = N'C:\Users\Teo\FanFictionAppDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'FanFictionAppDb_log', FILENAME = N'C:\Users\Teo\FanFictionAppDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [FanFictionAppDb] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FanFictionAppDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FanFictionAppDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [FanFictionAppDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FanFictionAppDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FanFictionAppDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [FanFictionAppDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FanFictionAppDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [FanFictionAppDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FanFictionAppDb] SET  MULTI_USER 
GO
ALTER DATABASE [FanFictionAppDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FanFictionAppDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FanFictionAppDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FanFictionAppDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [FanFictionAppDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [FanFictionAppDb] SET QUERY_STORE = OFF
GO
USE [FanFictionAppDb]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [FanFictionAppDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 26.4.2019 г. 18:51:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Announcements]    Script Date: 26.4.2019 г. 18:51:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Announcements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PublshedOn] [datetime2](7) NOT NULL,
	[Content] [nvarchar](500) NOT NULL,
	[AuthorId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Announcements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 26.4.2019 г. 18:51:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 26.4.2019 г. 18:51:25 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 26.4.2019 г. 18:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 26.4.2019 г. 18:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 26.4.2019 г. 18:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 26.4.2019 г. 18:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[Nickname] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 26.4.2019 г. 18:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlockedUsers]    Script Date: 26.4.2019 г. 18:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlockedUsers](
	[FanfictionUserId] [nvarchar](450) NOT NULL,
	[BlockedUserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_BlockedUsers] PRIMARY KEY CLUSTERED 
(
	[FanfictionUserId] ASC,
	[BlockedUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Chapters]    Script Date: 26.4.2019 г. 18:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Chapters](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[AuthorId] [nvarchar](450) NULL,
	[FanFictionStoryId] [int] NOT NULL,
	[Content] [nvarchar](3000) NOT NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[Title] [nvarchar](100) NULL,
 CONSTRAINT [PK_Chapters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 26.4.2019 г. 18:51:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NULL,
	[StoryId] [int] NULL,
	[Message] [nvarchar](300) NOT NULL,
	[CommentedOn] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FictionRatings]    Script Date: 26.4.2019 г. 18:51:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FictionRatings](
	[RatingId] [int] NOT NULL,
	[FanFictionId] [int] NOT NULL,
 CONSTRAINT [PK_FictionRatings] PRIMARY KEY CLUSTERED 
(
	[FanFictionId] ASC,
	[RatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FictionStories]    Script Date: 26.4.2019 г. 18:51:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FictionStories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[Summary] [nvarchar](200) NULL,
	[CreatedOn] [datetime2](7) NOT NULL,
	[LastEditedOn] [datetime2](7) NOT NULL,
	[StoryTypeId] [int] NOT NULL,
	[AuthorId] [nvarchar](450) NULL,
 CONSTRAINT [PK_FictionStories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logs]    Script Date: 26.4.2019 г. 18:51:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logs](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LogType] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Handled] [bit] NOT NULL,
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 26.4.2019 г. 18:51:27 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SendOn] [datetime2](7) NOT NULL,
	[SenderId] [nvarchar](450) NULL,
	[ReceiverId] [nvarchar](450) NULL,
	[Text] [nvarchar](400) NOT NULL,
	[IsReaden] [bit] NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 26.4.2019 г. 18:51:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](200) NOT NULL,
	[FanFictionUserId] [nvarchar](450) NULL,
	[Seen] [bit] NOT NULL,
	[UpdatedStoryId] [int] NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoryRatings]    Script Date: 26.4.2019 г. 18:51:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoryRatings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rating] [float] NOT NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_StoryRatings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StoryTypes]    Script Date: 26.4.2019 г. 18:51:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoryTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_StoryTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UsersStories]    Script Date: 26.4.2019 г. 18:51:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersStories](
	[FanfictionUserId] [nvarchar](450) NOT NULL,
	[FanFictionStoryId] [int] NOT NULL,
 CONSTRAINT [PK_UsersStories] PRIMARY KEY CLUSTERED 
(
	[FanfictionUserId] ASC,
	[FanFictionStoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Announcements_AuthorId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_Announcements_AuthorId] ON [dbo].[Announcements]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_BlockedUsers_BlockedUserId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_BlockedUsers_BlockedUserId] ON [dbo].[BlockedUsers]
(
	[BlockedUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Chapters_AuthorId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_Chapters_AuthorId] ON [dbo].[Chapters]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Chapters_FanFictionStoryId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_Chapters_FanFictionStoryId] ON [dbo].[Chapters]
(
	[FanFictionStoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_StoryId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_Comments_StoryId] ON [dbo].[Comments]
(
	[StoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Comments_UserId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_Comments_UserId] ON [dbo].[Comments]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FictionRatings_RatingId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_FictionRatings_RatingId] ON [dbo].[FictionRatings]
(
	[RatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_FictionStories_AuthorId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_FictionStories_AuthorId] ON [dbo].[FictionStories]
(
	[AuthorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FictionStories_StoryTypeId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_FictionStories_StoryTypeId] ON [dbo].[FictionStories]
(
	[StoryTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Messages_ReceiverId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_Messages_ReceiverId] ON [dbo].[Messages]
(
	[ReceiverId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Messages_SenderId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_Messages_SenderId] ON [dbo].[Messages]
(
	[SenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Notifications_FanFictionUserId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_Notifications_FanFictionUserId] ON [dbo].[Notifications]
(
	[FanFictionUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_StoryRatings_UserId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_StoryRatings_UserId] ON [dbo].[StoryRatings]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UsersStories_FanFictionStoryId]    Script Date: 26.4.2019 г. 18:51:29 ******/
CREATE NONCLUSTERED INDEX [IX_UsersStories_FanFictionStoryId] ON [dbo].[UsersStories]
(
	[FanFictionStoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Chapters] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [CreatedOn]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT ((0)) FOR [Seen]
GO
ALTER TABLE [dbo].[Notifications] ADD  DEFAULT ((0)) FOR [UpdatedStoryId]
GO
ALTER TABLE [dbo].[Announcements]  WITH CHECK ADD  CONSTRAINT [FK_Announcements_AspNetUsers_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Announcements] CHECK CONSTRAINT [FK_Announcements_AspNetUsers_AuthorId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[BlockedUsers]  WITH CHECK ADD  CONSTRAINT [FK_BlockedUsers_AspNetUsers_BlockedUserId] FOREIGN KEY([BlockedUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[BlockedUsers] CHECK CONSTRAINT [FK_BlockedUsers_AspNetUsers_BlockedUserId]
GO
ALTER TABLE [dbo].[BlockedUsers]  WITH CHECK ADD  CONSTRAINT [FK_BlockedUsers_AspNetUsers_FanfictionUserId] FOREIGN KEY([FanfictionUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BlockedUsers] CHECK CONSTRAINT [FK_BlockedUsers_AspNetUsers_FanfictionUserId]
GO
ALTER TABLE [dbo].[Chapters]  WITH CHECK ADD  CONSTRAINT [FK_Chapters_AspNetUsers_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Chapters] CHECK CONSTRAINT [FK_Chapters_AspNetUsers_AuthorId]
GO
ALTER TABLE [dbo].[Chapters]  WITH CHECK ADD  CONSTRAINT [FK_Chapters_FictionStories_FanFictionStoryId] FOREIGN KEY([FanFictionStoryId])
REFERENCES [dbo].[FictionStories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Chapters] CHECK CONSTRAINT [FK_Chapters_FictionStories_FanFictionStoryId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_FictionStories_StoryId] FOREIGN KEY([StoryId])
REFERENCES [dbo].[FictionStories] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_FictionStories_StoryId]
GO
ALTER TABLE [dbo].[FictionRatings]  WITH CHECK ADD  CONSTRAINT [FK_FictionRatings_FictionStories_FanFictionId] FOREIGN KEY([FanFictionId])
REFERENCES [dbo].[FictionStories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FictionRatings] CHECK CONSTRAINT [FK_FictionRatings_FictionStories_FanFictionId]
GO
ALTER TABLE [dbo].[FictionRatings]  WITH CHECK ADD  CONSTRAINT [FK_FictionRatings_StoryRatings_RatingId] FOREIGN KEY([RatingId])
REFERENCES [dbo].[StoryRatings] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FictionRatings] CHECK CONSTRAINT [FK_FictionRatings_StoryRatings_RatingId]
GO
ALTER TABLE [dbo].[FictionStories]  WITH CHECK ADD  CONSTRAINT [FK_FictionStories_AspNetUsers_AuthorId] FOREIGN KEY([AuthorId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[FictionStories] CHECK CONSTRAINT [FK_FictionStories_AspNetUsers_AuthorId]
GO
ALTER TABLE [dbo].[FictionStories]  WITH CHECK ADD  CONSTRAINT [FK_FictionStories_StoryTypes_StoryTypeId] FOREIGN KEY([StoryTypeId])
REFERENCES [dbo].[StoryTypes] ([Id])
GO
ALTER TABLE [dbo].[FictionStories] CHECK CONSTRAINT [FK_FictionStories_StoryTypes_StoryTypeId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_AspNetUsers_ReceiverId] FOREIGN KEY([ReceiverId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_AspNetUsers_ReceiverId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_AspNetUsers_SenderId] FOREIGN KEY([SenderId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_AspNetUsers_SenderId]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_AspNetUsers_FanFictionUserId] FOREIGN KEY([FanFictionUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_AspNetUsers_FanFictionUserId]
GO
ALTER TABLE [dbo].[StoryRatings]  WITH CHECK ADD  CONSTRAINT [FK_StoryRatings_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[StoryRatings] CHECK CONSTRAINT [FK_StoryRatings_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[UsersStories]  WITH CHECK ADD  CONSTRAINT [FK_UsersStories_AspNetUsers_FanfictionUserId] FOREIGN KEY([FanfictionUserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersStories] CHECK CONSTRAINT [FK_UsersStories_AspNetUsers_FanfictionUserId]
GO
ALTER TABLE [dbo].[UsersStories]  WITH CHECK ADD  CONSTRAINT [FK_UsersStories_FictionStories_FanFictionStoryId] FOREIGN KEY([FanFictionStoryId])
REFERENCES [dbo].[FictionStories] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UsersStories] CHECK CONSTRAINT [FK_UsersStories_FictionStories_FanFictionStoryId]
GO
USE [master]
GO
ALTER DATABASE [FanFictionAppDb] SET  READ_WRITE 
GO
