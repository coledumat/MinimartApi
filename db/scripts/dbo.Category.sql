CREATE TABLE [dbo].[Category] (
    [Id]          INT         IDENTITY (1, 1) NOT NULL,
    [Name]        NCHAR (200) NOT NULL,
    [Description] NCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

