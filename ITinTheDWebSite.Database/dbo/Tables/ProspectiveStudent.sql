CREATE TABLE [dbo].[ProspectiveStudent](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[Status] [int] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Telephone] [varchar](14) NULL,
	[EmailAddress] [nvarchar](255) NOT NULL,
	[DesiredCareerPath] [nvarchar](255) NULL,
	[Gender] [varchar](6) NULL,
	[ResumeUploaded] [varchar](3) NULL,
	[TranscriptUploaded] [varchar](3) NULL,
	[ProspectiveStudentTextField] [nvarchar](max) NULL,
	[ImageUploaded] [varchar](3) NULL,
 CONSTRAINT [PK_ProspectiveStudent] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]