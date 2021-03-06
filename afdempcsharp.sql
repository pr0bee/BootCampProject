USE [master]
GO
/****** Object:  Database [afdemp_csharp_1]    Script Date: 17/1/2018 9:25:51 μμ ******/
CREATE DATABASE [afdemp_csharp_1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'afdemp_csharp_1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\afdemp_csharp_1.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'afdemp_csharp_1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\afdemp_csharp_1_log.ldf' , SIZE = 2112KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [afdemp_csharp_1] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [afdemp_csharp_1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [afdemp_csharp_1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET ARITHABORT OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [afdemp_csharp_1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [afdemp_csharp_1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [afdemp_csharp_1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET  DISABLE_BROKER 
GO
ALTER DATABASE [afdemp_csharp_1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [afdemp_csharp_1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [afdemp_csharp_1] SET  MULTI_USER 
GO
ALTER DATABASE [afdemp_csharp_1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [afdemp_csharp_1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [afdemp_csharp_1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [afdemp_csharp_1] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [afdemp_csharp_1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [afdemp_csharp_1] SET QUERY_STORE = OFF
GO
USE [afdemp_csharp_1]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [afdemp_csharp_1]
GO
/****** Object:  Table [dbo].[accounts]    Script Date: 17/1/2018 9:25:51 μμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[accounts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_id] [int] NOT NULL,
	[transaction_date] [datetime] NOT NULL,
	[amount] [money] NOT NULL,
 CONSTRAINT [PK_accounts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 17/1/2018 9:25:51 μμ ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](30) NOT NULL,
	[password] [varchar](30) NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[accounts] ON 

INSERT [dbo].[accounts] ([id], [user_id], [transaction_date], [amount]) VALUES (1, 1, CAST(N'2017-11-13T19:28:47.000' AS DateTime), 100000.0000)
INSERT [dbo].[accounts] ([id], [user_id], [transaction_date], [amount]) VALUES (2, 2, CAST(N'2017-11-13T19:29:06.000' AS DateTime), 1000.0000)
INSERT [dbo].[accounts] ([id], [user_id], [transaction_date], [amount]) VALUES (3, 3, CAST(N'2017-11-13T19:29:15.000' AS DateTime), 1000.0000)
SET IDENTITY_INSERT [dbo].[accounts] OFF
SET IDENTITY_INSERT [dbo].[users] ON 

INSERT [dbo].[users] ([id], [username], [password]) VALUES (1, N'admin', N'admin')
INSERT [dbo].[users] ([id], [username], [password]) VALUES (2, N'user1', N'password1')
INSERT [dbo].[users] ([id], [username], [password]) VALUES (3, N'user2', N'password2')
SET IDENTITY_INSERT [dbo].[users] OFF
ALTER TABLE [dbo].[accounts]  WITH CHECK ADD  CONSTRAINT [FK_accounts_users] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[accounts] CHECK CONSTRAINT [FK_accounts_users]
GO
USE [master]
GO
ALTER DATABASE [afdemp_csharp_1] SET  READ_WRITE 
GO
