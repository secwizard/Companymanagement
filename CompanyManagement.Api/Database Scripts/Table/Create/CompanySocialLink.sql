GO
CREATE TABLE [dbo].[CompanySocialLink](
	[CompanySocialLinkId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[Facebook] [nvarchar](max) NOT NULL,
	[ShowFacebookOnline] [bit] NOT NULL,
	[Instagram] [nvarchar](max) NOT NULL,
	[ShowInstagramOnline] [bit] NOT NULL,
	[Twitter] [nvarchar](max) NOT NULL,
	[ShowTwitterOnline] [bit] NOT NULL,
	[ContactEmail] [nvarchar](max) NOT NULL,
	[ShowContactEmailOnline] [bit] NOT NULL,
	[ContactPhone] [nvarchar](max) NOT NULL,
	[ShowContactPhoneOnline] [bit] NOT NULL,
	[CreatedByUserId] [uniqueidentifier] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedByUserID] [uniqueidentifier] NULL,
	[UpdatedAt] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanySocialLinkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompanySocialLink]  WITH CHECK ADD FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
