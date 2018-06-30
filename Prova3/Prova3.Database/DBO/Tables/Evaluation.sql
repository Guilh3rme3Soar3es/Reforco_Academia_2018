CREATE TABLE [dbo].[Evaluation]
(
	[id_evaluation] INT NOT NULL PRIMARY KEY IDENTITY, 
    [assunto] VARCHAR(100) NOT NULL, 
    [date] DATETIME NOT NULL
)
