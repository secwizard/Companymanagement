GO
CREATE TABLE [dbo].[CompanyNotificationTemplate](
	[CompanyNotificationTemplateId] [int] IDENTITY(1,1) NOT NULL,
	[NotificationTemplateMasterId] [int] NOT NULL,
	[NotificationTypeId] [int] NOT NULL,
	[EmailSubject] [nvarchar](max) NULL,
	[EmailTemplate] [nvarchar](max) NULL,
	[IsEmailSelect] [bit] NOT NULL,
	[SMSTemplate] [nvarchar](max) NULL,
	[IsSMSSelect] [bit] NOT NULL,
	[WhatsAppTemplate] [nvarchar](max) NULL,
	[IsWhatsAppSelect] [bit] NOT NULL,
	[RoboCallTemplate] [nvarchar](max) NULL,
	[IsRoboCallSelect] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[ModifiedDate] [datetime] NOT NULL,
	[ModifiedBy] [uniqueidentifier] NOT NULL,
	[CompanyId] [int] NOT NULL,
 CONSTRAINT [PK_CompanyNotificationTemplate] PRIMARY KEY CLUSTERED 
(
	[CompanyNotificationTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompanyNotificationTemplate]  WITH CHECK ADD  CONSTRAINT [FK_CompanyNotificationTemplate_NotificationTemplateMaster] FOREIGN KEY([NotificationTemplateMasterId])
REFERENCES [dbo].[NotificationTemplateMaster] ([NotificationTemplateMasterId])
GO
ALTER TABLE [dbo].[CompanyNotificationTemplate] CHECK CONSTRAINT [FK_CompanyNotificationTemplate_NotificationTemplateMaster]
GO
