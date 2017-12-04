CREATE TABLE applications (
	id INT IDENTITY(1, 1) NOT NULL,
	userID INT NOT NULL,
	universityID INT NOT NULL,
	personalMessage NTEXT,
	applied DATETIME,
	notification_seen DATETIME,
  constraint fk_app_userid foreign key (userID) REFERENCES users(userID) ON DELETE CASCADE,
  constraint fk_app_universityID foreign key (universityID) REFERENCES universities(UNITID) ON DELETE CASCADE,
  constraint pk_applications primary key (userID, universityID)
);
