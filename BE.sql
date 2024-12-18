USE [master]
GO
/****** Object:  Database [GOStudyContext]    Script Date: 25/09/2024 9:21:14 CH ******/
CREATE DATABASE [GOStudyContext]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GOStudyContext', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\GOStudyContext.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GOStudyContext_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\GOStudyContext_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [GOStudyContext] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GOStudyContext].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GOStudyContext] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GOStudyContext] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GOStudyContext] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GOStudyContext] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GOStudyContext] SET ARITHABORT OFF 
GO
ALTER DATABASE [GOStudyContext] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GOStudyContext] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GOStudyContext] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GOStudyContext] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GOStudyContext] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GOStudyContext] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GOStudyContext] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GOStudyContext] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GOStudyContext] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GOStudyContext] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GOStudyContext] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GOStudyContext] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GOStudyContext] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GOStudyContext] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GOStudyContext] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GOStudyContext] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [GOStudyContext] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GOStudyContext] SET RECOVERY FULL 
GO
ALTER DATABASE [GOStudyContext] SET  MULTI_USER 
GO
ALTER DATABASE [GOStudyContext] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GOStudyContext] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GOStudyContext] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GOStudyContext] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GOStudyContext] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'GOStudyContext', N'ON'
GO
ALTER DATABASE [GOStudyContext] SET QUERY_STORE = OFF
GO
USE [GOStudyContext]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [GOStudyContext]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 25/09/2024 9:21:14 CH ******/
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
/****** Object:  Table [dbo].[Accounts]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accounts](
	[AccountId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Accounts] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Analytics]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Analytics](
	[AnalyticsId] [int] IDENTITY(1,1) NOT NULL,
	[Metric] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[TaskId] [int] NOT NULL,
	[ClassroomId] [int] NOT NULL,
	[Timestamp] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Analytics] PRIMARY KEY CLUSTERED 
(
	[AnalyticsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Attendances]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendances](
	[AttendanceId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Date] [datetime2](7) NOT NULL,
	[IsPresent] [bit] NOT NULL,
	[Notes] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Attendances] PRIMARY KEY CLUSTERED 
(
	[AttendanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogImgs]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogImgs](
	[BlogImgId] [int] IDENTITY(1,1) NOT NULL,
	[BlogId] [int] NOT NULL,
	[Img] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_BlogImgs] PRIMARY KEY CLUSTERED 
(
	[BlogImgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BlogPosts]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BlogPosts](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[Category] [nvarchar](max) NOT NULL,
	[Tags] [nvarchar](max) NOT NULL,
	[ViewCount] [int] NOT NULL,
	[IsDraft] [bit] NOT NULL,
	[shareCount] [int] NOT NULL,
	[likeCount] [int] NOT NULL,
	[image] [nvarchar](max) NOT NULL,
	[IsFavorite] [bit] NOT NULL,
	[IsTrending] [bit] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_BlogPosts] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bookmarks]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bookmarks](
	[BookmarkId] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Bookmarks] PRIMARY KEY CLUSTERED 
(
	[BookmarkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classrooms]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classrooms](
	[ClassroomId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[SpecializationId] [int] NOT NULL,
	[Nickname] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[LinkUrl] [nvarchar](max) NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_Classrooms] PRIMARY KEY CLUSTERED 
(
	[ClassroomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comments]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContactInfos]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContactInfos](
	[ContactInfoId] [int] IDENTITY(1,1) NOT NULL,
	[ContactType] [nvarchar](max) NOT NULL,
	[Detail] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_ContactInfos] PRIMARY KEY CLUSTERED 
(
	[ContactInfoId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Data]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Data](
	[DataId] [int] IDENTITY(1,1) NOT NULL,
	[DataType] [nvarchar](max) NOT NULL,
	[DataContent] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_Data] PRIMARY KEY CLUSTERED 
(
	[DataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EncryptionKeys]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EncryptionKeys](
	[EncryptionKeyId] [int] IDENTITY(1,1) NOT NULL,
	[DataId] [int] NOT NULL,
	[Key] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_EncryptionKeys] PRIMARY KEY CLUSTERED 
(
	[EncryptionKeyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FAQs]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FAQs](
	[FaqId] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](max) NOT NULL,
	[Answer] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_FAQs] PRIMARY KEY CLUSTERED 
(
	[FaqId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Features]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Features](
	[FeatureId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[PackageId] [int] NOT NULL,
 CONSTRAINT [PK_Features] PRIMARY KEY CLUSTERED 
(
	[FeatureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FriendRequests]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FriendRequests](
	[FriendRequestId] [int] IDENTITY(1,1) NOT NULL,
	[RequesterId] [int] NOT NULL,
	[RecipientId] [int] NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[SentAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_FriendRequests] PRIMARY KEY CLUSTERED 
(
	[FriendRequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Messages]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Messages](
	[MessageId] [int] IDENTITY(1,1) NOT NULL,
	[SenderId] [int] NOT NULL,
	[RecipientId] [int] NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[ClassroomId] [int] NULL,
 CONSTRAINT [PK_Messages] PRIMARY KEY CLUSTERED 
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notifications]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notifications](
	[NotificationId] [int] IDENTITY(1,1) NOT NULL,
	[TaskId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[SentAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Notifications] PRIMARY KEY CLUSTERED 
(
	[NotificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Packages]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Packages](
	[PackageId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Packages] PRIMARY KEY CLUSTERED 
(
	[PackageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PaymentTransactions]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaymentTransactions](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PackageId] [int] NOT NULL,
	[TransactionDate] [datetime2](7) NOT NULL,
	[PaymentMethod] [nvarchar](max) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[PaymentLastMessage] [nvarchar](max) NOT NULL,
	[PaymentContent] [nvarchar](max) NOT NULL,
	[PaymentCurrency] [nvarchar](max) NOT NULL,
	[PaymentRefId] [nvarchar](max) NOT NULL,
	[ExpireDate] [datetime2](7) NOT NULL,
	[PaymentLanguage] [nvarchar](max) NOT NULL,
	[MerchantId] [nvarchar](max) NOT NULL,
	[PaymentDestinationId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PaymentTransactions] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PrivacySettings]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PrivacySettings](
	[PrivacySettingId] [int] IDENTITY(1,1) NOT NULL,
	[Visibility] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PrivacySettings] PRIMARY KEY CLUSTERED 
(
	[PrivacySettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Rankings]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Rankings](
	[RankId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PerformanceScore] [decimal](18, 2) NOT NULL,
	[RankPosition] [int] NOT NULL,
	[Period] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Rankings] PRIMARY KEY CLUSTERED 
(
	[RankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reactions]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reactions](
	[ReactionId] [int] IDENTITY(1,1) NOT NULL,
	[MessageId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Emoji] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Reactions] PRIMARY KEY CLUSTERED 
(
	[ReactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshToken]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshToken](
	[RefreshTokenId] [uniqueidentifier] NOT NULL,
	[Token] [varchar](500) NOT NULL,
	[IssuedAt] [datetime] NOT NULL,
	[ExpriedAt] [datetime] NOT NULL,
	[JwtId] [varchar](100) NOT NULL,
	[IsUsed] [bit] NOT NULL,
	[IsRevoked] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_RefreshToken] PRIMARY KEY CLUSTERED 
(
	[RefreshTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Semesters]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Semesters](
	[SemesterId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Semesters] PRIMARY KEY CLUSTERED 
(
	[SemesterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Specializations]    Script Date: 25/09/2024 9:21:14 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Specializations](
	[SpecializationId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Specializations] PRIMARY KEY CLUSTERED 
(
	[SpecializationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupportTickets]    Script Date: 25/09/2024 9:21:15 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupportTickets](
	[TicketId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Issue] [nvarchar](max) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_SupportTickets] PRIMARY KEY CLUSTERED 
(
	[TicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 25/09/2024 9:21:15 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[TaskId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[TimeComplete] [int] NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[ScheduledTime] [datetime2](7) NOT NULL,
	[Status] [nvarchar](max) NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLikes]    Script Date: 25/09/2024 9:21:15 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLikes](
	[UserLikeId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[BlogId] [int] NOT NULL,
 CONSTRAINT [PK_UserLikes] PRIMARY KEY CLUSTERED 
(
	[UserLikeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 25/09/2024 9:21:15 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Role] [int] NOT NULL,
	[PasswordHash] [nvarchar](max) NOT NULL,
	[ProfileImage] [nvarchar](max) NOT NULL,
	[PrivacySettingId] [int] NOT NULL,
	[SemesterId] [int] NOT NULL,
	[birthday] [datetime2](7) NOT NULL,
	[phone] [nvarchar](max) NOT NULL,
	[sex] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserSpecializations]    Script Date: 25/09/2024 9:21:15 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserSpecializations](
	[UserSpecializationId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SpecializationId] [int] NOT NULL,
	[DateStart] [datetime2](7) NOT NULL,
	[DateEnd] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_UserSpecializations] PRIMARY KEY CLUSTERED 
(
	[UserSpecializationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_Accounts_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Accounts_UserId] ON [dbo].[Accounts]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Analytics_ClassroomId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Analytics_ClassroomId] ON [dbo].[Analytics]
(
	[ClassroomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Analytics_TaskId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Analytics_TaskId] ON [dbo].[Analytics]
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Analytics_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Analytics_UserId] ON [dbo].[Analytics]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Attendances_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Attendances_UserId] ON [dbo].[Attendances]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BlogImgs_BlogId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_BlogImgs_BlogId] ON [dbo].[BlogImgs]
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_BlogPosts_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_BlogPosts_UserId] ON [dbo].[BlogPosts]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bookmarks_PostId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Bookmarks_PostId] ON [dbo].[Bookmarks]
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Bookmarks_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Bookmarks_UserId] ON [dbo].[Bookmarks]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Classrooms_SpecializationId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Classrooms_SpecializationId] ON [dbo].[Classrooms]
(
	[SpecializationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_PostId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Comments_PostId] ON [dbo].[Comments]
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Comments_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Comments_UserId] ON [dbo].[Comments]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Data_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Data_UserId] ON [dbo].[Data]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_EncryptionKeys_DataId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_EncryptionKeys_DataId] ON [dbo].[EncryptionKeys]
(
	[DataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Features_PackageId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Features_PackageId] ON [dbo].[Features]
(
	[PackageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FriendRequests_RecipientId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_FriendRequests_RecipientId] ON [dbo].[FriendRequests]
(
	[RecipientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_FriendRequests_RequesterId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_FriendRequests_RequesterId] ON [dbo].[FriendRequests]
(
	[RequesterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Messages_ClassroomId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Messages_ClassroomId] ON [dbo].[Messages]
(
	[ClassroomId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Messages_RecipientId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Messages_RecipientId] ON [dbo].[Messages]
(
	[RecipientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Messages_SenderId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Messages_SenderId] ON [dbo].[Messages]
(
	[SenderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Notifications_TaskId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Notifications_TaskId] ON [dbo].[Notifications]
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Notifications_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Notifications_UserId] ON [dbo].[Notifications]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PaymentTransactions_PackageId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_PaymentTransactions_PackageId] ON [dbo].[PaymentTransactions]
(
	[PackageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PaymentTransactions_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_PaymentTransactions_UserId] ON [dbo].[PaymentTransactions]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Rankings_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Rankings_UserId] ON [dbo].[Rankings]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reactions_MessageId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Reactions_MessageId] ON [dbo].[Reactions]
(
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Reactions_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Reactions_UserId] ON [dbo].[Reactions]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_RefreshToken_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_RefreshToken_UserId] ON [dbo].[RefreshToken]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_SupportTickets_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_SupportTickets_UserId] ON [dbo].[SupportTickets]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Tasks_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Tasks_UserId] ON [dbo].[Tasks]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserLikes_BlogId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_UserLikes_BlogId] ON [dbo].[UserLikes]
(
	[BlogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserLikes_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_UserLikes_UserId] ON [dbo].[UserLikes]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_PrivacySettingId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Users_PrivacySettingId] ON [dbo].[Users]
(
	[PrivacySettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_Users_SemesterId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_Users_SemesterId] ON [dbo].[Users]
(
	[SemesterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserSpecializations_SpecializationId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_UserSpecializations_SpecializationId] ON [dbo].[UserSpecializations]
(
	[SpecializationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_UserSpecializations_UserId]    Script Date: 25/09/2024 9:21:15 CH ******/
