CREATE TABLE [dbo].[Role]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[Code] CHAR(3),
	[Description] NVARCHAR(100),
	PRIMARY KEY (ID)
);

CREATE TABLE [dbo].[Login]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[UserName] NVARCHAR(200),
	[Email] NVARCHAR(200),
	[PasswordHash] BINARY(64),
	[Active] BIT,
	[RoleID] INT,
	PRIMARY KEY (ID),
	FOREIGN KEY (RoleID) REFERENCES [dbo].[Role](ID)
);

CREATE TABLE [dbo].[Person]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[FirstName] NVARCHAR(50),
	[LastName] NVARCHAR(50),
	[Gender] CHAR,
	[Age] INT,
	[Active] BIT,
	[LoginID] INT,
	FOREIGN KEY (LoginID) REFERENCES [dbo].[Login](ID),
	PRIMARY KEY (ID)
);

CREATE TABLE [dbo].[EventList]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[Name] NVARCHAR(100),
	PRIMARY KEY (ID)
);

CREATE TABLE [dbo].[Event]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[Location] NVARCHAR(200),
	[Date] DATE,
	[EventListID] INT,
	PRIMARY KEY (ID),
	FOREIGN KEY (EventListID) REFERENCES [dbo].[EventList](ID)
);

CREATE TABLE [dbo].[Team]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[Name] NVARCHAR(100),
	[Location] NVARCHAR(200),
	[Level] NVARCHAR(50),
	PRIMARY KEY (ID)
);

CREATE TABLE [dbo].[Affiliation]
(
	[PersonID] INT,
	[TeamID] INT,
	FOREIGN KEY (PersonID) REFERENCES [dbo].[Person](ID),
	FOREIGN KEY (TeamID) REFERENCES [dbo].[Team](ID)
);

CREATE TABLE [dbo].[Times]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[Time] TIME,
	[PersonID] INT,
	[EventID] INT, 
	PRIMARY KEY (ID),
	FOREIGN KEY (PersonID) REFERENCES [dbo].[Person](ID),
	FOREIGN KEY (EventID) REFERENCES [dbo].[Event](ID)
);