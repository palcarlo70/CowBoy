CREATE DATABASE [CowBoy] 
GO
ALTER DATABASE CowBoy MODIFY FILE --inserire il percorso nel NAME
( NAME = N'D:\PC\Microsoft SQL Server\DATA\CowBoy' , SIZE = 3048KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
GO
ALTER DATABASE CowBoy MODIFY FILE 
( NAME = N'D:\PC\Microsoft SQL Server\DATA\CowBoy_log' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

USE [CowBoy]
GO
/****** Object:  Table [dbo].[Anagrafica]    Script Date: 08/09/2014 17:53:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Anagrafica](
	[idAnagrafica] [int] IDENTITY(1,1) NOT NULL,
	[Nome] [nvarchar](120) NULL,
	[Madre] [int] NULL,
	[Padre] [int] NULL,
	[DataNascita] [date] NULL,
	[DataFine] [date] NULL,
	[Note] [nvarchar](max) NULL,
	[ToroDaMonta] [bit] NULL CONSTRAINT [DF_Anagrafica_ToroDaMonta]  DEFAULT ((0)),
	[ToroArtificiale] [bit] NULL CONSTRAINT [DF_Anagrafica_ToroArtificiale]  DEFAULT ((0)),
	[Sesso] [char](1) NULL,
	[MatricolaASL] [nvarchar](150) NULL,
	[MatricolaAzienda] [nvarchar](150) NULL,
	[idFiglio] [int] NULL,
 CONSTRAINT [PK_Anagrafica] PRIMARY KEY CLUSTERED 
(
	[idAnagrafica] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Foto]    Script Date: 08/09/2014 17:53:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Foto](
	[idFoto] [int] IDENTITY(1,1) NOT NULL,
	[idAnagrafica] [int] NOT NULL,
	[Nome] [nvarchar](150) NULL,
	[DataInserimento] [date] NULL,
	[Principale] [bit] NULL CONSTRAINT [DF_Foto_Principale]  DEFAULT ((0)),
 CONSTRAINT [PK_Foto] PRIMARY KEY CLUSTERED 
(
	[idFoto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PartiSalti]    Script Date: 08/09/2014 17:53:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PartiSalti](
	[idPartoSalto] [int] IDENTITY(1,1) NOT NULL,
	[idAnagrafica] [int] NOT NULL,
	[DataMessaAsciutta] [date] NULL,
	[DataParto] [date] NULL,
	[Note] [nvarchar](max) NULL,
	[Naturale] [bit] NULL,
	[Facile] [bit] NULL,
	[DaSola] [bit] NULL,
	[PartoNoParto] [nvarchar](6) NULL,
	[Abortito] [bit] NULL CONSTRAINT [DF_PartiSalti_Aborto]  DEFAULT ((0)),
 CONSTRAINT [PK_PartiSalti] PRIMARY KEY CLUSTERED 
(
	[idPartoSalto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Salti]    Script Date: 08/09/2014 17:53:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Salti](
	[idSalto] [int] IDENTITY(1,1) NOT NULL,
	[idPartoSalto] [int] NOT NULL,
	[DataSalto] [date] NULL,
	[idToro] [int] NULL,
	[SaltoArtificiale] [bit] NULL CONSTRAINT [DF_Salti_SaltoArtificiale]  DEFAULT ((0)),
	[MatrToroArt] [nvarchar](max) NULL,
	[Note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Salti] PRIMARY KEY CLUSTERED 
(
	[idSalto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Stati]    Script Date: 08/09/2014 17:53:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Stati](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Stato] [nvarchar](50) NULL,
 CONSTRAINT [PK_Stati] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Anagrafica]  WITH CHECK ADD  CONSTRAINT [FK_Anagrafica_IdMadre] FOREIGN KEY([Madre])
REFERENCES [dbo].[Anagrafica] ([idAnagrafica])
GO
ALTER TABLE [dbo].[Anagrafica] CHECK CONSTRAINT [FK_Anagrafica_IdMadre]
GO
ALTER TABLE [dbo].[Anagrafica]  WITH CHECK ADD  CONSTRAINT [FK_Anagrafica_IdPadre] FOREIGN KEY([Padre])
REFERENCES [dbo].[Anagrafica] ([idAnagrafica])
GO
ALTER TABLE [dbo].[Anagrafica] CHECK CONSTRAINT [FK_Anagrafica_IdPadre]
GO
ALTER TABLE [dbo].[Foto]  WITH CHECK ADD  CONSTRAINT [FK_Foto_Anagrafica] FOREIGN KEY([idAnagrafica])
REFERENCES [dbo].[Anagrafica] ([idAnagrafica])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Foto] CHECK CONSTRAINT [FK_Foto_Anagrafica]
GO
ALTER TABLE [dbo].[PartiSalti]  WITH CHECK ADD  CONSTRAINT [FK_PartiSalti_Anagrafica] FOREIGN KEY([idAnagrafica])
REFERENCES [dbo].[Anagrafica] ([idAnagrafica])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PartiSalti] CHECK CONSTRAINT [FK_PartiSalti_Anagrafica]
GO
ALTER TABLE [dbo].[Salti]  WITH CHECK ADD  CONSTRAINT [FK_Salti_Anagrafica] FOREIGN KEY([idToro])
REFERENCES [dbo].[Anagrafica] ([idAnagrafica])
GO
ALTER TABLE [dbo].[Salti] CHECK CONSTRAINT [FK_Salti_Anagrafica]
GO
ALTER TABLE [dbo].[Salti]  WITH CHECK ADD  CONSTRAINT [FK_Salti_PartiSalti] FOREIGN KEY([idPartoSalto])
REFERENCES [dbo].[PartiSalti] ([idPartoSalto])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Salti] CHECK CONSTRAINT [FK_Salti_PartiSalti]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Dati di fine carriera' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Anagrafica', @level2type=N'COLUMN',@level2name=N'DataFine'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Se uno significa che viene utilizzato per coprire le mucche' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Anagrafica', @level2type=N'COLUMN',@level2name=N'ToroDaMonta'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'F femmina M maschio' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Anagrafica', @level2type=N'COLUMN',@level2name=N'Sesso'
GO

--USE [CowBoy]
--GO
SET IDENTITY_INSERT [dbo].[Stati] ON 

GO
INSERT [dbo].[Stati] ([ID], [Stato]) VALUES (1, N'MANZA')
GO
INSERT [dbo].[Stati] ([ID], [Stato]) VALUES (2, N'MANZO')
GO
INSERT [dbo].[Stati] ([ID], [Stato]) VALUES (3, N'TORO')
GO
INSERT [dbo].[Stati] ([ID], [Stato]) VALUES (4, N'LATTAZIONE')
GO
INSERT [dbo].[Stati] ([ID], [Stato]) VALUES (5, N'ASCIUTTA')
GO
SET IDENTITY_INSERT [dbo].[Stati] OFF
GO
