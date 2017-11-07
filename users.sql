CREATE TABLE users (
	userID INT PRIMARY KEY,
	firstName NVARCHAR(50),
	lastName NVARCHAR(50),
	email VARCHAR(100) NOT NULL,
	securityQuestion NVARCHAR(255),
	securityAnswer NVARCHAR(255),
	constraint unique_email UNIQUE (email),
	constraint fk_userid foreign key (userID) REFERENCES logins(userID)
);