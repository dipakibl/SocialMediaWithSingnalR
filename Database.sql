USE [master]
GO
/****** Object:  Database [UploadProfilePhoto]    Script Date: 6/24/2020 2:02:15 PM ******/
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
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/24/2020 2:02:15 PM ******/
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
/****** Object:  Table [dbo].[pictureLikes]    Script Date: 6/24/2020 2:02:15 PM ******/
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
/****** Object:  Table [dbo].[ProfilesPictureGalleries]    Script Date: 6/24/2020 2:02:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProfilesPictureGalleries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Picture] [nvarchar](max) NULL,
 CONSTRAINT [PK_ProfilesPictureGalleries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserNotifications]    Script Date: 6/24/2020 2:02:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserNotifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FriendId] [int] NOT NULL,
	[PictureId] [int] NOT NULL,
	[Type] [nvarchar](max) NULL,
	[IsRead] [bit] NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
	[DateDeleted] [datetime2](7) NULL,
 CONSTRAINT [PK_UserNotifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 6/24/2020 2:02:15 PM ******/
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
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200612053933_AddDatabase12062020at1109am', N'3.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200612120200_UpdateGalleyTable12062020at0531pm', N'3.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200616103206_AddpicturelikeTable16062020at0400pm', N'3.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200616105001_addliketable16062020at0419pm', N'3.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200617124321_AddNotificaitontable17062020at0613pm', N'3.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200618051421_updatetables18062020at1042am', N'3.0.0')
SET IDENTITY_INSERT [dbo].[pictureLikes] ON 

INSERT [dbo].[pictureLikes] ([Id], [FileId], [UserId], [TimeStamp]) VALUES (51, 10, 1, CAST(N'2020-06-24 06:46:54.2373849' AS DateTime2))
INSERT [dbo].[pictureLikes] ([Id], [FileId], [UserId], [TimeStamp]) VALUES (52, 6, 1, CAST(N'2020-06-24 06:46:58.6498830' AS DateTime2))
INSERT [dbo].[pictureLikes] ([Id], [FileId], [UserId], [TimeStamp]) VALUES (53, 1, 3, CAST(N'2020-06-24 06:59:01.5476668' AS DateTime2))
INSERT [dbo].[pictureLikes] ([Id], [FileId], [UserId], [TimeStamp]) VALUES (54, 14, 2, CAST(N'2020-06-24 07:41:19.8273649' AS DateTime2))
INSERT [dbo].[pictureLikes] ([Id], [FileId], [UserId], [TimeStamp]) VALUES (55, 11, 2, CAST(N'2020-06-24 07:44:30.1615153' AS DateTime2))
INSERT [dbo].[pictureLikes] ([Id], [FileId], [UserId], [TimeStamp]) VALUES (56, 15, 2, CAST(N'2020-06-24 08:22:03.4969837' AS DateTime2))
INSERT [dbo].[pictureLikes] ([Id], [FileId], [UserId], [TimeStamp]) VALUES (57, 7, 2, CAST(N'2020-06-24 08:23:08.2974899' AS DateTime2))
INSERT [dbo].[pictureLikes] ([Id], [FileId], [UserId], [TimeStamp]) VALUES (58, 14, 3, CAST(N'2020-06-24 08:24:13.8773778' AS DateTime2))
INSERT [dbo].[pictureLikes] ([Id], [FileId], [UserId], [TimeStamp]) VALUES (59, 11, 3, CAST(N'2020-06-24 08:24:20.1876212' AS DateTime2))
INSERT [dbo].[pictureLikes] ([Id], [FileId], [UserId], [TimeStamp]) VALUES (60, 9, 3, CAST(N'2020-06-24 08:24:24.8394479' AS DateTime2))
INSERT [dbo].[pictureLikes] ([Id], [FileId], [UserId], [TimeStamp]) VALUES (61, 7, 3, CAST(N'2020-06-24 08:24:28.7600827' AS DateTime2))
INSERT [dbo].[pictureLikes] ([Id], [FileId], [UserId], [TimeStamp]) VALUES (62, 8, 1, CAST(N'2020-06-24 08:25:21.7365501' AS DateTime2))
SET IDENTITY_INSERT [dbo].[pictureLikes] OFF
SET IDENTITY_INSERT [dbo].[ProfilesPictureGalleries] ON 

INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (1, 2, N'ef2ccc9566504d30bbcf151217ccb1a4.jpg')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (2, 2, N'72747f3c69164badb973c48749870133.jpg')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (3, 2, N'4f3ebaa8c80e409198a75fc678f4d046.jpg')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (4, 1, N'26091d72e21e419ba2cdd9875898ffa5.jpg')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (5, 1, N'47afb16025c0473b8180ca432b2b8f9e.jpg')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (6, 2, N'a5c910d522e3447dac46522bf4febdf1.jpg')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (7, 1, N'57085bf9d986456f9bd32743d58d2266.jpg')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (8, 2, N'2b4fd71990354c6cb43e730295ce3850.jpg')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (9, 1, N'7633034628724b21a5f5b22ce5bf86d4.jpg')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (10, 2, N'b4d933fbb42b4b8f99c79008dd2f1394.jpg')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (11, 1, N'775483e059314a1da1efe3fbbc7041d7.jpg')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (12, 3, N'8facc601057241deb15affff73ee374d.JPG')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (13, 3, N'7191b381abcc4eefa33f297195d59bbd.JPG')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (14, 1, N'e4fb9ebbeb734b6d92b67173ca1a629b.jpg')
INSERT [dbo].[ProfilesPictureGalleries] ([Id], [UserId], [Picture]) VALUES (15, 3, N'9a0e343a5ac2415ca69ca5fcb35fbc02.png')
SET IDENTITY_INSERT [dbo].[ProfilesPictureGalleries] OFF
SET IDENTITY_INSERT [dbo].[UserNotifications] ON 

