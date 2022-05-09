CREATE TABLE [dbo].[Minimart_Product] (
    [Id_Minimart]  INT NOT NULL,
    [Id_Product]   INT NOT NULL,
    [Stock]        INT NOT NULL,
    [MinimumStock] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Minimart] ASC, [Id_Product] ASC)
);

