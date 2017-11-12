CREATE TABLE [dbo].[student_extracurriculars](
	[userID] [int] NOT NULL,
	[ec_name] [nvarchar](100) NOT NULL,
	[ec_type] [int] NOT NULL,
	primary key (userID, [ec_name])
)
GO

ALTER TABLE [dbo].[student_extracurriculars]  WITH CHECK ADD  CONSTRAINT [fk_student_extracurriculars_userid] FOREIGN KEY([userID])
REFERENCES [dbo].[logins] ([userID])
GO

ALTER TABLE [dbo].[student_extracurriculars] CHECK CONSTRAINT [fk_student_extracurriculars_userid]
GO

ALTER TABLE [dbo].[student_extracurriculars]  WITH CHECK ADD  CONSTRAINT [fk_ectype] FOREIGN KEY([ec_type])
REFERENCES [dbo].[extracurricular_types] ([id])
GO

ALTER TABLE [dbo].[student_extracurriculars] CHECK CONSTRAINT [fk_ectype]
GO
