CREATE TABLE [dbo].[SiteAdmin](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Status] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[Name] [nchar](255) NOT NULL,
	[Telephone] [varchar](14) NULL,
	[EmailAddress] [nvarchar](255) NOT NULL,
	[Company] [nvarchar](255) NULL,
	[ImageUploaded] [varchar](3) NULL,
 CONSTRAINT [PK_SiteAdmin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]