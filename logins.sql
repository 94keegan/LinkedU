CREATE TABLE logins (
	userID INT IDENTITY(1,1) PRIMARY KEY ,
	userLogin VARCHAR(25) NOT NULL,
	userPassword VARCHAR(25) COLLATE SQL_Latin1_General_CP1_CS_AS NOT NULL,
	constraint unique_login UNIQUE (userLogin)
);