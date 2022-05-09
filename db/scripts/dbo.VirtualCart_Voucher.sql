CREATE TABLE [dbo].[VirtualCart_Voucher] (
    [Id_Minimart] INT NOT NULL,
    [Id_Customer] INT NOT NULL,
    [Id_Voucher]  INT NOT NULL,
    [Num_Voucher] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Minimart] ASC, [Id_Customer] ASC, [Id_Voucher] ASC)
);

