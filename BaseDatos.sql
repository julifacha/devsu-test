USE [master]
GO
/****** Object:  Database [DevsuDB]    Script Date: 8/14/2023 9:42:00 PM ******/
CREATE DATABASE [DevsuDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DevsuDB', FILENAME = N'D:\Soft\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DevsuDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DevsuDB_log', FILENAME = N'D:\Soft\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\DevsuDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [DevsuDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DevsuDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DevsuDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DevsuDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DevsuDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DevsuDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DevsuDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DevsuDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DevsuDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DevsuDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DevsuDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DevsuDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DevsuDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DevsuDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DevsuDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DevsuDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DevsuDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DevsuDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DevsuDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DevsuDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DevsuDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DevsuDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DevsuDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DevsuDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DevsuDB] SET RECOVERY FULL 
GO
ALTER DATABASE [DevsuDB] SET  MULTI_USER 
GO
ALTER DATABASE [DevsuDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DevsuDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DevsuDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DevsuDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DevsuDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DevsuDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DevsuDB', N'ON'
GO
ALTER DATABASE [DevsuDB] SET QUERY_STORE = OFF
GO
USE [DevsuDB]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 8/14/2023 9:42:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[Id] [int] NOT NULL,
	[ClienteId] [uniqueidentifier] NOT NULL,
	[ContraseñaHash] [varbinary](255) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuentas]    Script Date: 8/14/2023 9:42:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClienteId] [int] NOT NULL,
	[NumeroCuenta] [int] NOT NULL,
	[TipoCuenta] [nvarchar](20) NOT NULL,
	[SaldoInicial] [decimal](18, 2) NOT NULL,
	[Estado] [bit] NOT NULL,
 CONSTRAINT [PK_Cuentas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NumeroCuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Movimientos]    Script Date: 8/14/2023 9:42:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Movimientos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CuentaId] [int] NOT NULL,
	[Fecha] [smalldatetime] NOT NULL,
	[TipoMovimiento] [nvarchar](20) NOT NULL,
	[Valor] [decimal](18, 2) NOT NULL,
	[Saldo] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Movimientos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personas]    Script Date: 8/14/2023 9:42:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Genero] [char](1) NOT NULL,
	[FechaNacimiento] [date] NOT NULL,
	[Identificacion] [nvarchar](50) NOT NULL,
	[Direccion] [nvarchar](100) NOT NULL,
	[Telefono] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Personas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Identificacion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Clientes]  WITH CHECK ADD  CONSTRAINT [FK_Clientes_Personas_Id] FOREIGN KEY([Id])
REFERENCES [dbo].[Personas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Clientes] CHECK CONSTRAINT [FK_Clientes_Personas_Id]
GO
ALTER TABLE [dbo].[Cuentas]  WITH CHECK ADD  CONSTRAINT [FK_Cuentas_Cliente_Id] FOREIGN KEY([ClienteId])
REFERENCES [dbo].[Clientes] ([Id])
GO
ALTER TABLE [dbo].[Cuentas] CHECK CONSTRAINT [FK_Cuentas_Cliente_Id]
GO
ALTER TABLE [dbo].[Movimientos]  WITH CHECK ADD  CONSTRAINT [FK_Movimientos_Cuentas_Id] FOREIGN KEY([CuentaId])
REFERENCES [dbo].[Cuentas] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Movimientos] CHECK CONSTRAINT [FK_Movimientos_Cuentas_Id]
GO
USE [master]
GO
ALTER DATABASE [DevsuDB] SET  READ_WRITE 
GO
