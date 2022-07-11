GO
CREATE TABLE [dbo].[CompanyTemplate](
	[CompanyTemplateId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[TemplateId] [int] NOT NULL,
	[TemplateName] [nvarchar](200) NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[Url] [nvarchar](max) NOT NULL,
	[PrimaryColor] [nvarchar](100) NULL,
	[SecondaryColor] [nvarchar](100) NULL,
	[TertiaryColor] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](max) NOT NULL,
	[UpdatedAt] [datetime] NOT NULL,
	[Type] [nvarchar](10) NOT NULL,
	[IsForB2C] [bit] NOT NULL,
	[TemplateView] [nvarchar](max) NULL,
	[ViewName] [nvarchar](500) NULL,
	[MobileViewName] [nvarchar](500) NULL,
	[ImagePath] [nvarchar](max) NULL,
	[OnlyForMobile] [bit] NULL,
	[IsEditable] [bit] NULL,
	[TopBarBackgroundColor] [nvarchar](50) NULL,
	[TopLogoUrl] [nvarchar](max) NULL,
	[TopCartIconUrl] [nvarchar](max) NULL,
	[TopCartIconBackgroundColor] [nvarchar](50) NULL,
	[TopProfileIconUrl] [nvarchar](max) NULL,
	[TopProfileIconBackgroundColor] [nvarchar](50) NULL,
	[TopMenuIconUrl] [nvarchar](max) NULL,
	[PageBackgroundColor] [nvarchar](50) NULL,
	[FontBackgroundBrushColor] [nvarchar](50) NULL,
	[FontFamilyId] [int] NULL,
	[GeneralFontColor] [nvarchar](50) NULL,
	[SeeAllArrowIconUrl] [nvarchar](max) NULL,
	[ShopNowFontColor] [nvarchar](50) NULL,
	[ShopNowBackgroundColor] [nvarchar](50) NULL,
	[ShopNowBorderRadius] [decimal](18, 2) NULL,
	[ShopNowBorderColor] [nvarchar](50) NULL,
	[SectionBorderRadius] [decimal](18, 2) NULL,
	[SubSectionBorderRadius] [decimal](18, 2) NULL,
	[IsSubSectionTransparent] [bit] NULL,
	[SubSectionGradientPrimaryColor] [nvarchar](50) NULL,
	[SubSectionGradientSecondaryColor] [nvarchar](50) NULL,
	[LargeBrushName] [nvarchar](100) NULL,
	[MediumBrushName] [nvarchar](100) NULL,
	[SmallBrushName] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanyTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompanyTemplate] ADD  DEFAULT ((1)) FOR [IsForB2C]
GO
ALTER TABLE [dbo].[CompanyTemplate]  WITH CHECK ADD  CONSTRAINT [FK__CompanyTe__Compa__28B808A7] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[CompanyTemplate] CHECK CONSTRAINT [FK__CompanyTe__Compa__28B808A7]
GO
ALTER TABLE [dbo].[CompanyTemplate]  WITH CHECK ADD  CONSTRAINT [FK__CompanyTe__Templ__4316F928] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[FronEndTemplate] ([TemplateId])
GO
ALTER TABLE [dbo].[CompanyTemplate] CHECK CONSTRAINT [FK__CompanyTe__Templ__4316F928]
GO
ALTER TABLE [dbo].[CompanyTemplate]  WITH CHECK ADD  CONSTRAINT [FK_CompanyTemplate_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[CompanyTemplate] CHECK CONSTRAINT [FK_CompanyTemplate_Company]
GO
ALTER TABLE [dbo].[CompanyTemplate]  WITH CHECK ADD  CONSTRAINT [FK_CompanyTemplate_FontFamilyMaster] FOREIGN KEY([FontFamilyId])
REFERENCES [dbo].[FrontEndTemplateFontFamilyMaster] ([Id])
GO
ALTER TABLE [dbo].[CompanyTemplate] CHECK CONSTRAINT [FK_CompanyTemplate_FontFamilyMaster]
GO
