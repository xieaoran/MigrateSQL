USE [TelecomData]
GO

/****** Object:  Table [dbo].[Ads]    Script Date: 2016/9/29 5:58:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Ads](
	[Id] [varchar](40) NOT NULL,
	[UserType] [tinyint] NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL,
 CONSTRAINT [PK_dbo.Ads] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[SIds](
	[Id] [varchar](40) NOT NULL,
	[City] [varchar](16) NULL,
	[Street] [nvarchar](16) NULL,
 CONSTRAINT [PK_dbo.SIds] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[PhoneBrowsings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Time] [datetime] NOT NULL,
	[Mobile] [varchar](24) NULL,
	[Site] [varchar](128) NULL,
	[Generation] [smallint] NOT NULL,
	[Ad_Id] [varchar](40) NULL,
	[SId_Id] [varchar](40) NULL,
 CONSTRAINT [PK_dbo.PhoneBrowsings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[PhoneBrowsings]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.PhoneBrowsings_dbo.Ads_Ad_Id] FOREIGN KEY([Ad_Id])
REFERENCES [dbo].[Ads] ([Id])
GO

ALTER TABLE [dbo].[PhoneBrowsings] CHECK CONSTRAINT [FK_dbo.PhoneBrowsings_dbo.Ads_Ad_Id]
GO

ALTER TABLE [dbo].[PhoneBrowsings]  WITH NOCHECK ADD  CONSTRAINT [FK_dbo.PhoneBrowsings_dbo.SIds_SId_Id] FOREIGN KEY([SId_Id])
REFERENCES [dbo].[SIds] ([Id])
GO

ALTER TABLE [dbo].[PhoneBrowsings] CHECK CONSTRAINT [FK_dbo.PhoneBrowsings_dbo.SIds_SId_Id]
GO
