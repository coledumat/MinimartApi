﻿CREATE TABLE [dbo].[Minimart]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NCHAR(200) NOT NULL, 
    [Adress] NCHAR(200) NOT NULL, 
    [Logo] BINARY(50) NULL
)
