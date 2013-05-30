CREATE TABLE [dbo].[14_Student](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](100) NOT NULL,
	[LastName] [nchar](100) NOT NULL,
	[Age] [smallint] NULL,
	[EducationLevel] [int] NOT NULL,
	[Address1] [nchar](100) NULL,
	[Address2] [nchar](10) NULL,
	[City] [nchar](100) NULL,
	[State] [nchar](10) NULL,
	[Zip] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[14_Student]  WITH CHECK ADD  CONSTRAINT [FK_14_Student_14_Student_EducationLevel] FOREIGN KEY([EducationLevel])
REFERENCES [dbo].[14_Student_EducationLevel] ([EducationLevelId])
GO

ALTER TABLE [dbo].[14_Student] CHECK CONSTRAINT [FK_14_Student_14_Student_EducationLevel]