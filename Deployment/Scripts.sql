CREATE TABLE [dbo].[NLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MachineName] [nvarchar](200) NULL,
	[Logged] [datetime] NOT NULL,
	[Level] [varchar](5) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Logger] [nvarchar](300) NULL,
	[Properties] [nvarchar](max) NULL,
	[Callsite] [nvarchar](300) NULL,
	[Exception] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Log] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE PROCEDURE [dbo].[NLog_AddEntry_p] (
  @machineName nvarchar(200),
  @logged datetime,
  @level varchar(5),
  @message nvarchar(max),
  @logger nvarchar(300),
  @properties nvarchar(max),
  @callsite nvarchar(300),
  @exception nvarchar(max)
) AS
BEGIN
  INSERT INTO [dbo].[NLog] (
    [MachineName],
    [Logged],
    [Level],
    [Message],
    [Logger],
    [Properties],
    [Callsite],
    [Exception]
  ) VALUES (
    @machineName,
    @logged,
    @level,
    @message,
    @logger,
    @properties,
    @callsite,
    @exception
  );
END
GO

GO

CREATE TABLE [dbo].[DeliveryRegistrationHistory](
	[Id] [bigint] NOT NULL,
	[CIF] [nvarchar](32) NOT NULL,
	[ECRN] [varchar](32) NULL,
	[CRNN] [varchar](32) NULL,
	[FirstName] [varchar](300) NULL,
	[LastName] [nvarchar](100) NULL,
	[MiddleName] [nvarchar](100) NULL,
	[AcountNumber] [varchar](32) NULL,
	[CreditCardNumber] [varchar](32) NULL,
	[DeliveryMode] [varchar](100) NULL,
	[MobileNumber] [varchar](20) NULL,
	[FaxNumber] [varchar](50) NULL,
	[PrimaryEmail] [varchar](80) NOT NULL,
	[SecondaryEmail] [varchar](80) NULL,
	[SubForAccount] [bit] NULL,
	[SubForCreditCard] [bit] NULL,
	[SubForRemittance] [bit] NULL,
	[SubForDeposit] [bit] NULL,
	[SubForJointAccount] [bit] NULL,
	[SubForInv] [bit] NULL,
	[SubForTF] [bit] NULL,
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
	ActionCode [int] NOT NULL,
	HistoryDateTime DateTime not null
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[DeliveryRegistrationHistory] ADD  CONSTRAINT [DF_DeliveryRegistrationHistory_HistoryDateTime]  DEFAULT (GetDate()) FOR [HistoryDateTime]
GO

create trigger dbo.trg_DeliveryRegistrationUpdate
on [dbo].[DeliveryRegistration]
after update
as 
begin
	set nocount on
	INSERT INTO [dbo].[DeliveryRegistrationHistory]
           ([Id]
           ,[CIF]
           ,[ECRN]
           ,[CRNN]
           ,[FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[AcountNumber]
           ,[CreditCardNumber]
           ,[DeliveryMode]
           ,[MobileNumber]
           ,[FaxNumber]
           ,[PrimaryEmail]
           ,[SecondaryEmail]
           ,[SubForAccount]
           ,[SubForCreditCard]
           ,[SubForRemittance]
           ,[SubForDeposit]
           ,[SubForJointAccount]
           ,[SubForInv]
           ,[SubForTF]
           ,[MakerId]
           ,[CheckerId]
           ,[CreationDate]
           ,[LastModifictionDate]
           ,[ApprovalStatus]
           ,[ModeOfRegistration]
           ,[MasterNumber]
           ,[ModificationId]
           ,[Modifications]
           ,[RequestStatus]
           ,[Comments]
           ,[ActionCode])
		   select 
		   [Id]
           ,[CIF]
           ,[ECRN]
           ,[CRNN]
           ,[FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[AcountNumber]
           ,[CreditCardNumber]
           ,[DeliveryMode]
           ,[MobileNumber]
           ,[FaxNumber]
           ,[PrimaryEmail]
           ,[SecondaryEmail]
           ,[SubForAccount]
           ,[SubForCreditCard]
           ,[SubForRemittance]
           ,[SubForDeposit]
           ,[SubForJointAccount]
           ,[SubForInv]
           ,[SubForTF]
           ,[MakerId]
           ,[CheckerId]
           ,[CreationDate]
           ,[LastModifictionDate]
           ,[ApprovalStatus]
           ,[ModeOfRegistration]
           ,[MasterNumber]
           ,[ModificationId]
           ,[Modifications]
           ,[RequestStatus]
           ,[Comments]
		   ,2
		   from deleted
end
go

create trigger dbo.trg_DeliveryRegistrationDelete
on [dbo].[DeliveryRegistration]
after delete
as 
begin
	set nocount on
	INSERT INTO [dbo].[DeliveryRegistrationHistory]
           ([Id]
           ,[CIF]
           ,[ECRN]
           ,[CRNN]
           ,[FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[AcountNumber]
           ,[CreditCardNumber]
           ,[DeliveryMode]
           ,[MobileNumber]
           ,[FaxNumber]
           ,[PrimaryEmail]
           ,[SecondaryEmail]
           ,[SubForAccount]
           ,[SubForCreditCard]
           ,[SubForRemittance]
           ,[SubForDeposit]
           ,[SubForJointAccount]
           ,[SubForInv]
           ,[SubForTF]
           ,[MakerId]
           ,[CheckerId]
           ,[CreationDate]
           ,[LastModifictionDate]
           ,[ApprovalStatus]
           ,[ModeOfRegistration]
           ,[MasterNumber]
           ,[ModificationId]
           ,[Modifications]
           ,[RequestStatus]
           ,[Comments]
           ,[ActionCode])
		   select 
		   [Id]
           ,[CIF]
           ,[ECRN]
           ,[CRNN]
           ,[FirstName]
           ,[LastName]
           ,[MiddleName]
           ,[AcountNumber]
           ,[CreditCardNumber]
           ,[DeliveryMode]
           ,[MobileNumber]
           ,[FaxNumber]
           ,[PrimaryEmail]
           ,[SecondaryEmail]
           ,[SubForAccount]
           ,[SubForCreditCard]
           ,[SubForRemittance]
           ,[SubForDeposit]
           ,[SubForJointAccount]
           ,[SubForInv]
           ,[SubForTF]
           ,[MakerId]
           ,[CheckerId]
           ,[CreationDate]
           ,[LastModifictionDate]
           ,[ApprovalStatus]
           ,[ModeOfRegistration]
           ,[MasterNumber]
           ,[ModificationId]
           ,[Modifications]
           ,[RequestStatus]
           ,[Comments]
		   ,3
		   from deleted
end
go

--History for DeliveryRegistrationDetails
CREATE TABLE [dbo].[DeliveryRegistrationDetailsHistory](
	[ID] [bigint] NOT NULL,
	[CIFID] [nvarchar](32) NOT NULL,
	[DateOfBirth] [varchar](10) NULL,
	[TradeLicenseNumber] [varchar](50) NULL,
	[RetailCorporateIndicator] [char](1) NOT NULL,
	[CreationDate] [datetime] NOT NULL,
	[ModificationDate] [datetime] NOT NULL,
	ActionCode int not null,
	HistoryDateTime Datetime not null
) ON [PRIMARY]
GO

ALTER TABLE [dbo].DeliveryRegistrationDetailsHistory ADD  CONSTRAINT [DF_DeliveryRegistrationDetailsHistory_HistoryDateTime]  DEFAULT (GetDate()) FOR [HistoryDateTime]
GO

CREATE TRIGGER dbo.trg_DeliveryRegistrationDetailsHistoryUpdate
ON dbo.DeliveryRegistrationDetails
AFTER UPDATE
AS
BEGIN
	INSERT INTO [dbo].[DeliveryRegistrationDetailsHistory]
           ([ID]
           ,[CIFID]
           ,[DateOfBirth]
           ,[TradeLicenseNumber]
           ,[RetailCorporateIndicator]
           ,[CreationDate]
           ,[ModificationDate]
           ,[ActionCode])

	SELECT 
			[ID]
           ,[CIFID]
           ,[DateOfBirth]
           ,[TradeLicenseNumber]
           ,[RetailCorporateIndicator]
           ,[CreationDate]
           ,[ModificationDate]
		   ,2
	FROM deleted
END
go

CREATE TRIGGER dbo.trg_DeliveryRegistrationDetailsHistoryDelete
ON dbo.DeliveryRegistrationDetails
AFTER DELETE
AS BEGIN
	INSERT INTO [dbo].[DeliveryRegistrationDetailsHistory]
           ([ID]
           ,[CIFID]
           ,[DateOfBirth]
           ,[TradeLicenseNumber]
           ,[RetailCorporateIndicator]
           ,[CreationDate]
           ,[ModificationDate]
           ,[ActionCode])

	SELECT 
			[ID]
           ,[CIFID]
           ,[DateOfBirth]
           ,[TradeLicenseNumber]
           ,[RetailCorporateIndicator]
           ,[CreationDate]
           ,[ModificationDate]
		   ,3
	FROM deleted
END
GO