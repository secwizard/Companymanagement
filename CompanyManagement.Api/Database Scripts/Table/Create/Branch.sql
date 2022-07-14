GO
CREATE TABLE [dbo].[Branch](
	[BranchId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Address1] [nvarchar](max) NULL,
	[Address2] [nvarchar](max) NULL,
	[PostalCode] [nvarchar](20) NULL,
	[District] [nvarchar](50) NULL,
	[State] [nvarchar](50) NULL,
	[Country] [nvarchar](50) NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Branch] PRIMARY KEY CLUSTERED 
(
	[BranchId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Branch] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Branch] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
