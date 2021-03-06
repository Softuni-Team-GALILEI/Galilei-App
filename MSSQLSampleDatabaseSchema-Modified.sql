USE [master]
GO
/****** Object:  Database [Supermarket]    Script Date: 12.3.2015 г. 22:50:52 ч. ******/
CREATE DATABASE [Supermarket]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Supermarket', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Supermarket.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Supermarket_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\Supermarket_log.ldf' , SIZE = 1072KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Supermarket] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Supermarket].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Supermarket] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Supermarket] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Supermarket] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Supermarket] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Supermarket] SET ARITHABORT OFF 
GO
ALTER DATABASE [Supermarket] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Supermarket] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Supermarket] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Supermarket] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Supermarket] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Supermarket] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Supermarket] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Supermarket] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Supermarket] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Supermarket] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Supermarket] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Supermarket] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Supermarket] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Supermarket] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Supermarket] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Supermarket] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Supermarket] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Supermarket] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Supermarket] SET  MULTI_USER 
GO
ALTER DATABASE [Supermarket] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Supermarket] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Supermarket] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Supermarket] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [Supermarket] SET DELAYED_DURABILITY = DISABLED 
GO
USE [Supermarket]
GO
/****** Object:  Table [dbo].[Expenses]    Script Date: 12.3.2015 г. 22:50:52 ч. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Expenses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorID] [int] NULL,
	[Time] [datetime] NULL,
	[ExpenseSum] [money] NULL,
 CONSTRAINT [PK_Expenses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Measures]    Script Date: 12.3.2015 г. 22:50:52 ч. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Measures](
	[ID] [int] IDENTITY(100,100) NOT NULL,
	[Measure Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__Measures__3214EC273ED9ABCB] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 12.3.2015 г. 22:50:52 ч. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorID] [int] NOT NULL,
	[Product Name] [nvarchar](100) NULL,
	[MeasureID] [int] NULL,
	[Price] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sales]    Script Date: 12.3.2015 г. 22:50:52 ч. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sales](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorID] [int] NOT NULL,
	[ProductID] [int] NULL,
	[Quantity] [int] NULL,
	[PriceSum] [money] NULL,
	[Date] [datetime] NULL,
	[SupermarketID] [int] NULL,
 CONSTRAINT [PK_Sales] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Supermarkets]    Script Date: 12.3.2015 г. 22:50:52 ч. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supermarkets](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Supermarkets] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Vendors]    Script Date: 12.3.2015 г. 22:50:52 ч. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendors](
	[ID] [int] IDENTITY(10,10) NOT NULL,
	[Vendor Name] [nvarchar](100) NULL
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Expenses]  WITH CHECK ADD  CONSTRAINT [FK_Expenses_Vendors] FOREIGN KEY([VendorID])
REFERENCES [dbo].[Vendors] ([ID])
GO
ALTER TABLE [dbo].[Expenses] CHECK CONSTRAINT [FK_Expenses_Vendors]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Measures] FOREIGN KEY([MeasureID])
REFERENCES [dbo].[Measures] ([ID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Measures]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Vendors] FOREIGN KEY([VendorID])
REFERENCES [dbo].[Vendors] ([ID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Vendors]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Products] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ID])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Products]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Supermarkets] FOREIGN KEY([SupermarketID])
REFERENCES [dbo].[Supermarkets] ([ID])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Supermarkets]
GO
ALTER TABLE [dbo].[Sales]  WITH CHECK ADD  CONSTRAINT [FK_Sales_Vendors] FOREIGN KEY([VendorID])
REFERENCES [dbo].[Vendors] ([ID])
GO
ALTER TABLE [dbo].[Sales] CHECK CONSTRAINT [FK_Sales_Vendors]
GO
USE [master]
GO
ALTER DATABASE [Supermarket] SET  READ_WRITE 
GO
