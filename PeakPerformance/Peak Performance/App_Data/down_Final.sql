ALTER TABLE [dbo].[ExcerciseMuscleGroups] DROP CONSTRAINT [FK_dbo.ExerciseMuscleGroups_dbo.MuscleGroups_MuscleGroupsID];
ALTER TABLE [dbo].[ExcerciseMuscleGroups] DROP CONSTRAINT [FK_dbo.ExerciseMuscleGroups_dbo.Exercises_ExerciseID];
ALTER TABLE [dbo].[Workouts] DROP CONSTRAINT [FK_dbo.Workouts_dbo.Teams_TeamID];
ALTER TABLE [dbo].[Complexes] DROP CONSTRAINT [FK_dbo.Complexes_dbo.Workouts_WorkoutID];
ALTER TABLE [dbo].[ComplexItems] DROP CONSTRAINT [FK_dbo.ComplexItems_dbo.Exercises_ExerciseID];
ALTER TABLE [dbo].[ComplexItems] DROP CONSTRAINT [FK_dbo.ComplexItems_dbo.Complexes_ComplexID];

ALTER TABLE [dbo].[MuscleGroups] DROP CONSTRAINT [PK_dbo.MuscleGroups];
ALTER TABLE [dbo].[Exercises] DROP CONSTRAINT [PK_dbo.Exercises];
ALTER TABLE [dbo].[ExcerciseMuscleGroups] DROP CONSTRAINT [PK_dbo.ExerciseMuscleGroups];
ALTER TABLE [dbo].[Workouts] DROP CONSTRAINT [PK_dbo.Workouts];
ALTER TABLE [dbo].[Complexes] DROP CONSTRAINT [PK_dbo.Complexes];
ALTER TABLE [dbo].[ComplexItems] DROP CONSTRAINT [PK_dbo.ComplexItems];

DROP TABLE MuscleGroups;
DROP TABLE Exercises;
DROP TABLE ExerciseMuscleGroups;
DROP TABLE Workouts;
DROP TABLE Complexes;
DROP TABLE ComplexItems;

ALTER TABLE [dbo].[Coaches] DROP CONSTRAINT [FK_dbo.Coaches_dbo.Persons_ID]
ALTER TABLE [dbo].[Teams] DROP CONSTRAINT [FK_dbo.Teams_dbo.Coaches_CoachID]
ALTER TABLE [dbo].[Athletes] DROP CONSTRAINT [FK_dbo.Athletes_dbo.Persons_ID]
ALTER TABLE [dbo].[Athletes] DROP CONSTRAINT [FK_dbo.Athletes_dbo.Teams_TeamID]

DROP TABLE [dbo].[Persons];

DROP TABLE [dbo].[Coaches];

DROP TABLE [dbo].[Sports];

DROP TABLE [dbo].[Teams];

DROP TABLE [dbo].[Athletes];