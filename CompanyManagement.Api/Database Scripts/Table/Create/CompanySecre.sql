GO
CREATE TABLE [dbo].[CompanySecret](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [int] NOT NULL,
	[ServiceName] [nvarchar](50) NOT NULL,
	[AccountSID] [nvarchar](max) NOT NULL,
	[AuthToken] [nvarchar](max) NOT NULL,
	[FromNumber] [varchar](255) NULL,
	[SortCode] [varchar](50) NULL,
	[APIKey] [varchar](200) NULL,
	[SenderId] [varchar](50) NULL,
	[URLLink] [varchar](max) NULL,
	[SMTPServerAddress] [varchar](500) NULL,
	[MailSendPort] [varchar](50) NULL,
	[FromEmailId] [varchar](100) NULL,
	[SMTPUserId] [varchar](500) NULL,
	[SMTPPassword] [varchar](500) NULL,
	[IsSSLEnabled] [bit] NULL,
	[RoboCallFromNumber] [varchar](500) NULL,
	[MessagingServiceSid] [varchar](max) NULL,
 CONSTRAINT [PK_CompanySecret] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
