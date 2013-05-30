CREATE TABLE [dbo].[ProspectiveAcademic](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[AcademicId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[AcademyName] [nvarchar](255) NOT NULL,
	[AcademyAddress] [nvarchar](255) NULL,
	[PrimaryContactName] [nvarchar](255) NOT NULL,
	[PrimaryTitle] [nvarchar](255) NULL,
	[PrimaryTelephone] [varchar](14) NULL,
	[PrimaryEmailAddress] [nvarchar](255) NOT NULL,
	[SecondaryContactName] [nvarchar](255) NULL,
	[SecondaryTitle] [nvarchar](255) NULL,
	[SecondaryTelephone] [varchar](14) NULL,
	[SecondaryEmailAddress] [nvarchar](255) NULL,
	[AcademicInstitutionTextField] [nvarchar](max) NULL,
	[ImageUploaded] [varchar](3) NULL,
 CONSTRAINT [PK_ProspectiveAcademic] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]