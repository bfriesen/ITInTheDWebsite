CREATE TABLE [dbo].[ProspectiveCorporateSponsor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SponsorId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[CompanyName] [nvarchar](255) NOT NULL,
	[CompanyAddress] [nvarchar](255) NULL,
	[ContactName] [nvarchar](255) NOT NULL,
	[Title] [nvarchar](255) NULL,
	[Telephone] [varchar](14) NULL,
	[EmailAddress] [nvarchar](255) NOT NULL,
	[Reason] [nvarchar](max) NULL,
	[SponsorPageTextField] [nvarchar](max) NULL,
	[ImageUploaded] [varchar](3) NULL,
 CONSTRAINT [PK_ProspectiveCorporateSponsor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]