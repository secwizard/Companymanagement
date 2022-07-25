GO
CREATE TABLE [dbo].[FrontEndTemplateFontFamilyMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FontName] [nvarchar](50) NOT NULL,
	[FontDemoUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_FrontEndTemplateFontFamilyMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
