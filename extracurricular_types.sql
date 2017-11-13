CREATE TABLE [dbo].[extracurricular_types](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[name] [nvarchar](100) NOT NULL
)
GO

INSERT INTO extracurricular_types (name) values ('Student Government')
INSERT INTO extracurricular_types (name) values ('Academic Team/Club')
INSERT INTO extracurricular_types (name) values ('Internship')
INSERT INTO extracurricular_types (name) values ('Culture Club')
INSERT INTO extracurricular_types (name) values ('Volunteer/Community Service')
INSERT INTO extracurricular_types (name) values ('Student Newspaper')
INSERT INTO extracurricular_types (name) values ('Athletics')
INSERT INTO extracurricular_types (name) values ('Arts')
