CREATE TABLE [dbo].[student_file_types](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[name] [nvarchar](100) NOT NULL
)
GO

INSERT INTO student_file_types (name) values ('Transcript')
INSERT INTO student_file_types (name) values ('Video')
INSERT INTO student_file_types (name) values ('Audio')
INSERT INTO student_file_types (name) values ('Certification')
INSERT INTO student_file_types (name) values ('Award')
insert into student_file_types (name) values ('Image')
insert into student_file_types (name) values ('Profile Picture')
