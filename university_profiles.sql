CREATE TABLE university_profiles (
	universityID INT PRIMARY KEY,
	constraint fk_universityID foreign key (universityID) REFERENCES logins(userID) ON DELETE CASCADE
)
