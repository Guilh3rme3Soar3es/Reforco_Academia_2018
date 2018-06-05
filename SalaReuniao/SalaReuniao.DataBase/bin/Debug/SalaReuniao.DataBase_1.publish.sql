/*
Script de implantação para DBSalaReuniao

Este código foi gerado por uma ferramenta.
As alterações feitas nesse arquivo poderão causar comportamento incorreto e serão perdidas se
o código for gerado novamente.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "DBSalaReuniao"
:setvar DefaultFilePrefix "DBSalaReuniao"
:setvar DefaultDataPath "C:\Users\Guilherme Soares\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSqllocaldb"
:setvar DefaultLogPath "C:\Users\Guilherme Soares\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSqllocaldb"

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
PRINT N'A operação a seguir foi gerada de um arquivo de log de refatoração 7b3e5ead-0e7d-432e-8540-4e6581df4a25';

PRINT N'Renomear [dbo].[TBEvento].[date_inicio] para data_inicio';


GO
EXECUTE sp_rename @objname = N'[dbo].[TBEvento].[date_inicio]', @newname = N'data_inicio', @objtype = N'COLUMN';


GO
-- Etapa de refatoração para atualizar o servidor de destino com logs de transação implantados
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '7b3e5ead-0e7d-432e-8540-4e6581df4a25')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('7b3e5ead-0e7d-432e-8540-4e6581df4a25')

GO

GO
PRINT N'Atualização concluída.';


GO
