CREATE TABLE [dbo].[VirtualCart_Product] (
    [Id_Minimart] INT NOT NULL,
    [Id_Customer] INT NOT NULL,
    [Id_Product]  INT NOT NULL,
    [Units]       INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id_Minimart] ASC, [Id_Product] ASC, [Id_Customer] ASC)
);


GO

CREATE TRIGGER [TRD_VirtualCart_Product]
	ON [dbo].[VirtualCart_Product]
	 FOR DELETE
	AS
	--update stock in the minimart
	BEGIN
		SET NOCOUNT ON

		declare @stock_desc int ;
		declare @minimart int;
		declare @product int;
		declare @units int;
		
		select @minimart = i.Id_Minimart, @product = i.Id_Product, @units = i.Units  
		  from inserted as i;

		--update stock in the minimart
		UPDATE Minimart_Product 
		   SET Stock = Stock + @units
         WHERE Id_Minimart = @minimart
		    AND Id_Product  = @product
			;

	END
GO
CREATE TRIGGER [TRI_VirtualCart_Product]
	ON [dbo].[VirtualCart_Product]
	 FOR INSERT
	AS
	--chek and update stock in the minimart
	BEGIN
		SET NOCOUNT ON

		declare @stock_desc int ;
		declare @minimart int;
		declare @product int;
		declare @units int;
		
		select @minimart = i.Id_Minimart, @product = i.Id_Product, @units = i.Units  
		  from inserted as i;

		--chek stock in the minimart
		SELECT @stock_desc =  mp.Stock - @units
		  FROM Minimart_Product AS mp
		 WHERE mp.Id_Minimart = @minimart
		   AND mp.Id_Product  = @product
		   ;
		

		IF (@stock_desc >= 0) 
		BEGIN
		    --update stock in the minimart
			UPDATE Minimart_Product 
			   SET Stock = Stock - @units
			 WHERE Id_Minimart = @minimart
		       AND Id_Product  = @product
			   ;
		END
		ELSE 
			ROLLBACK;
	END