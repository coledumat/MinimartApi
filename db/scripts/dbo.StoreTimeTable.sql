CREATE TABLE [dbo].[StoreTimeTable] (
    [Id_Minimart] INT        NOT NULL,
    [WorkingDay]  NCHAR (20) NOT NULL,
    [OpeningTime] TIME (7)   NOT NULL,
    [ClosingTime] TIME (7)   NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Minimart] ASC, [WorkingDay] ASC)
);

