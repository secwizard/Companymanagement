GO
CREATE TABLE [dbo].[CompanySetting](
	[CompanySettingId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[SettingType] [nvarchar](50) NOT NULL,
	[DataText] [nvarchar](100) NOT NULL,
	[DataValue] [nvarchar](max) NULL,
	[Option1] [nvarchar](200) NULL,
	[Option2] [nvarchar](200) NULL,
	[Option3] [nvarchar](200) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsAllProductInclusiveOfTax] [bit] NOT NULL,
 CONSTRAINT [PK_CompanySetting] PRIMARY KEY CLUSTERED 
(
	[CompanySettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompanySetting] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[CompanySetting] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[CompanySetting] ADD  CONSTRAINT [DF_CompanySetting_IsAllProductInclusiveOfTax]  DEFAULT ('0') FOR [IsAllProductInclusiveOfTax]
GO
