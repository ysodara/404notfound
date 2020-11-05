-- add new coach role from existing user
INSERT INTO [dbo].[AspNetUserRoles] (UserId,RoleId)
VALUES
('d0659218-f187-411e-9518-476999b30deb',2);

-- add new coach item in coach table from existing user
INSERT INTO [dbo].[Coaches] (FirstName,LastName,PreferredName,Active,UserId)
VALUES
('Shariah','Green','Shay',1,'d0659218-f187-411e-9518-476999b30deb');

-- add new teams for coach id = 5
INSERT INTO [dbo].[Teams] (TeamName, CoachId)
VALUES
	('WOU Wolves', 5),
	('Sport Pros', 5),
	('Some High School', 5),
	('Johns Pickleball Group', 5);
   
-- add new athletes to teams with no user id
INSERT INTO [dbo].[Athletes] (FirstName, LastName, PreferredName, Active, TeamId, DOB, UserId)
VALUES
	('Kelly', 'Johnston', 'Kelly', 0, 1, '01/01/1998', 'temp'),
	('Laura', 'Jean', 'Laura', 0, 3, '01/01/1998', 'temp'),
	('David','Barnett', 'Davey', 0, 2, '01/01/1998', 'temp'),
	('Allen','Jefferson', 'Al', 0, 1, '01/01/1998', 'temp'),
	('Josiah', 'Callum', 'Jo', 0, 4, '01/01/1998', 'temp');