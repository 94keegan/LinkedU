CREATE TABLE [dbo].[university_program_types](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[name] [nvarchar](100) NOT NULL
)
GO

INSERT INTO university_program_types (name) values ('Undergraduate')
INSERT INTO university_program_types (name) values ('Graduate')
INSERT INTO university_program_types (name) values ('Athletic')
INSERT INTO university_program_types (name) values ('Community')
INSERT INTO university_program_types (name) values ('Employment')
