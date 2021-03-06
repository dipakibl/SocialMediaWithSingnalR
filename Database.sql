USE [master]
GO
/****** Object:  Database [UploadProfilePhoto]    Script Date: 09-07-2020 11:16:10 ******/
CREATE DATABASE [UploadProfilePhoto]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UploadProfilePhoto', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\UploadProfilePhoto.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'UploadProfilePhoto_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\UploadProfilePhoto_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [UploadProfilePhoto] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UploadProfilePhoto].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UploadProfilePhoto] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET ARITHABORT OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [UploadProfilePhoto] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UploadProfilePhoto] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UploadProfilePhoto] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET  ENABLE_BROKER 
GO
ALTER DATABASE [UploadProfilePhoto] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UploadProfilePhoto] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [UploadProfilePhoto] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [UploadProfilePhoto] SET  MULTI_USER 
GO
ALTER DATABASE [UploadProfilePhoto] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UploadProfilePhoto] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UploadProfilePhoto] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UploadProfilePhoto] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [UploadProfilePhoto] SET DELAYED_DURABILITY = DISABLED 
GO
USE [UploadProfilePhoto]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 09-07-2020 11:16:10 ******/
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
/****** Object:  Table [dbo].[Friends]    Script Date: 09-07-2020 11:16:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Friends](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FrindUserId] [int] NOT NULL,
	[Confirmed] [bit] NOT NULL,
	[InvitedDate] [datetime2](7) NULL,
	[AcceptedDate] [datetime2](7) NULL,
	[DeclineDate] [datetime2](7) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
	[DateDeleted] [datetime2](7) NULL,
 CONSTRAINT [PK_Friends] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PictureCommentReplays]    Script Date: 09-07-2020 11:16:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PictureCommentReplays](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[PictureCommentId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[FriendId] [int] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
	[DateDeleted] [datetime2](7) NULL,
 CONSTRAINT [PK_PictureCommentReplays] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PictureComments]    Script Date: 09-07-2020 11:16:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PictureComments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PictureId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Message] [nvarchar](max) NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
	[DateDeleted] [datetime2](7) NULL,
 CONSTRAINT [PK_PictureComments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PictureCommentsLikes]    Script Date: 09-07-2020 11:16:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PictureCommentsLikes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CommentId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_PictureCommentsLikes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[pictureLikes]    Script Date: 09-07-2020 11:16:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[pictureLikes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[TimeStamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_pictureLikes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProfilesPictureGalleries]    Script Date: 09-07-2020 11:16:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfilesPictureGalleries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Picture] [nvarchar](max) NULL,
	[DateCreated] [datetime2](7) NOT NULL DEFAULT ('0001-01-01T00:00:00.0000000'),
	[DateDeleted] [datetime2](7) NULL,
	[DateModified] [datetime2](7) NULL,
 CONSTRAINT [PK_ProfilesPictureGalleries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserNotifications]    Script Date: 09-07-2020 11:16:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserNotifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FriendId] [int] NOT NULL,
	[PictureId] [int] NOT NULL,
	[Type] [smallint] NOT NULL,
	[IsRead] [bit] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
	[DateDeleted] [datetime2](7) NULL,
	[CommentedId] [int] NOT NULL DEFAULT ((0)),
 CONSTRAINT [PK_UserNotifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 09-07-2020 11:16:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[First_Name] [nvarchar](max) NULL,
	[Last_Name] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Password] [nvarchar](max) NULL,
	[ProfilePictureId] [int] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
USE [master]
GO
ALTER DATABASE [UploadProfilePhoto] SET  READ_WRITE 
GO
