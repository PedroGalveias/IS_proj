CREATE TABLE [dbo].[Alerts]
(
	[Id] SMALLINT PRIMARY KEY IDENTITY(1, 1) NOT NULL, 
    [Tipo] VARCHAR(50) NOT NULL, 
    [Operacao] VARCHAR(50) NOT NULL, 
    [Valor1] FLOAT NOT NULL, 
    [Valor2] FLOAT NOT NULL, 
    [Ativo] SMALLINT NOT NULL, 
    [Sensor_Id] INT NOT NULL,

)
