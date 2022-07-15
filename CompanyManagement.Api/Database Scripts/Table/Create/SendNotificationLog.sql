GO
CREATE TABLE [dbo].[SendNotificationLog](
	[SendNotificationLogId] [int] IDENTITY(1,1) NOT NULL,
	[JsonSendData] [varchar](max) NULL,
	[Status] [bit] NULL,
	[Message] [varchar](max) NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[CreatedDate] [datetime] NULL,
	[IsActive] [bit] NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_SendNotificationLog] PRIMARY KEY CLUSTERED 
(
	[SendNotificationLogId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
