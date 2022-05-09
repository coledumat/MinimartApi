CREATE TABLE [dbo].[VirtualCart_Product] (
    [Id_Minimart] INT NOT NULL,
    [Id_Customer] INT NOT NULL,
    [Id_Product]  INT NOT NULL,
    [Units]       INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Minimart] ASC, [Id_Product] ASC, [Id_Customer] ASC)
);

