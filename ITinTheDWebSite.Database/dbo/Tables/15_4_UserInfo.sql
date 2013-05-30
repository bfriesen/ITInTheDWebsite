CREATE TABLE [dbo].[15_4_UserInfo](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nchar](15) NOT NULL,
	[Password] [nchar](15) NOT NULL,
	[Address1] [nchar](30) NULL,
	[Address2] [nchar](30) NULL,
	[City] [nchar](30) NULL,
	[State] [nchar](30) NULL,
	[ZipCode] [int] NULL,
	[FirstName] [nchar](30) NOT NULL,
	[LastName] [nchar](30) NOT NULL,
	[SecurityQuestion] [nchar](30) NOT NULL,
	[SecurityAnswer] [nchar](15) NOT NULL,
	[SecurityHint] [nchar](10) NOT NULL
) ON [PRIMARY]