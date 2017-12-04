CREATE TABLE [dbo].[highlighted_programs](
	[userID] [int] NOT NULL,
	[program_name] [nvarchar](100) NOT NULL,
	[program_type] [int] NOT NULL,
	primary key (userID, [program_name])
)
GO

ALTER TABLE [dbo].[highlighted_programs]  WITH CHECK ADD  CONSTRAINT [fk_highlighted_programs_userid] FOREIGN KEY([userID])
REFERENCES [dbo].[logins] ([userID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[highlighted_programs] CHECK CONSTRAINT [fk_highlighted_programs_userid]
GO

ALTER TABLE [dbo].[highlighted_programs]  WITH CHECK ADD  CONSTRAINT [fk_program_type] FOREIGN KEY([program_type])
REFERENCES [dbo].[university_program_types] ([id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[highlighted_programs] CHECK CONSTRAINT [fk_program_type]
GO
