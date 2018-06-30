﻿/*
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
PRINT N'Atualização concluída.';


GO