INSERT [dbo].[UserNotifications] ([Id], [UserId], [FriendId], [PictureId], [Type], [IsRead], [DateCreated], [DateModified], [DateDeleted]) VALUES (48, 2, 1, 10, N'Liked your Photo', 1, CAST(N'2020-06-24 06:46:54.6924634' AS DateTime2), NULL, NULL)
INSERT [dbo].[UserNotifications] ([Id], [UserId], [FriendId], [PictureId], [Type], [IsRead], [DateCreated], [DateModified], [DateDeleted]) VALUES (49, 2, 1, 6, N'Liked your Photo', 1, CAST(N'2020-06-24 06:46:58.7359700' AS DateTime2), NULL, NULL)
INSERT [dbo].[UserNotifications] ([Id], [UserId], [FriendId], [PictureId], [Type], [IsRead], [DateCreated], [DateModified], [DateDeleted]) VALUES (50, 2, 3, 1, N'Liked your Photo', 1, CAST(N'2020-06-24 06:59:01.8694511' AS DateTime2), NULL, NULL)
INSERT [dbo].[UserNotifications] ([Id], [UserId], [FriendId], [PictureId], [Type], [IsRead], [DateCreated], [DateModified], [DateDeleted]) VALUES (51, 1, 2, 14, N'Liked your Photo', 1, CAST(N'2020-06-24 07:41:19.9902883' AS DateTime2), NULL, NULL)
INSERT [dbo].[UserNotifications] ([Id], [UserId], [FriendId], [PictureId], [Type], [IsRead], [DateCreated], [DateModified], [DateDeleted]) VALUES (52, 1, 2, 11, N'Liked your Photo', 1, CAST(N'2020-06-24 07:44:30.5082669' AS DateTime2), NULL, NULL)
INSERT [dbo].[UserNotifications] ([Id], [UserId], [FriendId], [PictureId], [Type], [IsRead], [DateCreated], [DateModified], [DateDeleted]) VALUES (53, 3, 2, 15, N'Liked your Photo', 1, CAST(N'2020-06-24 08:22:03.5836372' AS DateTime2), NULL, NULL)
INSERT [dbo].[UserNotifications] ([Id], [UserId], [FriendId], [PictureId], [Type], [IsRead], [DateCreated], [DateModified], [DateDeleted]) VALUES (54, 1, 2, 7, N'Liked your Photo', 1, CAST(N'2020-06-24 08:23:08.3414799' AS DateTime2), NULL, NULL)
INSERT [dbo].[UserNotifications] ([Id], [UserId], [FriendId], [PictureId], [Type], [IsRead], [DateCreated], [DateModified], [DateDeleted]) VALUES (55, 1, 3, 14, N'Liked your Photo', 0, CAST(N'2020-06-24 08:24:13.9092400' AS DateTime2), NULL, NULL)
INSERT [dbo].[UserNotifications] ([Id], [UserId], [FriendId], [PictureId], [Type], [IsRead], [DateCreated], [DateModified], [DateDeleted]) VALUES (56, 1, 3, 11, N'Liked your Photo', 0, CAST(N'2020-06-24 08:24:20.2087998' AS DateTime2), NULL, NULL)
INSERT [dbo].[UserNotifications] ([Id], [UserId], [FriendId], [PictureId], [Type], [IsRead], [DateCreated], [DateModified], [DateDeleted]) VALUES (57, 1, 3, 9, N'Liked your Photo', 0, CAST(N'2020-06-24 08:24:24.8801851' AS DateTime2), NULL, NULL)
INSERT [dbo].[UserNotifications] ([Id], [UserId], [FriendId], [PictureId], [Type], [IsRead], [DateCreated], [DateModified], [DateDeleted]) VALUES (58, 1, 3, 7, N'Liked your Photo', 0, CAST(N'2020-06-24 08:24:28.7825757' AS DateTime2), NULL, NULL)
INSERT [dbo].[UserNotifications] ([Id], [UserId], [FriendId], [PictureId], [Type], [IsRead], [DateCreated], [DateModified], [DateDeleted]) VALUES (59, 2, 1, 8, N'Liked your Photo', 1, CAST(N'2020-06-24 08:25:21.7630114' AS DateTime2), NULL, NULL)
SET IDENTITY_INSERT [dbo].[UserNotifications] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([Id], [First_Name], [Last_Name], [Email], [Password], [ProfilePictureId]) VALUES (1, N'Nitin', N'Borse', N'borsenitin99@gmail.com', N'nitin', 14)
INSERT [dbo].[Users] ([Id], [First_Name], [Last_Name], [Email], [Password], [ProfilePictureId]) VALUES (2, N'Mayur', N'Lonari', N'mayurlonari22@gmail.com', N'mayur', 10)
INSERT [dbo].[Users] ([Id], [First_Name], [Last_Name], [Email], [Password], [ProfilePictureId]) VALUES (3, N'Jagdish', N'Prajapati', N'jagdish22@gmail.com', N'jagdish', 15)
SET IDENTITY_INSERT [dbo].[Users] OFF
USE [master]
GO
ALTER DATABASE [UploadProfilePhoto] SET  READ_WRITE 
GO
