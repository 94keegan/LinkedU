
CREATE TABLE [dbo].[student_profiles](
	[userID] [int] NOT NULL PRIMARY KEY,
	[address1] [nvarchar](100) NULL,
	[address2] [nvarchar](100) NULL,
	[city] [varchar](100) NULL,
	[state] [char](2) NULL,
	[zipcode] [varchar](10) NULL,
	[highschool] [nvarchar] (100) NULL,
	[gpa] [float] NULL
)
GO

ALTER TABLE [dbo].[student_profiles]  WITH CHECK ADD  CONSTRAINT [fk_profile_userid] FOREIGN KEY([userID])
REFERENCES [dbo].[logins] ([userID])
GO

ALTER TABLE [dbo].[student_profiles] CHECK CONSTRAINT [fk_profile_userid]
GO
