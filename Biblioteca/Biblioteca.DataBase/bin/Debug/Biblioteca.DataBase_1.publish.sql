/*
Script de implantação para DBBiblioteca

Este código foi gerado por uma ferramenta.
As alterações feitas nesse arquivo poderão causar comportamento incorreto e serão perdidas se
o código for gerado novamente.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "DBBiblioteca"
:setvar DefaultFilePrefix "DBBiblioteca"
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
/*
A coluna Author na tabela [dbo].[TBBook] deve ser alterada de NULL para NOT NULL. Se a tabela contiver dados, o script ALTER talvez não funcione. Para evitar o problema, você deve adicionar valores a essa coluna para todas as linhas, marcá-la para permitir valores NULL ou habilitar a geração de padrões inteligentes como uma opção de implantação.

A coluna DatePost na tabela [dbo].[TBBook] deve ser alterada de NULL para NOT NULL. Se a tabela contiver dados, o script ALTER talvez não funcione. Para evitar o problema, você deve adicionar valores a essa coluna para todas as linhas, marcá-la para permitir valores NULL ou habilitar a geração de padrões inteligentes como uma opção de implantação.

A coluna IsAvaliable na tabela [dbo].[TBBook] deve ser alterada de NULL para NOT NULL. Se a tabela contiver dados, o script ALTER talvez não funcione. Para evitar o problema, você deve adicionar valores a essa coluna para todas as linhas, marcá-la para permitir valores NULL ou habilitar a geração de padrões inteligentes como uma opção de implantação.

A coluna Theme na tabela [dbo].[TBBook] deve ser alterada de NULL para NOT NULL. Se a tabela contiver dados, o script ALTER talvez não funcione. Para evitar o problema, você deve adicionar valores a essa coluna para todas as linhas, marcá-la para permitir valores NULL ou habilitar a geração de padrões inteligentes como uma opção de implantação.

A coluna Title na tabela [dbo].[TBBook] deve ser alterada de NULL para NOT NULL. Se a tabela contiver dados, o script ALTER talvez não funcione. Para evitar o problema, você deve adicionar valores a essa coluna para todas as linhas, marcá-la para permitir valores NULL ou habilitar a geração de padrões inteligentes como uma opção de implantação.

A coluna Volume na tabela [dbo].[TBBook] deve ser alterada de NULL para NOT NULL. Se a tabela contiver dados, o script ALTER talvez não funcione. Para evitar o problema, você deve adicionar valores a essa coluna para todas as linhas, marcá-la para permitir valores NULL ou habilitar a geração de padrões inteligentes como uma opção de implantação.
*/

IF EXISTS (select top 1 1 from [dbo].[TBBook])
    RAISERROR (N'Linhas foram detectadas. A atualização de esquema está sendo encerrada porque pode ocorrer perda de dados.', 16, 127) WITH NOWAIT

GO
/*
A coluna BookId na tabela [dbo].[TBLoan] deve ser alterada de NULL para NOT NULL. Se a tabela contiver dados, o script ALTER talvez não funcione. Para evitar o problema, você deve adicionar valores a essa coluna para todas as linhas, marcá-la para permitir valores NULL ou habilitar a geração de padrões inteligentes como uma opção de implantação.

A coluna DateDevolution na tabela [dbo].[TBLoan] deve ser alterada de NULL para NOT NULL. Se a tabela contiver dados, o script ALTER talvez não funcione. Para evitar o problema, você deve adicionar valores a essa coluna para todas as linhas, marcá-la para permitir valores NULL ou habilitar a geração de padrões inteligentes como uma opção de implantação.

A coluna NameClient na tabela [dbo].[TBLoan] deve ser alterada de NULL para NOT NULL. Se a tabela contiver dados, o script ALTER talvez não funcione. Para evitar o problema, você deve adicionar valores a essa coluna para todas as linhas, marcá-la para permitir valores NULL ou habilitar a geração de padrões inteligentes como uma opção de implantação.
*/

IF EXISTS (select top 1 1 from [dbo].[TBLoan])
    RAISERROR (N'Linhas foram detectadas. A atualização de esquema está sendo encerrada porque pode ocorrer perda de dados.', 16, 127) WITH NOWAIT

GO
PRINT N'Removendo [dbo].[FK_TBLoan_ToTBBook]...';


GO
ALTER TABLE [dbo].[TBLoan] DROP CONSTRAINT [FK_TBLoan_ToTBBook];


GO
PRINT N'Alterando [dbo].[TBBook]...';


GO
ALTER TABLE [dbo].[TBBook] ALTER COLUMN [Author] VARCHAR (100) NOT NULL;

ALTER TABLE [dbo].[TBBook] ALTER COLUMN [DatePost] DATE NOT NULL;

ALTER TABLE [dbo].[TBBook] ALTER COLUMN [IsAvaliable] BIT NOT NULL;

ALTER TABLE [dbo].[TBBook] ALTER COLUMN [Theme] VARCHAR (100) NOT NULL;

ALTER TABLE [dbo].[TBBook] ALTER COLUMN [Title] VARCHAR (100) NOT NULL;

ALTER TABLE [dbo].[TBBook] ALTER COLUMN [Volume] INT NOT NULL;


GO
PRINT N'Alterando [dbo].[TBLoan]...';


GO
ALTER TABLE [dbo].[TBLoan] ALTER COLUMN [BookId] INT NOT NULL;

ALTER TABLE [dbo].[TBLoan] ALTER COLUMN [DateDevolution] DATE NOT NULL;

ALTER TABLE [dbo].[TBLoan] ALTER COLUMN [NameClient] VARCHAR (100) NOT NULL;


GO
PRINT N'Criando [dbo].[FK_TBLoan_ToTBBook]...';


GO
ALTER TABLE [dbo].[TBLoan] WITH NOCHECK
    ADD CONSTRAINT [FK_TBLoan_ToTBBook] FOREIGN KEY ([BookId]) REFERENCES [dbo].[TBBook] ([IdBook]);


GO
PRINT N'Verificando os dados existentes em restrições recém-criadas';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[TBLoan] WITH CHECK CHECK CONSTRAINT [FK_TBLoan_ToTBBook];


GO
PRINT N'Atualização concluída.';


GO
