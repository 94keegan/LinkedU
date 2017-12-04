CREATE TABLE [dbo].[university_files](
	[ID] [int] IDENTITY(1, 1) PRIMARY KEY NOT NULL,
	[userID] [int] NOT NULL,
	[file_name] [nvarchar](100) NOT NULL,
	[file_type] [int] NOT NULL,
	[file] [varbinary](max) NOT NULL
)
GO

ALTER TABLE [dbo].[university_files]  WITH CHECK ADD  CONSTRAINT [fk_university_files_userid] FOREIGN KEY([userID])
REFERENCES [dbo].[logins] ([userID])
GO

ALTER TABLE [dbo].[university_files] CHECK CONSTRAINT [fk_university_files_userid]
GO

ALTER TABLE [dbo].[university_files]  WITH CHECK ADD  CONSTRAINT [fk_university_files_type] FOREIGN KEY([file_type])
REFERENCES [dbo].[university_file_types] ([id])
GO

ALTER TABLE [dbo].[university_files] CHECK CONSTRAINT [fk_university_files_type]
GO