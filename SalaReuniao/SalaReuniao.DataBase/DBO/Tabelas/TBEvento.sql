CREATE TABLE [dbo].[TBEvento]
(
	[id_evento] INT NOT NULL IDENTITY(1,1), 
    [data_inicio] DATE NOT NULL, 
    [data_termino] DATE NOT NULL, 
    [funcionario_id] INT NOT NULL, 
    [sala_id] INT NOT NULL, 
    CONSTRAINT [FK_TBEvento_TBFuncionario] FOREIGN KEY ([funcionario_id]) REFERENCES [TBFuncionario]([id_funcionario]), 
    CONSTRAINT [FK_TBEvento_TBSala] FOREIGN KEY ([sala_id]) REFERENCES [TBSala]([id_sala]),
	CONSTRAINT [PK_eventos] PRIMARY KEY CLUSTERED

(
	[id_evento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
