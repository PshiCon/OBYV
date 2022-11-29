USE [master]
GO
/****** Object:  Database [OOO Obyv]    Script Date: 23.11.2022 18:33:02 ******/
CREATE DATABASE [OOO Obyv]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'OOO Obyv', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\OOO Obyv.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'OOO Obyv_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\OOO Obyv_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [OOO Obyv] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [OOO Obyv].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [OOO Obyv] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [OOO Obyv] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [OOO Obyv] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [OOO Obyv] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [OOO Obyv] SET ARITHABORT OFF 
GO
ALTER DATABASE [OOO Obyv] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [OOO Obyv] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [OOO Obyv] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [OOO Obyv] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [OOO Obyv] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [OOO Obyv] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [OOO Obyv] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [OOO Obyv] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [OOO Obyv] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [OOO Obyv] SET  DISABLE_BROKER 
GO
ALTER DATABASE [OOO Obyv] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [OOO Obyv] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [OOO Obyv] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [OOO Obyv] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [OOO Obyv] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [OOO Obyv] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [OOO Obyv] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [OOO Obyv] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [OOO Obyv] SET  MULTI_USER 
GO
ALTER DATABASE [OOO Obyv] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [OOO Obyv] SET DB_CHAINING OFF 
GO
ALTER DATABASE [OOO Obyv] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [OOO Obyv] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [OOO Obyv] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [OOO Obyv] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [OOO Obyv] SET QUERY_STORE = OFF
GO
USE [OOO Obyv]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 23.11.2022 18:33:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 23.11.2022 18:33:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserLastName] [nvarchar](100) NOT NULL,
	[UserName] [nvarchar](100) NOT NULL,
	[UserMiddleName] [nvarchar](100) NOT NULL,
	[UserLogin] [nvarchar](100) NOT NULL,
	[UserPassword] [nvarchar](100) NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (1, N'Администратор')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (2, N'Менеджер')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (3, N'Клиент')
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserID], [UserLastName], [UserName], [UserMiddleName], [UserLogin], [UserPassword], [RoleID]) VALUES (1, N'Федоров', N'Глеб', N'Михайлович', N'o@outlook.com', N'2L6KZG', 1)
INSERT [dbo].[User] ([UserID], [UserLastName], [UserName], [UserMiddleName], [UserLogin], [UserPassword], [RoleID]) VALUES (2, N'Семенова', N'Софья', N'Дмитриевна', N'hr6zdl@yandex.ru', N'uzWC67', 1)
INSERT [dbo].[User] ([UserID], [UserLastName], [UserName], [UserMiddleName], [UserLogin], [UserPassword], [RoleID]) VALUES (3, N'Васильев', N'Егор', N'Германович', N'kaft93x@outlook.com', N'8ntwUp', 1)
INSERT [dbo].[User] ([UserID], [UserLastName], [UserName], [UserMiddleName], [UserLogin], [UserPassword], [RoleID]) VALUES (4, N'Смирнова', N'Ирина', N'Александровна', N'dcu@yandex.ru', N'YOyhfR', 2)
INSERT [dbo].[User] ([UserID], [UserLastName], [UserName], [UserMiddleName], [UserLogin], [UserPassword], [RoleID]) VALUES (5, N'Петров', N'Андрей', N'Владимирович', N'19dn@outlook.com', N'RSbvHv', 2)
INSERT [dbo].[User] ([UserID], [UserLastName], [UserName], [UserMiddleName], [UserLogin], [UserPassword], [RoleID]) VALUES (6, N'Егоров', N'Максим', N'Андреевич', N'pa5h@mail.ru', N'rwVDh9', 2)
INSERT [dbo].[User] ([UserID], [UserLastName], [UserName], [UserMiddleName], [UserLogin], [UserPassword], [RoleID]) VALUES (7, N'Никитин', N'Артур', N'Алексеевич', N'281av0@gmail.com', N'LdNyos', 3)
INSERT [dbo].[User] ([UserID], [UserLastName], [UserName], [UserMiddleName], [UserLogin], [UserPassword], [RoleID]) VALUES (8, N'Киселев', N'Максим', N'Сергеевич', N'8edmfh@outlook.com', N'gynQMT', 3)
INSERT [dbo].[User] ([UserID], [UserLastName], [UserName], [UserMiddleName], [UserLogin], [UserPassword], [RoleID]) VALUES (9, N'Борисов', N'Тимур', N'Егорович', N'sfn13i@mail.ru', N'AtnDjr', 3)
INSERT [dbo].[User] ([UserID], [UserLastName], [UserName], [UserMiddleName], [UserLogin], [UserPassword], [RoleID]) VALUES (10, N'Климов', N'Арсений', N'Тимурович', N'g0orc3x1@outlook.com', N'JlFRCZ', 3)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [OOO Obyv] SET  READ_WRITE 
GO
