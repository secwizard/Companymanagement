GO
CREATE TABLE [dbo].[CurrencyMaster](
	[CurrencyId] [int] IDENTITY(1,1) NOT NULL,
	[CountryCode] [nvarchar](10) NOT NULL,
	[CurrencyCode] [nvarchar](10) NOT NULL,
	[CurrencySymbol] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_CurrencyMaster] PRIMARY KEY CLUSTERED 
(
	[CurrencyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
