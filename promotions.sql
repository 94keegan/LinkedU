CREATE TABLE promotions (
	userID INT NOT NULL,
	universityID INT NOT NULL,
	personalMessage NTEXT,
	promoted DATETIME,
	notification_seen DATETIME,
  constraint fk_promo_userid foreign key (userID) REFERENCES student_profiles(userID) ON DELETE CASCADE,
  constraint fk_promo_universityID foreign key (universityID) REFERENCES universities(UNITID) ON DELETE CASCADE
);
