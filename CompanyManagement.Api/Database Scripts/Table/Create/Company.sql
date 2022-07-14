GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[ShortName] [nvarchar](100) NOT NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[PIN] [nvarchar](20) NULL,
	[DistrictCode] [nvarchar](200) NULL,
	[StateCode] [nvarchar](200) NULL,
	[CountryCode] [nvarchar](200) NULL,
	[AdminPhone] [nvarchar](20) NULL,
	[ServicePhone] [nvarchar](20) NULL,
	[AdminEmail] [nvarchar](100) NULL,
	[ServiceEmail] [nvarchar](100) NULL,
	[SecondaryEmail] [nvarchar](100) NULL,
	[GSTNumber] [nvarchar](200) NULL,
	[PanNumber] [nvarchar](201) NULL,
	[BusinessType] [nvarchar](200) NOT NULL,
	[CurrencyCode] [nvarchar](10) NULL,
	[ImageFilePath] [nvarchar](max) NOT NULL,
	[LogoFileName] [nvarchar](max) NULL,
	[FavIconFileName] [nvarchar](max) NULL,
	[LoginImageFileName] [nvarchar](max) NULL,
	[Website] [nvarchar](max) NULL,
	[PINRequired] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[CompanySiteUrl] [nvarchar](255) NULL,
	[AdminPhoneCountryCode] [nvarchar](30) NULL,
	[ServicePhoneCountryCode] [nvarchar](30) NULL,
	[AdminPhoneCode] [nvarchar](50) NULL,
	[ServicePhoneCode] [nvarchar](50) NULL,
	[CurrencyId] [int] NULL,
	[GoogleClientId] [nvarchar](500) NULL,
	[FaceBookApiId] [nvarchar](500) NULL,
	[IsPhonePeActive] [bit] NOT NULL,
	[GoogleClientSecret] [nvarchar](500) NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Company__A6160FD1B71F36DC] UNIQUE NONCLUSTERED 
(
	[ShortName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF__Company__PINRequ__5EBF139D]  DEFAULT ((1)) FOR [PINRequired]
GO
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF__Company__IsActiv__5FB337D6]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF__Company__Created__60A75C0F]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Company] ADD  DEFAULT ((0)) FOR [IsPhonePeActive]
GO
ALTER TABLE [dbo].[Company]  WITH CHECK ADD  CONSTRAINT [FK_Company_CurrencyMaster] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[CurrencyMaster] ([CurrencyId])
GO
ALTER TABLE [dbo].[Company] CHECK CONSTRAINT [FK_Company_CurrencyMaster]
GO
