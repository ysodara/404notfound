CREATE TABLE [dbo].[Sports]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[SportName] NVARCHAR(100),
	[Season] NVARCHAR(100),
	CONSTRAINT [PK_dbo.Sports] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Teams]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[TeamName] NVARCHAR(200),
	[SportID] INT,
	CONSTRAINT [FK_dbo.Teams_dbo.Sports_SportID] FOREIGN KEY ([SportID]) REFERENCES [dbo].[Sports] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.Teams] PRIMARY KEY CLUSTERED ([ID] ASC)
);



CREATE TABLE [dbo].[Roles]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[RoleName] NVARCHAR(100),
	CONSTRAINT [PK_dbo.Roles] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Persons]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[Email] NVARCHAR(320),
	[FirstName] NVARCHAR(100),
	[M.I.] CHAR(1),
	[LastName] NVARCHAR(100),
	[PreferredName] NVARCHAR(200),
	[ProfilePic] VARBINARY(max),
	[Sex] CHAR(1),
	[Gender] NVARCHAR(200),
	[Active] BIT,
	[DOB] DATE,
	[DateAdded] DATE,
	[RoleID] INT,
	[TeamID] INT,
	CONSTRAINT [FK_dbo.Persons_dbo.Roles_RoleID] FOREIGN KEY ([RoleID]) REFERENCES [dbo].[Roles] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Persons_dbo.Teams_TeamID] FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Teams] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.Persons] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[WorkOuts]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[Date] DATE,
	[PersonID] INT,
	CONSTRAINT [FK_dbo.WorkOuts_dbo.Persons_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Persons] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.WorkOuts] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[MuscleList]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[Name] NVARCHAR(200),
	CONSTRAINT [PK_dbo.MuscleList] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[MovementList]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[Name] NVARCHAR(200),
	CONSTRAINT [PK_dbo.MovementList] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Exercise]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[URL] NVARCHAR(MAX),
	CONSTRAINT [PK_dbo.Exercise] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[ExerciseMovements]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[MovementListID] INT NOT NULL,
	[MuscleListID] INT NOT NULL,
	[ExerciseID] INT NOT NULL,
	CONSTRAINT [FK_dbo.ExerciseMovements_dbo.MuscleList_MuscleListID] FOREIGN KEY ([MuscleListID]) REFERENCES [dbo].[MuscleList] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.ExerciseMovements_dbo.MovementList_MovementListID] FOREIGN KEY ([MovementListID]) REFERENCES [dbo].[MovementList] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.ExerciseMovements_dbo.Exercise_ExerciseID] FOREIGN KEY ([ExerciseID]) REFERENCES [dbo].[Exercise] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.ExerciseMovements] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[ComplexItems]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[Reps] INT,
	[Sets] INT,
	[Weight] FLOAT,
	[Speed] FLOAT,
	[Time] TIME,
	[Distance] FLOAT,
	[ExerciseID] INT,
	CONSTRAINT [FK_dbo.ComplexItems_dbo.Exercise_ExerciseID] FOREIGN KEY ([ExerciseID]) REFERENCES [dbo].[Exercise] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.ComplexItems] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Complex]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[ComplexItemID] INT,
	[WorkOutID] INT,
	CONSTRAINT [FK_dbo.Complex_dbo.ComplexItems_ComplexItemID] FOREIGN KEY ([ComplexItemID]) REFERENCES [dbo].[ComplexItems] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Complex_dbo.WorkOuts_WorkOutID] FOREIGN KEY ([WorkOutID]) REFERENCES [dbo].[WorkOuts] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.Complex] PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Records]
(
	[ID] INT NOT NULL IDENTITY(1,1),
	[Weight] FLOAT,
	[Time] TIME,
	[PersonID] INT,
	[ExerciseID] INT,
	CONSTRAINT [FK_dbo.Records_dbo.Persons_PersonID] FOREIGN KEY ([PersonID]) REFERENCES [dbo].[Persons] ([ID]) ON DELETE CASCADE,
	FOREIGN KEY (ExerciseID) REFERENCES [dbo].[Exercise](ID),
	CONSTRAINT [FK_dbo.Records_dbo.Exercise_ExerciseID] FOREIGN KEY ([ExerciseID]) REFERENCES [dbo].[Exercise] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.Records] PRIMARY KEY CLUSTERED ([ID] ASC)
);
