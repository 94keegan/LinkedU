CREATE TABLE [dbo].[university_file_types](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[name] [nvarchar](100) NOT NULL
)
GO

INSERT INTO university_file_types (name) values ('Brochure')
INSERT INTO university_file_types (name) values ('Video')
INSERT INTO university_file_types (name) values ('Audio')
INSERT INTO university_file_types (name) values ('Campus Map')
insert into university_file_types (name) values ('Image')
insert into university_file_types (name) values ('Profile Picture')
