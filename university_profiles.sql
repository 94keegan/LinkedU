CREATE TABLE university_profiles (
	universityID INT PRIMARY KEY,
	[newsletter] [bit] NULL,
	constraint fk_universityID foreign key (universityID) REFERENCES logins(userID) ON DELETE CASCADE
)
