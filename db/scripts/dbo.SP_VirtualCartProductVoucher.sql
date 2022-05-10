/*
return all VirtualCart_Vouvher  (CategoryVoucherPromo and ProductVucherPromo)
filters: product id or name / category id or name
*/

CREATE   PROCEDURE [dbo].[SP_VirtualCartProductVoucher]
    @minimart_id int = 0,
	@minimart_name nchar(200) = '',
	@customer_id int = 0,
	@customer_fullName nchar(200) = '',
	@voucher_id int = 0,
	@voucher_num int = 0,
	--@vocher_type int = 0,      --0 ProductVoucher / 1 CategoryVoucher
	@category_id int = 0,             
	@category_name nchar(200) = '', 
	@product_id int = 0,              
	@product_name nchar(200) = ''    

AS

begin 
	SELECT m.Id as minimartId, 
	       LTrim(RTrim(m.Name)) as minimartName,
		   --
		   cu.Id as customerId, 
	       LTrim(RTrim(cu.Name)) + ' ' + LTrim(RTrim(cu.LastName)) as customerFullName,
		   --
		   vp.Id as VoucherId,
		   LTrim(RTrim(vp.Name)) as VoucherName,
		   LTrim(RTrim(vp.Description)) as VoucherDescription,
		   vv.Num_Voucher as NumVoucher,
		   vp.StartDate,
		   vp.EndDate,
		   LTrim(RTrim(vp.WeekDays)) as WeekDays,
		   vp.StartingWhitXUnits,
		   vp.UnitOnDiscount,
		   vp.PercentageDiscount,
		   vp.TipoVoucher,
		   --
		   c.Id as categoryId,
		   LTrim(RTrim(c.Name)) as categoryName,
		   p.Id as productId, 
		   LTrim(RTrim(p.Name)) as productName,
		   p.Price
	  FROM VirtualCart_Voucher vv
	  join Minimart m on (m.Id = vv.Id_Minimart) 
	  join Customer cu on (cu.Id = vv.Id_Customer) 
	  join VoucherPromo vp on (vp.Id = vv.Id_Voucher)
	  --
	  join ProductVoucherPromo pvp on (pvp.Id_Voucher = vp.Id)
	  join Product p on (p.Id = pvp.Id_Product)
	  join Product_Category pc on (p.Id = pc.Id_Product )
      join Category c on ( c.Id = pc.id_Category)
     WHERE ((m.Id = @minimart_id or @minimart_id = 0) and (Upper(LTrim(RTrim(m.Name))) Like  '%' + Upper(LTrim(RTrim(@minimart_name))) + '%' or  @minimart_name = ''))
	   and ((cu.Id = @customer_id or @customer_id = 0) and (Upper(LTrim(RTrim(cu.Name)) + ' ' + LTrim(RTrim(cu.LastName))) Like  '%' + Upper(LTrim(RTrim(@customer_fullName))) + '%' or  @customer_fullName = ''))
       --
       and (vv.Id_Voucher = @voucher_id or @voucher_id = 0)
	   and (vv.Num_Voucher = @voucher_num or @voucher_num = 0)
	   --
	   and ((p.Id = @product_id or @product_id = 0) and (Upper(LTrim(RTrim(p.Name))) Like '%' + Upper(LTrim(RTrim(@product_name))) + '%' or  @product_name = ''))
	   and ((c.Id =@category_id or @category_id = 0) and (Upper(LTrim(RTrim(c.Name))) Like '%' + Upper(LTrim(RTrim(@category_name))) + '%' or  @category_name = ''))
    ORDER BY m.Id, cu.Id, vp.Id, c.Name, p.Name 

end