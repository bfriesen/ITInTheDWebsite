CREATE TABLE [dbo].[ProspectiveStudentResume](
	[FileId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FileName] [nvarchar](max) NOT NULL,
	[FileContent] [varbinary](max) NOT NULL,
	[ContentType] [nvarchar](max) NOT NULL,
	[ContentLength] [int] NOT NULL,
 CONSTRAINT [PK_ProspectiveStudentResume] PRIMARY KEY CLUSTERED 
(
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]