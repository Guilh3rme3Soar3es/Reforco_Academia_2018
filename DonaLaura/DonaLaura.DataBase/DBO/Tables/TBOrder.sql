﻿CREATE TABLE [dbo].[TBOrder]
(
	[IdOrder] INT NOT NULL IDENTITY(1,1), 
    [Cliente ] VARCHAR(100) NOT NULL, 
    [Lucro] DECIMAL(18, 2) NOT NULL, 
    [Quantidade] INT NOT NULL, 
    [ProductId] INT NOT NULL,
	CONSTRAINT [FK_TBOrder_ToTBProduct] FOREIGN KEY ([ProductId]) REFERENCES [TBProduct]([IdProduct]),
    CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED
(
	[IdOrder] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
	
