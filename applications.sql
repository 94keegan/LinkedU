CREATE TABLE applications (
	userID INT NOT NULL,
	universityID INT NOT NULL,
	personalMessage NTEXT,
	constraint fk_app_userid foreign key (userID) REFERENCES student_profiles(userID) ON DELETE CASCADE,
  constraint fk_app_universityID foreign key (universityID) REFERENCES university_profiles(universityID) ON DELETE NO ACTION,
  constraint pk_applications primary key (userID, universityID)
);
