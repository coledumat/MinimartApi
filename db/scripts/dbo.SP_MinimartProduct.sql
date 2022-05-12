/*
return all products in stock 
order by category name and product name
filters: minimart sucursal id or name / product id or name / category id or name/
         lowStock 0 return available stock - lowStock 1 retrun stock under minimum amount

*/

CREATE   PROCEDURE [dbo].[SP_MinimartProduct]
    @minimart_id int = 0,
	@minimart_name nchar(200) = '',
	@product_id int = 0,              
	@product_name nchar(200) = '',    
	@category_id int = 0,             
	@category_name nchar(200) = '',    
	@lowStock bit = 0 --1 true 0 false
AS

begin 
	SELECT m.Id as minimartId, 
	       LTrim(RTrim(m.Name)) as minimartName,
		   LTrim(RTrim(m.Adress)) as minimartAdress,
		   --
		   c.Id as categoryId,
		   LTrim(RTrim(c.Name)) as categoryName,
		   LTrim(RTrim(c.Description)) as categoryDescription,
		   --
		   p.Id as productId, 
		   LTrim(RTrim(p.Name)) as productName,
		   LTrim(RTrim(p.Description)) as productDescription,
		   p.Price,
		   --
		   mp.Stock ,
		   mp.MinimumStock 
	  FROM 
	       Product p 
	  join Product_Category pc on (p.Id = pc.Id_Product )
      join Category AS c on ( c.Id = pc.id_Category)
	  left join Minimart_Product mp on (mp.Id_product = p.Id )
	  left join Minimart m on (m.Id = mp.Id_Minimart) 
     WHERE ((m.Id = @minimart_id or @minimart_id = 0) and (Upper(LTrim(RTrim(m.Name))) Like  '%' + Upper(LTrim(RTrim(@minimart_name))) + '%' or  @minimart_name = ''))
	   and ((p.Id = @product_id or @product_id = 0) and (Upper(LTrim(RTrim(p.Name))) Like '%' + Upper(LTrim(RTrim(@product_name))) + '%' or  @product_name = ''))
	   and ((c.Id =@category_id or @category_id = 0) and (Upper(LTrim(RTrim(c.Name))) Like '%' + Upper(LTrim(RTrim(@category_name))) + '%' or  @category_name = ''))
	   and ((mp.Stock > 0 and @LowStock = 0)                --available stock
	        or 
			(@LowStock = 1 and mp.Stock < mp.MinimumStock)	--stock under minimum amount
			)
    ORDER BY m.Name, c.Name, p.Name 

end