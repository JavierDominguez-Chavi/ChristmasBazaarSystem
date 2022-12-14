USE [master]
GO
/****** Object:  Database [ChristmasBazaar]    Script Date: 31/10/2022 08:21:55 a. m. ******/
CREATE DATABASE [ChristmasBazaar]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ChristmasBazaar', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ChristmasBazaar.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ChristmasBazaar_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ChristmasBazaar_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ChristmasBazaar] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ChristmasBazaar].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ChristmasBazaar] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET ARITHABORT OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ChristmasBazaar] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ChristmasBazaar] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ChristmasBazaar] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ChristmasBazaar] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ChristmasBazaar] SET  MULTI_USER 
GO
ALTER DATABASE [ChristmasBazaar] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ChristmasBazaar] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ChristmasBazaar] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ChristmasBazaar] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ChristmasBazaar] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ChristmasBazaar] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ChristmasBazaar] SET QUERY_STORE = OFF
GO
USE [ChristmasBazaar]
GO
/****** Object:  User [ChristmasBazaarUser]    Script Date: 31/10/2022 08:21:55 a. m. ******/
CREATE USER [ChristmasBazaarUser] FOR LOGIN [ChristmasBazaarUser] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [ChristmasBazaarUser]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 31/10/2022 08:21:55 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedidos](
	[idPedido] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [date] NOT NULL,
	[total] [float] NOT NULL,
	[nombre] [nvarchar](50) NOT NULL,
	[direccion] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Pedidos] PRIMARY KEY CLUSTERED 
(
	[idPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PedidosProductos]    Script Date: 31/10/2022 08:21:55 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidosProductos](
	[idPedido] [int] NOT NULL,
	[idProducto] [int] NOT NULL,
	[cantidad] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 31/10/2022 08:21:55 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[idProducto] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nvarchar](100) NOT NULL,
	[precio] [float] NOT NULL,
	[existencia] [int] NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[idProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[PedidosProductos]  WITH CHECK ADD  CONSTRAINT [FK_PedidosProductos_Pedidos] FOREIGN KEY([idPedido])
REFERENCES [dbo].[Pedidos] ([idPedido])
GO
ALTER TABLE [dbo].[PedidosProductos] CHECK CONSTRAINT [FK_PedidosProductos_Pedidos]
GO
ALTER TABLE [dbo].[PedidosProductos]  WITH CHECK ADD  CONSTRAINT [FK_PedidosProductos_Productos] FOREIGN KEY([idProducto])
REFERENCES [dbo].[Productos] ([idProducto])
GO
ALTER TABLE [dbo].[PedidosProductos] CHECK CONSTRAINT [FK_PedidosProductos_Productos]
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_pedido]    Script Date: 31/10/2022 08:21:55 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_insert_pedido]
	-- Add the parameters for the stored procedure here
	@Fecha Date,
	@Total float,
	@Nombre nvarchar(50),
	@Direccion nvarchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO Pedidos VALUES (@Fecha, @Total, @Nombre, @Direccion)

	SELECT IDENT_CURRENT('Pedidos') AS insertedIdPedido;


END
GO
/****** Object:  StoredProcedure [dbo].[sp_insert_pedidosproductos]    Script Date: 31/10/2022 08:21:55 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[sp_insert_pedidosproductos]
	-- Add the parameters for the stored procedure here
	@idPedido int,
	@idProducto int,
	@cantidad int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO PedidosProductos VALUES (@idPedido, @idProducto, @cantidad);
	UPDATE Productos SET existencia -= @cantidad WHERE idProducto = @idProducto;
END
GO
USE [master]
GO
ALTER DATABASE [ChristmasBazaar] SET  READ_WRITE 
GO
