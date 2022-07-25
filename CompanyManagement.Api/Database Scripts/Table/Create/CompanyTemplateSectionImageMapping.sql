GO
CREATE TABLE [dbo].[CompanyTemplateSectionImageMapping](
	[CompanyTemplateSectionImageMappingId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyTemplateSectionId] [int] NOT NULL,
	[ImagePath] [nvarchar](max) NOT NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedAt] [datetime] NULL,
	[ItemId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CompanyTemplateSectionImageMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompanyTemplateSectionImageMapping] ADD  DEFAULT ((0)) FOR [ItemId]
GO
ALTER TABLE [dbo].[CompanyTemplateSectionImageMapping]  WITH CHECK ADD  CONSTRAINT [FK__CompanyTe__Compa__2D7CBDC4] FOREIGN KEY([CompanyTemplateSectionId])
REFERENCES [dbo].[CompanyTemplateSection] ([CompanyTemplateSectionId])
GO
ALTER TABLE [dbo].[CompanyTemplateSectionImageMapping] CHECK CONSTRAINT [FK__CompanyTe__Compa__2D7CBDC4]
GO
