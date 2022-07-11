GO
CREATE TABLE [dbo].[LegendChild](
	[LegendChildId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[LegendId] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_LegendChild] PRIMARY KEY CLUSTERED 
(
	[LegendChildId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LegendChild]  WITH CHECK ADD  CONSTRAINT [FK_LegendChild_Legend] FOREIGN KEY([LegendId])
REFERENCES [dbo].[Legend] ([LegendId])
GO
ALTER TABLE [dbo].[LegendChild] CHECK CONSTRAINT [FK_LegendChild_Legend]
GO
