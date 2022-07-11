GO
CREATE TABLE [dbo].[TemplateDefaultSection](
	[TemplateDefaultSectionId] [int] IDENTITY(1,1) NOT NULL,
	[TemplateId] [int] NOT NULL,
	[SectionType] [int] NOT NULL,
	[SectionName] [nvarchar](500) NOT NULL,
	[SectionBackgrounColor] [nvarchar](100) NULL,
	[SectionFor] [int] NOT NULL,
 CONSTRAINT [PK__Template__6DB78277B5772EC6] PRIMARY KEY CLUSTERED 
(
	[TemplateDefaultSectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TemplateDefaultSection] ADD  CONSTRAINT [DF_TemplateDefaultSection_SectionFor]  DEFAULT ((1)) FOR [SectionFor]
GO
ALTER TABLE [dbo].[TemplateDefaultSection]  WITH CHECK ADD  CONSTRAINT [FK__TemplateD__Templ__46E78A0C] FOREIGN KEY([TemplateId])
REFERENCES [dbo].[FronEndTemplate] ([TemplateId])
GO
ALTER TABLE [dbo].[TemplateDefaultSection] CHECK CONSTRAINT [FK__TemplateD__Templ__46E78A0C]
GO
ALTER TABLE [dbo].[TemplateDefaultSection]  WITH CHECK ADD  CONSTRAINT [FK_TemplateDefaultSection_TemplateSectionForMetaData] FOREIGN KEY([SectionFor])
REFERENCES [dbo].[TemplateSectionForMetaData] ([TemplateSectionForId])
GO
ALTER TABLE [dbo].[TemplateDefaultSection] CHECK CONSTRAINT [FK_TemplateDefaultSection_TemplateSectionForMetaData]
GO
ALTER TABLE [dbo].[TemplateDefaultSection]  WITH CHECK ADD  CONSTRAINT [FK_TemplateDefaultSection_TemplateSectionTypeMaster] FOREIGN KEY([SectionType])
REFERENCES [dbo].[TemplateSectionTypeMaster] ([TemplateSectionType])
GO
ALTER TABLE [dbo].[TemplateDefaultSection] CHECK CONSTRAINT [FK_TemplateDefaultSection_TemplateSectionTypeMaster]
GO
