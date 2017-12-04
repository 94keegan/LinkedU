CREATE TABLE promotions (
	id INT IDENTITY(1, 1) NOT NULL,
	userID INT NOT NULL,
	universityID INT NOT NULL,
	personalMessage NTEXT,
	promoted DATETIME,
	notification_seen DATETIME,
  constraint fk_promo_userid foreign key (userID) REFERENCES users(userID) ON DELETE CASCADE,
  constraint fk_promo_universityID foreign key (universityID) REFERENCES universities(UNITID) ON DELETE CASCADE
);
