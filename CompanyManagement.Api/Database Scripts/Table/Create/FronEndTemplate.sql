GO
CREATE TABLE [dbo].[FronEndTemplate](
	[TemplateId] [int] NOT NULL,
	[TemplateName] [nvarchar](200) NOT NULL,
	[TemplateView] [nvarchar](max) NULL,
	[ViewName] [nvarchar](500) NOT NULL,
	[PrimaryColor] [nvarchar](100) NOT NULL,
	[SecondaryColor] [nvarchar](100) NOT NULL,
	[TertiaryColor] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[MobileViewName] [nvarchar](500) NOT NULL,
	[ImagePath] [nvarchar](max) NULL,
	[Type] [nvarchar](10) NULL,
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
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[FronEndTemplate]  WITH CHECK ADD  CONSTRAINT [FK_FronEndTemplate_FontFamilyMaster] FOREIGN KEY([FontFamilyId])
REFERENCES [dbo].[FrontEndTemplateFontFamilyMaster] ([Id])
GO
ALTER TABLE [dbo].[FronEndTemplate] CHECK CONSTRAINT [FK_FronEndTemplate_FontFamilyMaster]
GO
