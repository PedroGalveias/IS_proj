CREATE TABLE [dbo].[Sensores]
(
	[Id] SMALLINT NOT NULL PRIMARY KEY, 
    [Battery] SMALLINT NOT NULL, 
    [Timestamp] BIGINT NOT NULL, 
    [Location] VARCHAR(50) NULL
)
