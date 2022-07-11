GO
CREATE TABLE [dbo].[TaxDetails](
	[TaxDetailsId] [int] IDENTITY(1,1) NOT NULL,
	[TaxName] [nvarchar](50) NOT NULL,
	[Tax1Percentage] [decimal](18, 2) NOT NULL,
	[Tax2Percentage] [decimal](18, 2) NOT NULL,
	[Tax3Percentage] [decimal](18, 2) NOT NULL,
	[Tax4Percentage] [decimal](18, 2) NOT NULL,
	[Tax5Percentage] [decimal](18, 2) NOT NULL,
	[CreatedById] [uniqueidentifier] NULL,
	[UpdatedById] [uniqueidentifier] NULL,
	[CreatedOnUTC] [datetime] NULL,
	[UpdatedOnUTC] [datetime] NULL,
	[CompanyId] [int] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_TaxDetails] PRIMARY KEY CLUSTERED 
(
	[TaxDetailsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[TaxDetails] ADD  CONSTRAINT [DF_TaxDetails_Tax1Percentage]  DEFAULT ((0)) FOR [Tax1Percentage]
GO
ALTER TABLE [dbo].[TaxDetails] ADD  CONSTRAINT [DF_TaxDetails_Tax2Percentage]  DEFAULT ((0)) FOR [Tax2Percentage]
GO
ALTER TABLE [dbo].[TaxDetails] ADD  CONSTRAINT [DF_TaxDetails_Tax3Percentage]  DEFAULT ((0)) FOR [Tax3Percentage]
GO
ALTER TABLE [dbo].[TaxDetails] ADD  CONSTRAINT [DF_TaxDetails_Tax4Percentage]  DEFAULT ((0)) FOR [Tax4Percentage]
GO
ALTER TABLE [dbo].[TaxDetails] ADD  CONSTRAINT [DF_TaxDetails_Tax5Percentage]  DEFAULT ((0)) FOR [Tax5Percentage]
GO
ALTER TABLE [dbo].[TaxDetails] ADD  CONSTRAINT [DF_TaxDetails_IsDefault]  DEFAULT ('0') FOR [IsDefault]
GO
ALTER TABLE [dbo].[TaxDetails] ADD  CONSTRAINT [DF_TaxDetails_Total]  DEFAULT ((0)) FOR [Total]
GO
