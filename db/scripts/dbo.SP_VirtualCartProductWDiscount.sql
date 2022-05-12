/*
borrar no se usa

*/

CREATE   PROCEDURE [dbo].[SP_VirtualCartProductWDiscount]
    @minimart_id int = 0,
	@minimart_name nchar(200) = '',
	@customer_id int = 0,
	@customer_fullName nchar(200) = '',
	@category_id int = 0,             
	@category_name nchar(200) = '',    
	@product_id int = 0,              
	@product_name nchar(200) = '',    
	@voucher_id int = 0,
	@voucher_num int = 0
AS

begin

    --tomar en un cursor
	SELECT m.Id as minimartId, 
	       LTrim(RTrim(m.Name)) as minimartName,
		   --
		   cu.Id as customerId, 
	       LTrim(RTrim(cu.Name)) + ' ' + LTrim(RTrim(cu.LastName)) as customerFullName,
		   --
		   c.Id as categoryId,
		   LTrim(RTrim(c.Name)) as categoryName,
		   --
		   p.Id as productId, 
		   LTrim(RTrim(p.Name)) as productName,
		   p.Price,
		   --
           vcp.Units 
	  FROM VirtualCart_Product vcp
	  join Minimart m on (m.Id = vcp.Id_Minimart) 
	  join Customer cu on (cu.Id = vcp.Id_Customer) 
	  join Product p on (p.Id = vcp.Id_product)
	  join Product_Category pc on (pc.Id_Product = p.Id)
      join Category AS c on ( c.Id = pc.id_Category)
     WHERE ((m.Id = @minimart_id or @minimart_id = 0) and (Upper(LTrim(RTrim(m.Name))) Like  '%' + Upper(LTrim(RTrim(@minimart_name))) + '%' or  @minimart_name = ''))
	   and ((cu.Id = @customer_id or @customer_id = 0) and (Upper(LTrim(RTrim(cu.Name)) + ' ' + LTrim(RTrim(cu.LastName))) Like  '%' + Upper(LTrim(RTrim(@customer_fullName))) + '%' or  @customer_fullName = ''))
	   and ((p.Id = @product_id or @product_id = 0) and (Upper(LTrim(RTrim(p.Name))) Like '%' + Upper(LTrim(RTrim(@product_name))) + '%' or  @product_name = ''))
	   and ((c.Id =@category_id or @category_id = 0) and (Upper(LTrim(RTrim(c.Name))) Like '%' + Upper(LTrim(RTrim(@category_name))) + '%' or  @category_name = ''))
    ORDER BY m.Name, cu.Name, c.Name, p.Name 


	--tomar en un cursor los ProductVoucher

	--para cada producto en el virtualcart ver si aparece en un voucher valido cargado en el virtual cart (deberia estar validado al agregarlo al virtual cart)
	
	--calcular descuento y agregarlo al primer cursor

end