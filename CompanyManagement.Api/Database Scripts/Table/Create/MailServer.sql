GO
CREATE TABLE [dbo].[MailServer](
	[MailServerId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[SMTPServer] [nvarchar](100) NULL,
	[SMTPPort] [int] NULL,
	[EnableSSL] [bit] NULL,
	[FromEmailDisplayName] [nvarchar](200) NULL,
	[FromEmailId] [nvarchar](100) NULL,
	[FromEmailPwd] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_MailServerh] PRIMARY KEY CLUSTERED 
(
	[MailServerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[MailServer] ADD  DEFAULT ((25)) FOR [SMTPPort]
GO
ALTER TABLE [dbo].[MailServer] ADD  DEFAULT ((1)) FOR [EnableSSL]
GO
ALTER TABLE [dbo].[MailServer] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[MailServer] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
