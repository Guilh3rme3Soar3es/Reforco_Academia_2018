﻿CREATE TABLE [dbo].[TBBook]
(
	[IdBook] INT NOT NULL IDENTITY(1,1), 
    [Title] VARCHAR(100) NOT NULL, 
    [Theme] VARCHAR(100) NOT NULL, 
    [Author] VARCHAR(100) NOT NULL, 
    [Volume] INT NOT NULL,
    [DatePost] DATE NOT NULL, 
    [IsAvaliable] BIT NOT NULL
	CONSTRAINT [PK_Books] PRIMARY KEY CLUSTERED

(
	[IdBook] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
