CREATE TABLE [dbo].[student_test_types](
	[id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[name] [nvarchar](100) NOT NULL,
	[minimum] int NOT NULL,
	[maximum] int NOT NULL,
)
GO

INSERT INTO student_test_types (name, minimum, maximum) values ('SAT', 400, 1600)
INSERT INTO student_test_types (name, minimum, maximum) values ('ACT', 1, 36)
INSERT INTO student_test_types (name, minimum, maximum) values ('GRE-V', 130, 170)
INSERT INTO student_test_types (name, minimum, maximum) values ('GRE-Q', 130, 170)
INSERT INTO student_test_types (name, minimum, maximum) values ('GRE-W', 0, 6)
INSERT INTO student_test_types (name, minimum, maximum) values ('PSAT', 320, 1520)
INSERT INTO student_test_types (name, minimum, maximum) values ('PSAT/NMSQT', 320, 1520)
INSERT INTO student_test_types (name, minimum, maximum) values ('LSAT', 120, 180)
INSERT INTO student_test_types (name, minimum, maximum) values ('MCAT', 472, 528)
