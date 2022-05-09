CREATE TABLE [dbo].[VoucherPromo] (
    [Id]                 INT         IDENTITY (1, 1) NOT NULL,
    [Id_Minimart]        INT         NOT NULL,
    [VIniNumber]         INT         NOT NULL,
    [VEndNumber]         INT         NOT NULL,
    [Name]               NCHAR (200) NOT NULL,
    [Description]        NCHAR (200) NOT NULL,
    [StartDate]          DATE        NOT NULL,
    [EndDate]            DATE        NOT NULL,
    [WeekDays]           NCHAR (200) NOT NULL,
    [StartingWhitXUnits] INT         NOT NULL,
    [UnitOnDiscount]     INT         NOT NULL,
    [PercentageDiscount] INT         NOT NULL,
    [TipoVoucher]        INT         DEFAULT ((1)) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC, [Id_Minimart] ASC)
);