CREATE NONCLUSTERED INDEX [IX_UserSpecializations_UserId] ON [dbo].[UserSpecializations]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Classrooms] ADD  DEFAULT (N'') FOR [LinkUrl]
GO
ALTER TABLE [dbo].[Classrooms] ADD  DEFAULT ((0)) FOR [status]
GO
ALTER TABLE [dbo].[Tasks] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT ('0001-01-01T00:00:00.0000000') FOR [birthday]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (N'') FOR [phone]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (N'') FOR [sex]
GO
ALTER TABLE [dbo].[Accounts]  WITH CHECK ADD  CONSTRAINT [FK_Accounts_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Accounts] CHECK CONSTRAINT [FK_Accounts_Users_UserId]
GO
ALTER TABLE [dbo].[Analytics]  WITH CHECK ADD  CONSTRAINT [FK_Analytics_Classrooms_ClassroomId] FOREIGN KEY([ClassroomId])
REFERENCES [dbo].[Classrooms] ([ClassroomId])
GO
ALTER TABLE [dbo].[Analytics] CHECK CONSTRAINT [FK_Analytics_Classrooms_ClassroomId]
GO
ALTER TABLE [dbo].[Analytics]  WITH CHECK ADD  CONSTRAINT [FK_Analytics_Tasks_TaskId] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([TaskId])
GO
ALTER TABLE [dbo].[Analytics] CHECK CONSTRAINT [FK_Analytics_Tasks_TaskId]
GO
ALTER TABLE [dbo].[Analytics]  WITH CHECK ADD  CONSTRAINT [FK_Analytics_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Analytics] CHECK CONSTRAINT [FK_Analytics_Users_UserId]
GO
ALTER TABLE [dbo].[Attendances]  WITH CHECK ADD  CONSTRAINT [FK_Attendances_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Attendances] CHECK CONSTRAINT [FK_Attendances_Users_UserId]
GO
ALTER TABLE [dbo].[BlogImgs]  WITH CHECK ADD  CONSTRAINT [FK_BlogImgs_BlogPosts_BlogId] FOREIGN KEY([BlogId])
REFERENCES [dbo].[BlogPosts] ([PostId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BlogImgs] CHECK CONSTRAINT [FK_BlogImgs_BlogPosts_BlogId]
GO
ALTER TABLE [dbo].[BlogPosts]  WITH CHECK ADD  CONSTRAINT [FK_BlogPosts_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[BlogPosts] CHECK CONSTRAINT [FK_BlogPosts_Users_UserId]
GO
ALTER TABLE [dbo].[Bookmarks]  WITH CHECK ADD  CONSTRAINT [FK_Bookmarks_BlogPosts_PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[BlogPosts] ([PostId])
GO
ALTER TABLE [dbo].[Bookmarks] CHECK CONSTRAINT [FK_Bookmarks_BlogPosts_PostId]
GO
ALTER TABLE [dbo].[Bookmarks]  WITH CHECK ADD  CONSTRAINT [FK_Bookmarks_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Bookmarks] CHECK CONSTRAINT [FK_Bookmarks_Users_UserId]
GO
ALTER TABLE [dbo].[Classrooms]  WITH CHECK ADD  CONSTRAINT [FK_Classrooms_Specializations_SpecializationId] FOREIGN KEY([SpecializationId])
REFERENCES [dbo].[Specializations] ([SpecializationId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Classrooms] CHECK CONSTRAINT [FK_Classrooms_Specializations_SpecializationId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_BlogPosts_PostId] FOREIGN KEY([PostId])
REFERENCES [dbo].[BlogPosts] ([PostId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_BlogPosts_PostId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Users_UserId]
GO
ALTER TABLE [dbo].[Data]  WITH CHECK ADD  CONSTRAINT [FK_Data_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Data] CHECK CONSTRAINT [FK_Data_Users_UserId]
GO
ALTER TABLE [dbo].[EncryptionKeys]  WITH CHECK ADD  CONSTRAINT [FK_EncryptionKeys_Data_DataId] FOREIGN KEY([DataId])
REFERENCES [dbo].[Data] ([DataId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EncryptionKeys] CHECK CONSTRAINT [FK_EncryptionKeys_Data_DataId]
GO
ALTER TABLE [dbo].[Features]  WITH CHECK ADD  CONSTRAINT [FK_Features_Packages_PackageId] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([PackageId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Features] CHECK CONSTRAINT [FK_Features_Packages_PackageId]
GO
ALTER TABLE [dbo].[FriendRequests]  WITH CHECK ADD  CONSTRAINT [FK_FriendRequests_Users_RecipientId] FOREIGN KEY([RecipientId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[FriendRequests] CHECK CONSTRAINT [FK_FriendRequests_Users_RecipientId]
GO
ALTER TABLE [dbo].[FriendRequests]  WITH CHECK ADD  CONSTRAINT [FK_FriendRequests_Users_RequesterId] FOREIGN KEY([RequesterId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[FriendRequests] CHECK CONSTRAINT [FK_FriendRequests_Users_RequesterId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Classrooms_ClassroomId] FOREIGN KEY([ClassroomId])
REFERENCES [dbo].[Classrooms] ([ClassroomId])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Classrooms_ClassroomId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users_RecipientId] FOREIGN KEY([RecipientId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users_RecipientId]
GO
ALTER TABLE [dbo].[Messages]  WITH CHECK ADD  CONSTRAINT [FK_Messages_Users_SenderId] FOREIGN KEY([SenderId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Messages] CHECK CONSTRAINT [FK_Messages_Users_SenderId]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_Tasks_TaskId] FOREIGN KEY([TaskId])
REFERENCES [dbo].[Tasks] ([TaskId])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_Tasks_TaskId]
GO
ALTER TABLE [dbo].[Notifications]  WITH CHECK ADD  CONSTRAINT [FK_Notifications_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[Notifications] CHECK CONSTRAINT [FK_Notifications_Users_UserId]
GO
ALTER TABLE [dbo].[PaymentTransactions]  WITH CHECK ADD  CONSTRAINT [FK_PaymentTransactions_Packages_PackageId] FOREIGN KEY([PackageId])
REFERENCES [dbo].[Packages] ([PackageId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PaymentTransactions] CHECK CONSTRAINT [FK_PaymentTransactions_Packages_PackageId]
GO
ALTER TABLE [dbo].[PaymentTransactions]  WITH CHECK ADD  CONSTRAINT [FK_PaymentTransactions_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PaymentTransactions] CHECK CONSTRAINT [FK_PaymentTransactions_Users_UserId]
GO
ALTER TABLE [dbo].[Rankings]  WITH CHECK ADD  CONSTRAINT [FK_Rankings_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Rankings] CHECK CONSTRAINT [FK_Rankings_Users_UserId]
GO
ALTER TABLE [dbo].[Reactions]  WITH CHECK ADD  CONSTRAINT [FK_Reactions_Messages_MessageId] FOREIGN KEY([MessageId])
REFERENCES [dbo].[Messages] ([MessageId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reactions] CHECK CONSTRAINT [FK_Reactions_Messages_MessageId]
GO
ALTER TABLE [dbo].[Reactions]  WITH CHECK ADD  CONSTRAINT [FK_Reactions_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Reactions] CHECK CONSTRAINT [FK_Reactions_Users_UserId]
GO
ALTER TABLE [dbo].[RefreshToken]  WITH CHECK ADD  CONSTRAINT [FK_RefreshToken_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[RefreshToken] CHECK CONSTRAINT [FK_RefreshToken_Users]
GO
ALTER TABLE [dbo].[SupportTickets]  WITH CHECK ADD  CONSTRAINT [FK_SupportTickets_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SupportTickets] CHECK CONSTRAINT [FK_SupportTickets_Users_UserId]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Users_UserId]
GO
ALTER TABLE [dbo].[UserLikes]  WITH CHECK ADD  CONSTRAINT [FK_UserLikes_BlogPosts_BlogId] FOREIGN KEY([BlogId])
REFERENCES [dbo].[BlogPosts] ([PostId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserLikes] CHECK CONSTRAINT [FK_UserLikes_BlogPosts_BlogId]
GO
ALTER TABLE [dbo].[UserLikes]  WITH CHECK ADD  CONSTRAINT [FK_UserLikes_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
GO
ALTER TABLE [dbo].[UserLikes] CHECK CONSTRAINT [FK_UserLikes_Users_UserId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_PrivacySettings_PrivacySettingId] FOREIGN KEY([PrivacySettingId])
REFERENCES [dbo].[PrivacySettings] ([PrivacySettingId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_PrivacySettings_PrivacySettingId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Semesters_SemesterId] FOREIGN KEY([SemesterId])
REFERENCES [dbo].[Semesters] ([SemesterId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Semesters_SemesterId]
GO
ALTER TABLE [dbo].[UserSpecializations]  WITH CHECK ADD  CONSTRAINT [FK_UserSpecializations_Specializations_SpecializationId] FOREIGN KEY([SpecializationId])
REFERENCES [dbo].[Specializations] ([SpecializationId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserSpecializations] CHECK CONSTRAINT [FK_UserSpecializations_Specializations_SpecializationId]
GO
ALTER TABLE [dbo].[UserSpecializations]  WITH CHECK ADD  CONSTRAINT [FK_UserSpecializations_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserSpecializations] CHECK CONSTRAINT [FK_UserSpecializations_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [GOStudyContext] SET  READ_WRITE 
GO
