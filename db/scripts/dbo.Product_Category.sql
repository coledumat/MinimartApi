CREATE TABLE [dbo].[Product_Category] (
    [Id_Product]  INT NOT NULL,
    [Id_Category] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Category] ASC, [Id_Product] ASC)
);

