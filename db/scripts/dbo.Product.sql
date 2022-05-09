CREATE TABLE [dbo].[Product] (
    [Id]          INT             NOT NULL IDENTITY,
    [Name]        NCHAR (200)     NOT NULL,
    [Description] NCHAR (500)     NULL,
    [Price]       NUMERIC (18, 2) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

