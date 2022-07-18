GO
CREATE TABLE [dbo].[LookUp](
	[LookUpId] [bigint] IDENTITY(1,1) NOT NULL,
	[LookUpType] [nvarchar](100) NOT NULL,
	[LookUpValue] [nvarchar](100) NOT NULL,
	[LookUpDescription] [nvarchar](400) NULL,
	[IsActive] [bit] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
 CONSTRAINT [PK_LookUp] PRIMARY KEY CLUSTERED 
(
	[LookUpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[LookUp] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[LookUp] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO
