CREATE TABLE [dbo].[ExerciseRecords]
(
	[ExerciseRecordId] INT NOT NULL IDENTITY(1,1),
	[ComplexReps] INT,
	[ComplexSets] INT,
	[LiftWeight] FLOAT,
	[RunSpeed] FLOAT,
	[RunTime] TIME,
	[RunDistance] FLOAT,
	[ExerciseID] INT NOT NULL,
	[AthleteID] INT NOT NULL,
	
	CONSTRAINT [FK_dbo.ExerciseRecords_dbo.Exercises_ID] FOREIGN KEY ([ExerciseID]) REFERENCES [dbo].[Exercises] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.ExerciseRecords_dbo.Athletes_ID] FOREIGN KEY ([AthleteID]) REFERENCES [dbo].[Athletes] ([ID]) ON DELETE CASCADE,
	CONSTRAINT [PK_dbo.ExerciseRecords] PRIMARY KEY CLUSTERED ([ExerciseRecordId] ASC)
);