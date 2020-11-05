--ALTER TABLE [dbo].[Teams] DROP CONSTRAINT [FK_dbo.Teams_dbo.Coaches_CoachID];
--ALTER TABLE [dbo].[Athletes] DROP CONSTRAINT [FK_dbo.Athletes_dbo.Teams_TeamID];
--ALTER TABLE [dbo].[RaceEvents] DROP CONSTRAINT [FK_dbo.RaceEvents_dbo.Distances_DistanceID];
--ALTER TABLE [dbo].[RaceEvents] DROP CONSTRAINT [FK_dbo.RaceEvents_dbo.Athletes_AthleteID];
--ALTER TABLE [dbo].[RaceEvents] DROP CONSTRAINT [FK_dbo.RaceEvents_dbo.Locations_LocationID];

--DROP TABLE [dbo].[Coaches];

--DROP TABLE [dbo].[Teams];

--DROP TABLE [dbo].[Athletes];

--DROP TABLE [dbo].[Locations];

--DROP TABLE [dbo].[Distances];

--DROP TABLE [dbo].[RaceEvents];

DROP TABLE [dbo].[MuscleGroup];
DROP TABLE [dbo].[Roles];
DROP TABLE [dbo].[Sports];
DROP TABLE [dbo].[Teams];
DROP TABLE [dbo].[Exercise];
DROP TABLE [dbo].[ComplexItems];
DROP TABLE [dbo].[Persons];
DROP TABLE [dbo].[Records];
DROP TABLE [dbo].[WorkOuts];
DROP TABLE [dbo].[Complex];
DROP TABLE [dbo].[ExerciseMovements];
DROP TABLE [dbo].[MovementList];
DROP TABLE [dbo].[MuscleList];