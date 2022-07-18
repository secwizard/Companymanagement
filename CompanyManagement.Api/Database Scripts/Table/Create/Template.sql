GO
CREATE TABLE [dbo].[Template](
	[TemplateId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[TemplateType] [nvarchar](100) NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Title] [nvarchar](200) NULL,
	[HTMLData] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Template] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Template] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Template] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
