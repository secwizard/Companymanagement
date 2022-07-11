GO
CREATE TABLE [dbo].[TemplateSectionForMetaData](
	[TemplateSectionForId] [int] NOT NULL,
	[TemplateSectionForName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TemplateSectionForMetaData] PRIMARY KEY CLUSTERED 
(
	[TemplateSectionForId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
