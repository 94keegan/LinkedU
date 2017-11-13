CREATE TABLE [dbo].[student_files](
	[userID] [int] NOT NULL,
	[file_name] [nvarchar](100) NOT NULL,
	[file_type] [int] NOT NULL,
	[file] [varbinary](max) NOT NULL,
	primary key (userID, [file_name])
)
GO

ALTER TABLE [dbo].[student_files]  WITH CHECK ADD  CONSTRAINT [fk_student_files_userid] FOREIGN KEY([userID])
REFERENCES [dbo].[logins] ([userID])
GO

ALTER TABLE [dbo].[student_files] CHECK CONSTRAINT [fk_student_files_userid]
GO

ALTER TABLE [dbo].[student_files]  WITH CHECK ADD  CONSTRAINT [fk_student_files_type] FOREIGN KEY([file_type])
REFERENCES [dbo].[student_file_types] ([id])
GO

ALTER TABLE [dbo].[student_files] CHECK CONSTRAINT [fk_student_files_type]
GO
