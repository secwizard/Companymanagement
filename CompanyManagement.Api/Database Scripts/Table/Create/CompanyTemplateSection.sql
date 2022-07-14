GO
CREATE TABLE [dbo].[CompanyTemplateSection](
	[CompanyTemplateSectionId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyTemplateId] [int] NOT NULL,
	[SectionType] [int] NOT NULL,
	[SectionName] [nvarchar](500) NOT NULL,
	[SectionBackgrounColor] [nvarchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedAt] [datetime] NULL,
	[PrimaryText] [nvarchar](max) NULL,
	[SecondaryText] [nvarchar](max) NULL,
	[TertiaryText] [nvarchar](max) NULL,
	[DisplayOrder] [int] NOT NULL,
	[SectionFor] [int] NOT NULL,
 CONSTRAINT [PK__CompanyT__556FA58F4A565C72] PRIMARY KEY CLUSTERED 
(
	[CompanyTemplateSectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompanyTemplateSection] ADD  CONSTRAINT [DF_CompanyTemplateSection_SectionFor]  DEFAULT ((1)) FOR [SectionFor]
GO
ALTER TABLE [dbo].[CompanyTemplateSection]  WITH CHECK ADD  CONSTRAINT [FK_CompanyTemplateSection_CompanyTemplate] FOREIGN KEY([CompanyTemplateId])
REFERENCES [dbo].[CompanyTemplate] ([CompanyTemplateId])
GO
ALTER TABLE [dbo].[CompanyTemplateSection] CHECK CONSTRAINT [FK_CompanyTemplateSection_CompanyTemplate]
GO
ALTER TABLE [dbo].[CompanyTemplateSection]  WITH CHECK ADD  CONSTRAINT [FK_CompanyTemplateSection_TemplateSectionForMetaData] FOREIGN KEY([SectionFor])
REFERENCES [dbo].[TemplateSectionForMetaData] ([TemplateSectionForId])
GO
ALTER TABLE [dbo].[CompanyTemplateSection] CHECK CONSTRAINT [FK_CompanyTemplateSection_TemplateSectionForMetaData]
GO
ALTER TABLE [dbo].[CompanyTemplateSection]  WITH CHECK ADD  CONSTRAINT [FK_CompanyTemplateSection_TemplateSectionTypeMaster] FOREIGN KEY([SectionType])
REFERENCES [dbo].[TemplateSectionTypeMaster] ([TemplateSectionType])
GO
ALTER TABLE [dbo].[CompanyTemplateSection] CHECK CONSTRAINT [FK_CompanyTemplateSection_TemplateSectionTypeMaster]
GO
