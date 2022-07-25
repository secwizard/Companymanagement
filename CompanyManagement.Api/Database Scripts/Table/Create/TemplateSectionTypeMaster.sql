GO
CREATE TABLE [dbo].[TemplateSectionTypeMaster](
	[TemplateSectionType] [int] IDENTITY(1,1) NOT NULL,
	[TemplateSectionName] [nvarchar](50) NOT NULL,
	[HelpLink] [nvarchar](max) NULL,
 CONSTRAINT [PK_TemplateSectionTypeMaster] PRIMARY KEY CLUSTERED 
(
	[TemplateSectionType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
