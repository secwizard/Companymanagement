GO
CREATE TABLE [dbo].[LegendFieldConfig](
	[CompanyId] [int] NOT NULL,
	[NotificationTypeId] [int] NOT NULL,
	[LegendKey] [nvarchar](50) NOT NULL,
	[FieldName] [nvarchar](50) NOT NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_LegendFieldConfig] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC,
	[NotificationTypeId] ASC,
	[LegendKey] ASC,
	[FieldName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
