CREATE TABLE [dbo].[ProductVoucherPromo] (
    [Id_Voucher] INT NOT NULL,
    [Id_Product] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Voucher] ASC, [Id_Product] ASC)
);

