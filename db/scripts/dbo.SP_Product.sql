/*
return all products order by category name and product name
filters: product id or name / category id or name
*/

CREATE   PROCEDURE [dbo].[SP_Product]
	@category_id int = 0,      
	@category_name nchar(200) = '',
	@product_id int = 0,
	@product_name nchar(200) = ''
AS

begin 
	SELECT c.Id categoryId, LTrim(RTrim(c.Name)) categoryName , p.Id productId, LTrim(RTrim(p.Name)) productName, LTrim(RTrim(p.Description)) as productDescription, p.Price
	  FROM Product p
	  join Product_Category pc on (p.Id = pc.Id_Product )
      join Category AS c on ( c.Id = pc.id_Category)
     WHERE ((p.Id = @product_id or @product_id = 0) and (Upper(LTrim(RTrim(p.Name))) Like '%' + Upper(LTrim(RTrim(@product_name))) + '%' or  @product_name = ''))
	   and ((c.Id =@category_id or @category_id = 0) and (Upper(LTrim(RTrim(c.Name))) Like '%' + Upper(LTrim(RTrim(@category_name))) + '%' or  @category_name = ''))
    ORDER BY c.Name, p.Name 

end