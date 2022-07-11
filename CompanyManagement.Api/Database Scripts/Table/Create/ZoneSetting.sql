GO
CREATE TABLE [dbo].[ZoneSetting](
	[ZoneId] [int] IDENTITY(1,1) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[ZoneName] [nvarchar](100) NOT NULL,
	[PatternValue] [nvarchar](max) NOT NULL,
	[FromPostalCode] [bigint] NULL,
	[ToPostalCode] [bigint] NULL,
	[PatternType] [tinyint] NOT NULL,
	[Isdefault] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
	[CreatedOnUTC] [datetime] NULL,
	[UpdatedOnUTC] [datetime] NULL,
 CONSTRAINT [PK_ZoneSetting1] PRIMARY KEY CLUSTERED 
(
	[ZoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ZoneSetting] ADD  CONSTRAINT [DF__ZoneSetti__Isdef__0FEC5ADD]  DEFAULT ((0)) FOR [Isdefault]
GO
ALTER TABLE [dbo].[ZoneSetting] ADD  CONSTRAINT [DF__ZoneSetti__IsAct__10E07F16]  DEFAULT ((1)) FOR [IsActive]
GO
