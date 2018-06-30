/*
Script de implantação para Prova3

Este código foi gerado por uma ferramenta.
As alterações feitas nesse arquivo poderão causar comportamento incorreto e serão perdidas se
o código for gerado novamente.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "Prova3"
:setvar DefaultFilePrefix "Prova3"
:setvar DefaultDataPath "C:\Users\ndduser\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocaldb"
:setvar DefaultLogPath "C:\Users\ndduser\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocaldb"

GO
:on error exit
GO
/*
Detecta o modo SQLCMD e desabilita a execução do script se o modo SQLCMD não tiver suporte.
Para reabilitar o script após habilitar o modo SQLCMD, execute o comando a seguir:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'O modo SQLCMD deve ser habilitado para executar esse script com êxito.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Removendo [dbo].[FK_Result_evaluation]...';


GO
ALTER TABLE [dbo].[Result] DROP CONSTRAINT [FK_Result_evaluation];


GO
PRINT N'Removendo [dbo].[FK_Result_student]...';


GO
ALTER TABLE [dbo].[Result] DROP CONSTRAINT [FK_Result_student];


GO
PRINT N'Iniciando a recompilação da tabela [dbo].[Evaluation]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Evaluation] (
    [id_evaluation] INT           IDENTITY (1, 1) NOT NULL,
    [assunto]       VARCHAR (100) NOT NULL,
    [date]          DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([id_evaluation] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Evaluation])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Evaluation] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Evaluation] ([id_evaluation], [assunto], [date])
        SELECT   [id_evaluation],
                 [assunto],
                 [date]
        FROM     [dbo].[Evaluation]
        ORDER BY [id_evaluation] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Evaluation] OFF;
    END

DROP TABLE [dbo].[Evaluation];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Evaluation]', N'Evaluation';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Iniciando a recompilação da tabela [dbo].[result]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_result] (
    [id_result]     INT             IDENTITY (1, 1) NOT NULL,
    [note]          DECIMAL (18, 2) NOT NULL,
    [evaluation_id] INT             NOT NULL,
    [student_id]    INT             NOT NULL,
    PRIMARY KEY CLUSTERED ([id_result] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Result])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_result] ON;
        INSERT INTO [dbo].[tmp_ms_xx_result] ([id_result], [note], [evaluation_id], [student_id])
        SELECT   [id_result],
                 [note],
                 [evaluation_id],
                 [student_id]
        FROM     [dbo].[Result]
        ORDER BY [id_result] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_result] OFF;
    END

DROP TABLE [dbo].[Result];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_result]', N'result';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Iniciando a recompilação da tabela [dbo].[student]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_student] (
    [id_student] INT             IDENTITY (1, 1) NOT NULL,
    [name]       VARCHAR (100)   NOT NULL,
    [age]        INT             NOT NULL,
    [average]    DECIMAL (18, 2) NULL,
    PRIMARY KEY CLUSTERED ([id_student] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Student])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_student] ON;
        INSERT INTO [dbo].[tmp_ms_xx_student] ([id_student], [name], [age], [average])
        SELECT   [id_student],
                 [name],
                 [age],
                 [average]
        FROM     [dbo].[Student]
        ORDER BY [id_student] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_student] OFF;
    END

DROP TABLE [dbo].[Student];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_student]', N'student';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Criando [dbo].[FK_Result_evaluation]...';


GO
ALTER TABLE [dbo].[result] WITH NOCHECK
    ADD CONSTRAINT [FK_Result_evaluation] FOREIGN KEY ([evaluation_id]) REFERENCES [dbo].[Evaluation] ([id_evaluation]);


GO
PRINT N'Criando [dbo].[FK_Result_student]...';


GO
ALTER TABLE [dbo].[result] WITH NOCHECK
    ADD CONSTRAINT [FK_Result_student] FOREIGN KEY ([student_id]) REFERENCES [dbo].[student] ([id_student]);


GO
PRINT N'Verificando os dados existentes em restrições recém-criadas';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[result] WITH CHECK CHECK CONSTRAINT [FK_Result_evaluation];

ALTER TABLE [dbo].[result] WITH CHECK CHECK CONSTRAINT [FK_Result_student];


GO
PRINT N'Atualização concluída.';


GO
