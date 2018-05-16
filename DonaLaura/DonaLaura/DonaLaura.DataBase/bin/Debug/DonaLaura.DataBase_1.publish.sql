/*
Script de implantação para DonaLaura

Este código foi gerado por uma ferramenta.
As alterações feitas nesse arquivo poderão causar comportamento incorreto e serão perdidas se
o código for gerado novamente.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "DonaLaura"
:setvar DefaultFilePrefix "DonaLaura"
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
PRINT N'A operação de refatoração Renomear com chave 28012724-fdae-4d0c-ad4d-2711cb52cbb6 foi ignorada; o elemento [dbo].[TBOrder].[Id] (SqlSimpleColumn) não será renomeado para IdOrder';


GO
PRINT N'Criando [dbo].[TBOrder]...';


GO
CREATE TABLE [dbo].[TBOrder] (
    [IdOrder]    INT             IDENTITY (1, 1) NOT NULL,
    [Cliente ]   VARCHAR (100)   NOT NULL,
    [Lucro]      DECIMAL (18, 2) NOT NULL,
    [Quantidade] INT             NOT NULL,
    [ProductId]  INT             NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED ([IdOrder] ASC) ON [PRIMARY]
) ON [PRIMARY];


GO
PRINT N'Criando [dbo].[FK_TBOrder_ToTBProduct]...';


GO
ALTER TABLE [dbo].[TBOrder] WITH NOCHECK
    ADD CONSTRAINT [FK_TBOrder_ToTBProduct] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[TBProduct] ([IdProduct]);


GO
-- Etapa de refatoração para atualizar o servidor de destino com logs de transação implantados
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '28012724-fdae-4d0c-ad4d-2711cb52cbb6')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('28012724-fdae-4d0c-ad4d-2711cb52cbb6')

GO

GO
PRINT N'Verificando os dados existentes em restrições recém-criadas';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [dbo].[TBOrder] WITH CHECK CHECK CONSTRAINT [FK_TBOrder_ToTBProduct];


GO
PRINT N'Atualização concluída.';


GO
