GO
CREATE TABLE [dbo].[CompanyTemplateSectionItemMapping](
	[CompanyTemplateSectionItemMappingId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyTemplateSectionId] [int] NOT NULL,
	[ItemId] [bigint] NOT NULL,
	[VariantId] [bigint] NULL,
	[PrimaryText] [nvarchar](100) NULL,
	[SecondaryText] [nvarchar](100) NULL,
	[TertiaryText] [nvarchar](100) NULL,
	[DisplayOrder] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [nvarchar](max) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK__CompanyT__854069E1BB6B12B3] PRIMARY KEY CLUSTERED 
(
	[CompanyTemplateSectionItemMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CompanyTemplateSectionItemMapping]  WITH CHECK ADD  CONSTRAINT [FK__CompanyTe__Compa__7E02B4CC] FOREIGN KEY([CompanyTemplateSectionId])
REFERENCES [dbo].[CompanyTemplateSection] ([CompanyTemplateSectionId])
GO
ALTER TABLE [dbo].[CompanyTemplateSectionItemMapping] CHECK CONSTRAINT [FK__CompanyTe__Compa__7E02B4CC]
GO
