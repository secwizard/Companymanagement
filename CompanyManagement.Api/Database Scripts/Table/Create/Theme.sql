GO
CREATE TABLE [dbo].[Theme](
	[ThemeId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[ThemeName] [nvarchar](200) NOT NULL,
	[ExtThemeName] [nvarchar](200) NULL,
	[ImageRatio] [decimal](18, 0) NULL,
	[NoOfHomePanels] [int] NULL,
	[Colour] [nvarchar](100) NULL,
	[MobileHeight] [int] NULL,
	[DesktopHeight] [int] NULL,
	[IsDefault] [bit] NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Theme] PRIMARY KEY CLUSTERED 
(
	[ThemeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Theme] ADD  DEFAULT ((0)) FOR [IsDefault]
GO
ALTER TABLE [dbo].[Theme] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Theme] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
