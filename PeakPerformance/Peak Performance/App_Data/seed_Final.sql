INSERT INTO [dbo].[AspNetRoles]
	(Id, Name) VALUES
	(1, 'Admin'),
	(2, 'Coach'),
	(3, 'Athlete');

-- Add new coach and athlete items to persons table who will then be added to their respective tables
INSERT INTO [dbo].[Persons]
	(FirstName, LastName, PreferredName, Active, ASPNetIdentityID) VALUES
	('Shariah', 'Green', 'Shay', 1, 'd0659218-f187-411e-9518-476999b30deb'),
	('Laura', 'Palmer', 'Laura', 1, '4f1cd8e8-429b-4fc8-8203-008ec9530f52');

-- Add person already in persons table as a coach.
-- Manually assigning the ID like this won't work if data is already in this table.
INSERT INTO [dbo].[Coaches]
	(ID) VALUES
	(1);
	
-- Add new teams for coach ID = 1
INSERT INTO [dbo].[Teams]
	(TeamName, CoachID) VALUES
	('WOU Wolves', 1),
	('Sport Pros', 1),
	('Some High School', 1),
	('Johns Pickleball Group', 1);
   
-- Add new athletes to teams with no user ID
INSERT INTO [dbo].[Athletes]
	(ID, DOB, TeamID) VALUES
	(2, '01/01/1998', 1);