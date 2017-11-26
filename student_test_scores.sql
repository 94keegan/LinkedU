CREATE TABLE [dbo].[student_test_scores](
	[userID] [int] NOT NULL,
	[test_score] [float] NOT NULL,
	[test_date] [datetime],
	[test_type] [int] NOT NULL,
	primary key (userID, [test_type])
)
GO

ALTER TABLE [dbo].[student_test_scores]  WITH CHECK ADD  CONSTRAINT [fk_student_test_scores_userid] FOREIGN KEY([userID])
REFERENCES [dbo].[logins] ([userID])
GO

ALTER TABLE [dbo].[student_test_scores] CHECK CONSTRAINT [fk_student_test_scores_userid]
GO

ALTER TABLE [dbo].[student_test_scores]  WITH CHECK ADD  CONSTRAINT [fk_student_test_scores_test_type] FOREIGN KEY([test_type])
REFERENCES [dbo].[student_test_types] ([id])
GO

ALTER TABLE [dbo].[student_test_scores] CHECK CONSTRAINT [fk_student_test_scores_test_type]
GO
