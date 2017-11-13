CREATE TABLE [dbo].[student_test_types](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[name] [nvarchar](100) NOT NULL
)
GO

INSERT INTO student_test_types (name) values ('SAT')
INSERT INTO student_test_types (name) values ('ACT')
INSERT INTO student_test_types (name) values ('GRE')
INSERT INTO student_test_types (name) values ('PSAT')
INSERT INTO student_test_types (name) values ('PSAT/NMSQT')
INSERT INTO student_test_types (name) values ('LSAT')
INSERT INTO student_test_types (name) values ('MCAT')
