CREATE TABLE [dbo].[TBEvento]
(
	[id_evento] INT NOT NULL PRIMARY KEY, 
    [date_inicio] DATE NOT NULL, 
    [data_termino] DATE NOT NULL, 
    [funcionario_id] INT NOT NULL, 
    [sala_id] INT NOT NULL, 
    CONSTRAINT [FK_TBEvento_TBFuncionario] FOREIGN KEY ([funcionario_id]) REFERENCES [TBFuncionario]([id_funcionario]), 
    CONSTRAINT [FK_TBEvento_TBSala] FOREIGN KEY ([sala_id]) REFERENCES [TBSala]([id_sala])
)
