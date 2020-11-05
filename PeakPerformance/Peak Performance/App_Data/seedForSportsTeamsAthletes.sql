INSERT INTO [dbo].[Sports] (SportName, Season)
VALUES
	('Cheer Team', 'Fall'),
	('Cross Country', 'Fall'),
	('Football', 'Fall'),
	('Dance Team', 'Fall/Winter'),
	('Soccer', 'Fall'),
	('Volleyball', 'Fall'),
	('Other', 'Fall'),
	('Swim', 'Winter'),
	('Basketball', 'Winter'),
	('Wrestling', 'Winter'),
	('Other', 'Winter'),
	('Baseball', 'Spring'),
	('Softball', 'Spring'),
	('Golf', 'Spring'),
	('Tennis', 'Spring'),
	('Track & Field', 'Spring'),
	('Other', 'Spring');

INSERT INTO [dbo].[Coaches] (FirstName, LastName)
VALUES
	('Scot', 'Morse'),
	('Becka', 'Morgan'),
	('John', 'Doe'),
	('Jane', 'Smith');

INSERT INTO [dbo].[Teams] (TeamName, SportId, CoachId)
VALUES
	('WOU Wolves', 1, 11),
	('Sport Pros', 2, 6),
	('Some High School', 3, 15),
	('Johns Pickleball Group', 2, 10);

INSERT INTO [dbo].[Athletes] (FirstName, LastName, TeamId)
VALUES
	('Kelly', 'Johnston', 1),
	('Laura', 'Jean', 3),
	('David','Barnett', 2),
	('Allen','Jefferson', 1),
	('Jo', 'Callum', 0);
