GO
CREATE TABLE [dbo].[TaxName](
	[TaxNameId] [int] IDENTITY(1,1) NOT NULL,
	[Tax1Name] [nvarchar](50) NOT NULL,
	[Tax2Name] [nvarchar](50) NOT NULL,
	[Tax3Name] [nvarchar](50) NOT NULL,
	[Tax4Name] [nvarchar](50) NOT NULL,
	[Tax5Name] [nvarchar](50) NOT NULL,
	[CompanyId] [int] NULL,
	[CreatedOnUTC] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedOnUTC] [datetime] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_TaxName] PRIMARY KEY CLUSTERED 
(
	[TaxNameId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
