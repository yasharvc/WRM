USE [DMRAKPHASE3]
GO
/****** Object:  Table [dbo].[DeliveryRegistrationDetails]    Script Date: 09/18/2019 17:57:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeliveryRegistrationDetails](
	[ID] [bigint] IDENTITY(1,1) NOT NULL,
	[CIFID] [nvarchar](32) NOT NULL,
	[DateOfBirth] [varchar](10) NULL,
	[TradeLicenseNumber] [varchar](50) NULL,
	[RetailCorporateIndicator] [char](1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
 CONSTRAINT [PK_DeliveryRegistrationDetails] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[DeliveryRegistration]    Script Date: 09/18/2019 17:57:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DeliveryRegistration](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CIF] [nvarchar](32) NOT NULL,
	[ECRN] [varchar](32) NULL,
	[CRNN] [varchar](32) NULL,
	[FirstName] [varchar](300) NULL,
	[LastName] [nvarchar](100) NULL,
	[MiddleName] [nvarchar](100) NULL,
	[AcountNumber] [varchar](32) NULL,
	[CreditCardNumber] [varchar](32) NULL,
	[DeliveryMode] [varchar](100) NULL,		--
	[MobileNumber] [varchar](20) NULL,		--
	[FaxNumber] [varchar](50) NULL,			--FaxNum
	[PrimaryEmail] [varchar](80) NOT NULL,	--
	[SecondaryEmail] [varchar](80) NULL,	--
	[SubForAccount] [bit] NULL,				--
	[SubForCreditCard] [bit] NULL,			--SubforCC
	[SubForRemittance] [bit] NULL,			--
	[SubForDeposit] [bit] NULL,				--
	[SubForJointAccount] [bit] NULL,		
	[SubForInv] [bit] NULL,					--SubForInvestments
	[SubForTF] [bit] NULL,					--
	[MakerId] [varchar](70) NOT NULL,
	[CheckerId] [varchar](70) NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModifictionDate] [datetime] NOT NULL,
	[ApprovalStatus] [varchar](50) NOT NULL,
	[ModeOfRegistration] [varchar](20) NULL,
	[MasterNumber] [varchar](32) NULL,
	[ModificationId] [bigint] NULL,
	[Modifications] [int] NOT NULL,
	[RequestStatus] [varchar](10) NOT NULL,
	[Comments] [nvarchar](max) NULL,
 CONSTRAINT [PK_DeliveryRegistration] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'It is part of AccountNo. 6 digits starting from 4th index (zero based index). only applicable for account registrations.' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DeliveryRegistration', @level2type=N'COLUMN',@level2name=N'MasterNumber'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'The modification id from the modifications table. if no modification is done then its last modification id. for new registration it is Null' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'DeliveryRegistration', @level2type=N'COLUMN',@level2name=N'ModificationId'
GO
/****** Object:  Table [dbo].[PremiumBanking]    Script Date: 09/18/2019 17:57:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[PremiumBanking](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CIFID] [nvarchar](50) NULL,
	[PremiumCustomer] [char](1) NULL,
	[CustomerClassification] [nvarchar](1) NULL,
	[CreationDate] [datetime] NOT NULL,
	[LastModificationDate] [datetime] NULL,
 CONSTRAINT [PK_PrimiumBanking] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegistrationModification]    Script Date: 09/18/2019 17:57:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RegistrationModification](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RegistrationId] [bigint] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[AgentId] [varchar](50) NOT NULL,
	[FieldModified] [varchar](50) NOT NULL,
	[OldValue] [varchar](255) NULL,
	[NewValue] [varchar](255) NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Comments] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_RegistrationModification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PremiumModifications]    Script Date: 09/18/2019 17:57:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PremiumModifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PremiumId] [int] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[AgentId] [nvarchar](50) NOT NULL,
	[FieldModified] [nvarchar](50) NOT NULL,
	[OldValue] [nvarchar](50) NULL,
	[NewValue] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Comments] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_PremiumnModifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetailModifications]    Script Date: 09/18/2019 17:57:47 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetailModifications](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DetailsId] [bigint] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	[AgentId] [nvarchar](50) NOT NULL,
	[FieldModified] [nvarchar](50) NOT NULL,
	[OldValue] [nvarchar](50) NULL,
	[NewValue] [nvarchar](50) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Comments] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_DetailsModifications] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Default [DF_DeliveryRegistration_Modifications]    Script Date: 09/18/2019 17:57:47 ******/
ALTER TABLE [dbo].[DeliveryRegistration] ADD  CONSTRAINT [DF_DeliveryRegistration_Modifications]  DEFAULT ((0)) FOR [Modifications]
GO
/****** Object:  Default [DF_DeliveryRegistration_RequestStatus]    Script Date: 09/18/2019 17:57:47 ******/
ALTER TABLE [dbo].[DeliveryRegistration] ADD  CONSTRAINT [DF_DeliveryRegistration_RequestStatus]  DEFAULT ('SUC') FOR [RequestStatus]
GO
/****** Object:  Default [DF_RegistrationModification_Status]    Script Date: 09/18/2019 17:57:47 ******/
ALTER TABLE [dbo].[RegistrationModification] ADD  CONSTRAINT [DF_RegistrationModification_Status]  DEFAULT (N'PendingModification') FOR [Status]
GO
/****** Object:  ForeignKey [FK_DetailModifications_DetailModifications]    Script Date: 09/18/2019 17:57:47 ******/
ALTER TABLE [dbo].[DetailModifications]  WITH CHECK ADD  CONSTRAINT [FK_DetailModifications_DetailModifications] FOREIGN KEY([DetailsId])
REFERENCES [dbo].[DeliveryRegistrationDetails] ([ID])
GO
ALTER TABLE [dbo].[DetailModifications] CHECK CONSTRAINT [FK_DetailModifications_DetailModifications]
GO
/****** Object:  ForeignKey [FK_PremiumModifications_PremiumBanking]    Script Date: 09/18/2019 17:57:47 ******/
ALTER TABLE [dbo].[PremiumModifications]  WITH NOCHECK ADD  CONSTRAINT [FK_PremiumModifications_PremiumBanking] FOREIGN KEY([PremiumId])
REFERENCES [dbo].[PremiumBanking] ([Id])
GO
ALTER TABLE [dbo].[PremiumModifications] CHECK CONSTRAINT [FK_PremiumModifications_PremiumBanking]
GO
