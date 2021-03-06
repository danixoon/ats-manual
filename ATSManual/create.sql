USE [ATSManual]
GO
/****** Object:  Table [dbo].[Data]    Script Date: 20.12.2021 15:07:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Data](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[key] [varchar](10) NOT NULL,
	[name] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_Data_Key] UNIQUE NONCLUSTERED 
(
	[key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Department]    Script Date: 20.12.2021 15:07:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[parentDepartmentId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profile]    Script Date: 20.12.2021 15:07:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Profile](
	[id] [int] NOT NULL,
	[name] [nvarchar](50) NULL,
	[dectPhone] [int] NULL,
	[globalPhone] [int] NULL,
	[subscriberId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Subscriber]    Script Date: 20.12.2021 15:07:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscriber](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[phone] [int] NOT NULL,
	[name] [nvarchar](100) NOT NULL,
	[departmentId] [int] NULL,
	[description] [nvarchar](100) NULL,
	[statusType] [int] NOT NULL DEFAULT ((0)),
	[flags] [bit] NOT NULL CONSTRAINT [DF_Subscriber_isEmpty]  DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [AK_Subscriber_Phone] UNIQUE NONCLUSTERED 
(
	[phone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SubscriberData]    Script Date: 20.12.2021 15:07:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubscriberData](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[subscriberId] [int] NOT NULL,
	[dataId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[dataId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Department] FOREIGN KEY([parentDepartmentId])
REFERENCES [dbo].[Department] ([id])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Department]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_Subscriber] FOREIGN KEY([subscriberId])
REFERENCES [dbo].[Subscriber] ([id])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_Subscriber]
GO
ALTER TABLE [dbo].[Subscriber]  WITH CHECK ADD  CONSTRAINT [FK_Subscriber_Department] FOREIGN KEY([departmentId])
REFERENCES [dbo].[Department] ([id])
GO
ALTER TABLE [dbo].[Subscriber] CHECK CONSTRAINT [FK_Subscriber_Department]
GO
ALTER TABLE [dbo].[SubscriberData]  WITH CHECK ADD  CONSTRAINT [FK_SubscriberData_Data] FOREIGN KEY([dataId])
REFERENCES [dbo].[Data] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SubscriberData] CHECK CONSTRAINT [FK_SubscriberData_Data]
GO
ALTER TABLE [dbo].[SubscriberData]  WITH CHECK ADD  CONSTRAINT [FK_SubscriberData_Subscriber] FOREIGN KEY([subscriberId])
REFERENCES [dbo].[Subscriber] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SubscriberData] CHECK CONSTRAINT [FK_SubscriberData_Subscriber]
GO
ALTER TABLE [dbo].[Data]  WITH CHECK ADD  CONSTRAINT [CK_Data_Key] CHECK  ((datalength([key])>=(4) AND CONVERT([int],[key])=CONVERT([int],[key])))
GO
ALTER TABLE [dbo].[Data] CHECK CONSTRAINT [CK_Data_Key]
GO
