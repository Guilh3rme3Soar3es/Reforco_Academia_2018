CREATE TABLE [dbo].[student]
(
	[id_student] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] VARCHAR(100) NOT NULL, 
    [age] INT NOT NULL, 
    [average] DECIMAL(18, 2) NULL
)
