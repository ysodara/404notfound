CREATE TABLE [dbo].[Persons]
(
	[ID]				INT	IDENTITY (1,1)	NOT NULL,
	[FirstName]			NVARCHAR (100)		NOT NULL,
	[LastName]			NVARCHAR (100)		NOT NULL,
	[PreferredName] 	NVARCHAR (100),
	[Active] 			BIT, 				
	[ASPNetIdentityID]	NVARCHAR (128)		NOT NULL
	
	CONSTRAINT [PK_dbo.Persons]	PRIMARY KEY CLUSTERED ([ID] ASC)
);

CREATE TABLE [dbo].[Coaches]
(
	[ID] 			INT			 	NOT NULL,

	CONSTRAINT [PK_dbo.Coaches] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Coaches_dbo.Persons_ID] FOREIGN KEY ([ID]) REFERENCES [Persons] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE
);

CREATE TABLE [dbo].[Teams]
(
	[ID] 			INT IDENTITY (1,1)	NOT NULL,
	[TeamName] 		NVARCHAR (200) 		NOT NULL,
	[CoachID] 		INT					NOT NULL

	CONSTRAINT [PK_dbo.Teams] PRIMARY KEY CLUSTERED ([ID] ASC)
	CONSTRAINT [FK_dbo.Teams_dbo.Coaches_CoachID] FOREIGN KEY ([CoachID]) REFERENCES [dbo].[Coaches] ([ID]) ON DELETE CASCADE,
);

CREATE TABLE [dbo].[Athletes]
(
	[ID] 			INT				NOT NULL,
	[Sex] 			CHAR (1),  
	[Gender] 		NVARCHAR (200),
	[DOB] 			DATE			NOT NULL,
    [Height] 		FLOAT,
    [Weight] 		FLOAT,
	[TeamID] 		INT
	FitBitUserID 	NVARCHAR(50) 	NULL;
	FitBitAccessToken NVARCHAR(MAX) NULL;
	
	CONSTRAINT [PK_dbo.Athletes] PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Athletes_dbo.Persons_ID] FOREIGN KEY ([ID]) REFERENCES [Persons] ([ID]) ON DELETE CASCADE ON UPDATE CASCADE,
	CONSTRAINT [FK_dbo.Athletes_dbo.Teams_TeamID] FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Teams] ([ID]) ON DELETE NO ACTION
);

CREATE TABLE [dbo].[Records]
(
	[ID]			INT	IDENTITY (1,1)	NOT NULL,
	[Completed]		BIT,
	[Note]			NVARCHAR (250),
	[AthleteID] 	INT					NOT NULL,
	[WorkoutID]		INT					NOT NULL,

	CONSTRAINT [PK_dbo.Records]	PRIMARY KEY CLUSTERED ([ID] ASC),
	CONSTRAINT [FK_dbo.Records_dbo.Athletes_AthleteID] FOREIGN KEY ([AthleteID]) REFERENCES [dbo].[Athletes] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.Records_dbo.Workouts_WorkoutID] FOREIGN KEY ([WorkoutID]) REFERENCES [dbo].[Workouts] ([ID]) ON DELETE NO ACTION
);

CREATE TABLE [dbo].[MuscleGroups]
(
    [ID] INT NOT NULL IDENTITY(1,1),
	[Name] NVARCHAR(200),
	CONSTRAINT [PK_dbo.MuscleGroups] PRIMARY KEY CLUSTERED ([ID] ASC)
)

CREATE TABLE [dbo].[Exercises]
(
    [ID] INT NOT NULL IDENTITY(1,1),
    [Name] NVARCHAR(128),
	[URL] NVARCHAR(MAX),
	CONSTRAINT [PK_dbo.Exercises] PRIMARY KEY CLUSTERED ([ID] ASC)
)

CREATE TABLE [dbo].[ExcerciseMuscleGroups]
(
    [ID] INT NOT NULL IDENTITY(1,1),
    [MuscleGroupID] INT NOT NULL,
	[ExerciseID] INT NOT NULL,
    
	CONSTRAINT [FK_dbo.ExerciseMuscleGroups_dbo.MuscleGroups_MuscleGroupsID] FOREIGN KEY ([MuscleGroupID]) REFERENCES [dbo].[MuscleGroups] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.ExerciseMuscleGroups_dbo.Exercises_ExerciseID] FOREIGN KEY ([ExerciseID]) REFERENCES [dbo].[Exercises] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.ExerciseMuscleGroups] PRIMARY KEY CLUSTERED ([ID] ASC)
)

CREATE TABLE [dbo].[Workouts]
(
    [ID] INT NOT NULL IDENTITY(1,1),
	[WorkoutDate] DATE NOT NULL,
	[TeamID] INT NOT NULL,

	CONSTRAINT [FK_dbo.Workouts_dbo.Teams_TeamID] FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Teams] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.Workouts] PRIMARY KEY CLUSTERED ([ID] ASC)
)

CREATE TABLE [dbo].[Complexes]
(
    [ID] INT NOT NULL IDENTITY(1,1),
	[WorkoutID] INT,

	CONSTRAINT [FK_dbo.Complexes_dbo.Workouts_WorkoutID] FOREIGN KEY ([WorkoutID]) REFERENCES [dbo].[Workouts] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.Complexes] PRIMARY KEY CLUSTERED ([ID] ASC)
)

CREATE TABLE [dbo].[ComplexItems]
(
    [ID] INT NOT NULL IDENTITY(1,1),
	[ExerciseName] NVARCHAR (100),
	[ComplexReps] INT,
	[ComplexSets] INT,
	[LiftWeight] FLOAT,
	[RunSpeed] FLOAT,
	[RunTime] TIME,
	[RunDistance] FLOAT,
	[ExerciseID] INT,
    [ComplexId] INT NOT NULL,
    
	CONSTRAINT [FK_dbo.ComplexItems_dbo.Exercises_ExerciseID] FOREIGN KEY ([ExerciseID]) REFERENCES [dbo].[Exercises] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.ComplexItems_dbo.Complexes_ComplexID] FOREIGN KEY ([ComplexId)] REFERENCES [dbo].[Complexes] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.ComplexItems] PRIMARY KEY CLUSTERED ([ID] ASC)
)