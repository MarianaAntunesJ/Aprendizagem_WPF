CREATE DATABASE [AprendizagemCSharp]
GO
USE [AprendizagemCSharp]
GO
/****** Object:  Table [dbo].[Cliente]    Script Date: 03/01/2021 18:46:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [varchar](50) NOT NULL,
	[CPF] [varchar](14) NOT NULL,
	[Email] [varchar](50) NULL,
	[DataNascimento] [date] NULL,
	[Ativo] [bit] NOT NULL,
	[Sexo] [varchar](15) NULL,
	[EstadoCivil] [varchar](15) NULL,
	[Imagem] [image] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Funcionario]    Script Date: 03/01/2021 18:46:53 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Funcionario](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Usuario] [varchar](25) NOT NULL,
	[Senha] [varchar](100) NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Funcionario] ON 
GO
INSERT [dbo].[Funcionario] ([Id], [Usuario], [Senha]) VALUES (1, N'admin', N'21232f297a57a5a743894a0e4a801fc3')
GO
SET IDENTITY_INSERT [dbo].[Funcionario] OFF
GO
