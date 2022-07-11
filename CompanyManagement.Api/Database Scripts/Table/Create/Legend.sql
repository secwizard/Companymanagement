GO
CREATE TABLE [dbo].[Legend](
	[LegendId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[IsParent] [bit] NULL,
	[NotificationTypeId] [int] NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Legend] PRIMARY KEY CLUSTERED 
(
	[LegendId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Legend]  WITH CHECK ADD  CONSTRAINT [FK_Legend_NotificationType] FOREIGN KEY([NotificationTypeId])
REFERENCES [dbo].[NotificationType] ([NotificationTypeId])
GO
ALTER TABLE [dbo].[Legend] CHECK CONSTRAINT [FK_Legend_NotificationType]
GO
