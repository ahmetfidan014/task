USE [master]
GO
/****** Object:  Database [TaskDB]    Script Date: 4.01.2021 00:08:49 ******/
CREATE DATABASE [TaskDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TaskDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\TaskDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'TaskDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\TaskDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [TaskDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TaskDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TaskDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TaskDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TaskDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TaskDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TaskDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [TaskDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TaskDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TaskDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TaskDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TaskDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TaskDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TaskDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TaskDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TaskDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TaskDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TaskDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TaskDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TaskDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TaskDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TaskDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TaskDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TaskDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TaskDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TaskDB] SET  MULTI_USER 
GO
ALTER DATABASE [TaskDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TaskDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TaskDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TaskDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [TaskDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [TaskDB] SET QUERY_STORE = OFF
GO
USE [TaskDB]
GO
/****** Object:  Table [dbo].[Photos]    Script Date: 4.01.2021 00:08:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Vid] [int] NOT NULL,
	[Sort] [int] NOT NULL,
	[Flag] [int] NOT NULL,
	[Datetime] [datetime] NOT NULL,
	[Url] [nvarchar](150) NOT NULL,
	[Path] [nvarchar](350) NOT NULL,
	[ProductId] [int] NOT NULL,
 CONSTRAINT [PK_Photos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 4.01.2021 00:08:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Vid] [int] NOT NULL,
	[Sort] [int] NOT NULL,
	[Flag] [int] NOT NULL,
	[Datetime] [datetime] NOT NULL,
	[Url] [nvarchar](150) NOT NULL,
	[ByWhoId] [int] NOT NULL,
	[ProductName] [nvarchar](150) NOT NULL,
	[Barcode] [nvarchar](13) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Count] [int] NOT NULL,
	[Detail] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 4.01.2021 00:08:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Vid] [int] NOT NULL,
	[Sort] [int] NOT NULL,
	[Flag] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Url] [nvarchar](150) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Password] [nvarchar](150) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Surname] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Photos] ON 

INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (52, 1, 1, 1, CAST(N'2021-01-03T21:11:06.683' AS DateTime), N'01010621090311', N'/public/upload/img/test-urun-1/01.png', 17)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (53, 2, 2, 1, CAST(N'2021-01-03T21:11:06.710' AS DateTime), N'02010621090311', N'/public/upload/img/test-urun-1/02.png', 17)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (54, 3, 3, 1, CAST(N'2021-01-03T21:11:06.720' AS DateTime), N'03010621090311', N'/public/upload/img/test-urun-1/03.png', 17)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (55, 4, 4, 1, CAST(N'2021-01-03T21:11:40.657' AS DateTime), N'10014021090311', N'/public/upload/img/test-urun-2/10.png', 18)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (56, 5, 5, 1, CAST(N'2021-01-03T21:11:40.670' AS DateTime), N'11014021090311', N'/public/upload/img/test-urun-2/11.png', 18)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (57, 6, 6, 1, CAST(N'2021-01-03T21:11:40.680' AS DateTime), N'12014021090311', N'/public/upload/img/test-urun-2/12.png', 18)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (58, 7, 7, 1, CAST(N'2021-01-03T21:12:06.507' AS DateTime), N'13010621090312', N'/public/upload/img/test-urun-3/13.png', 19)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (59, 8, 8, 1, CAST(N'2021-01-03T21:12:06.520' AS DateTime), N'14010621090312', N'/public/upload/img/test-urun-3/14.png', 19)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (60, 9, 9, 1, CAST(N'2021-01-03T21:12:34.313' AS DateTime), N'07013421090312', N'/public/upload/img/test-urun-4/07.png', 20)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (61, 10, 10, 1, CAST(N'2021-01-03T21:12:34.323' AS DateTime), N'08013421090312', N'/public/upload/img/test-urun-4/08.png', 20)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (62, 11, 11, 1, CAST(N'2021-01-03T21:12:34.333' AS DateTime), N'09013421090312', N'/public/upload/img/test-urun-4/09.png', 20)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (63, 12, 12, 1, CAST(N'2021-01-03T21:13:07.697' AS DateTime), N'04010721090313', N'/public/upload/img/test-urun-5/04.png', 21)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (64, 13, 13, 1, CAST(N'2021-01-03T21:13:07.710' AS DateTime), N'05010721090313', N'/public/upload/img/test-urun-5/05.png', 21)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (65, 14, 14, 1, CAST(N'2021-01-03T21:13:07.720' AS DateTime), N'06010721090313', N'/public/upload/img/test-urun-5/06.png', 21)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (66, 15, 15, 1, CAST(N'2021-01-03T22:10:35.473' AS DateTime), N'03013521100310', N'/public/upload/img/test-urun-6/03.png', 22)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (67, 16, 16, 1, CAST(N'2021-01-03T22:10:35.500' AS DateTime), N'05013521100310', N'/public/upload/img/test-urun-6/05.png', 22)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (68, 17, 17, 1, CAST(N'2021-01-04T00:05:48.713' AS DateTime), N'03014821120405', N'/public/upload/img/test-urun-7/03.png', 23)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (69, 18, 18, 1, CAST(N'2021-01-04T00:05:48.730' AS DateTime), N'04014821120405', N'/public/upload/img/test-urun-7/04.png', 23)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (70, 19, 19, 1, CAST(N'2021-01-04T00:05:48.740' AS DateTime), N'13014821120405', N'/public/upload/img/test-urun-7/13.png', 23)
INSERT [dbo].[Photos] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [Path], [ProductId]) VALUES (71, 20, 20, 2, CAST(N'2021-01-04T00:06:43.963' AS DateTime), N'09-kopya-kopya014321120406', N'/public/upload/img/test/09-kopya-kopya.png', 24)
SET IDENTITY_INSERT [dbo].[Photos] OFF
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [ByWhoId], [ProductName], [Barcode], [Price], [Count], [Detail]) VALUES (17, 1, 1, 1, CAST(N'2021-01-03T21:11:06.530' AS DateTime), N'test-urun-1010621090311', 1, N'TEST ÜRÜN-1', N'8687963221762', CAST(30.00 AS Decimal(18, 2)), 22, N'Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500''lerden beri endüstri standardı sahte metinler olarak kullanılmıştır.')
INSERT [dbo].[Product] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [ByWhoId], [ProductName], [Barcode], [Price], [Count], [Detail]) VALUES (18, 2, 2, 1, CAST(N'2021-01-03T21:11:40.643' AS DateTime), N'test-urun-2014021090311', 1, N'TEST ÜRÜN-2', N'8685682854653', CAST(40.00 AS Decimal(18, 2)), 45, N'Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500''lerden beri endüstri standardı sahte metinler olarak kullanılmıştır.')
INSERT [dbo].[Product] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [ByWhoId], [ProductName], [Barcode], [Price], [Count], [Detail]) VALUES (19, 3, 3, 1, CAST(N'2021-01-03T21:12:06.497' AS DateTime), N'test-urun-3010621090312', 1, N'TEST ÜRÜN-3', N'8684286507271', CAST(45.00 AS Decimal(18, 2)), 56, N'Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500''lerden beri endüstri standardı sahte metinler olarak kullanılmıştır.')
INSERT [dbo].[Product] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [ByWhoId], [ProductName], [Barcode], [Price], [Count], [Detail]) VALUES (20, 4, 4, 1, CAST(N'2021-01-03T21:12:34.303' AS DateTime), N'test-urun-4013421090312', 1, N'TEST ÜRÜN-4', N'8685580410772', CAST(12.00 AS Decimal(18, 2)), 10, N'Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500''lerden beri endüstri standardı sahte metinler olarak kullanılmıştır.')
INSERT [dbo].[Product] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [ByWhoId], [ProductName], [Barcode], [Price], [Count], [Detail]) VALUES (21, 5, 5, 1, CAST(N'2021-01-03T21:13:07.683' AS DateTime), N'test-urun-5010721090313', 1, N'TEST ÜRÜN-5', N'8686721897692', CAST(1000.00 AS Decimal(18, 2)), 99, N'Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500''lerden beri endüstri standardı sahte metinler olarak kullanılmıştır.')
INSERT [dbo].[Product] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [ByWhoId], [ProductName], [Barcode], [Price], [Count], [Detail]) VALUES (22, 6, 6, 1, CAST(N'2021-01-03T22:10:35.273' AS DateTime), N'test-urun-6013521100310', 1, N'TEST ÜRÜN-6', N'8685302213043', CAST(44.00 AS Decimal(18, 2)), 45, N'Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500''lerden beri endüstri standardı sahte metinler olarak kullanılmıştır.')
INSERT [dbo].[Product] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [ByWhoId], [ProductName], [Barcode], [Price], [Count], [Detail]) VALUES (23, 7, 7, 1, CAST(N'2021-01-04T00:05:48.580' AS DateTime), N'test-urun-7014821120405', 1, N'TEST ÜRÜN-7', N'8682697229086', CAST(12.00 AS Decimal(18, 2)), 66, N'Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500''lerden beri endüstri standardı sahte metinler olarak kullanılmıştır.')
INSERT [dbo].[Product] ([Id], [Vid], [Sort], [Flag], [Datetime], [Url], [ByWhoId], [ProductName], [Barcode], [Price], [Count], [Detail]) VALUES (24, 8, 8, 2, CAST(N'2021-01-04T00:06:43.947' AS DateTime), N'test014321120406', 1, N'test', N'8688701941999', CAST(144.00 AS Decimal(18, 2)), 44, N'Lorem Ipsum, dizgi ve baskı endüstrisinde kullanılan mıgır metinlerdir. Lorem Ipsum, adı bilinmeyen bir matbaacının bir hurufat numune kitabı oluşturmak üzere bir yazı galerisini alarak karıştırdığı 1500''lerden beri endüstri standardı sahte metinler olarak kullanılmıştır.')
SET IDENTITY_INSERT [dbo].[Product] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Vid], [Sort], [Flag], [Date], [Url], [Email], [Password], [Name], [Surname]) VALUES (1, 1, 1, 1, CAST(N'2021-01-03T00:00:00.000' AS DateTime), N'taskinveon', N'task@inveon', N'dKScaY29PBLjawsodEfYM/dPOTf/Ey6/9wVLqhhiPDWnBbsYuC4qwDhLUSfblwFuY2CfcSvJDjUGz76pdZn0bzE=', N'ahmet', N'fidan')
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Photos]  WITH CHECK ADD  CONSTRAINT [FK__Photos__ProductI__46E78A0C] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([Id])
GO
ALTER TABLE [dbo].[Photos] CHECK CONSTRAINT [FK__Photos__ProductI__46E78A0C]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD FOREIGN KEY([ByWhoId])
REFERENCES [dbo].[User] ([Id])
GO
USE [master]
GO
ALTER DATABASE [TaskDB] SET  READ_WRITE 
GO
