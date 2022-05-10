/*
return all VirtualCart_Vouvher  (CategoryVoucherPromo and ProductVucherPromo)
filters: product id or name / category id or name
*/

CREATE   PROCEDURE [dbo].[SP_ProductVoucherPromo]
    @minimart_id int = 0,
	@minimart_name nchar(200) = '',
	@voucher_id int = 0,
	@category_id int = 0,             
	@category_name nchar(200) = '', 
	@product_id int = 0,              
	@product_name nchar(200) = ''    

AS

begin 
	SELECT m.Id as minimartId, 
	       LTrim(RTrim(m.Name)) as minimartName,
		   --
		   vp.Id as VoucherId,
		   vp.VIniNumber,
		   vp.VEndNumber, 
		   LTrim(RTrim(vp.Name)) as VoucherName,
		   LTrim(RTrim(vp.Description)) as VoucherDescription,
		   vp.StartDate,
		   vp.EndDate,
		   LTrim(RTrim(vp.WeekDays)) as WeekDays,
		   vp.StartingWithXUnits,
		   vp.UnitOnDiscount,
		   vp.PercentageDiscount,
		   vp.TipoVoucher,
		   --
		   c.Id as categoryId,
		   LTrim(RTrim(c.Name)) as categoryName,
		   p.Id as productId, 
		   LTrim(RTrim(p.Name)) as productName,
		   p.Price
	  FROM VoucherPromo vp
	  join Minimart m on (m.Id = vp.Id_Minimart) 
	  --
	  join ProductVoucherPromo pvp on (pvp.Id_Voucher = vp.Id)
	  join Product p on (p.Id = pvp.Id_Product)
	  join Product_Category pc on (p.Id = pc.Id_Product )
      join Category c on ( c.Id = pc.id_Category)
     WHERE ((m.Id = @minimart_id or @minimart_id = 0) and (Upper(LTrim(RTrim(m.Name))) Like  '%' + Upper(LTrim(RTrim(@minimart_name))) + '%' or  @minimart_name = ''))
       --
       and (vp.Id = @voucher_id or @voucher_id = 0)
	   --
	   and ((p.Id = @product_id or @product_id = 0) and (Upper(LTrim(RTrim(p.Name))) Like '%' + Upper(LTrim(RTrim(@product_name))) + '%' or  @product_name = ''))
	   and ((c.Id =@category_id or @category_id = 0) and (Upper(LTrim(RTrim(c.Name))) Like '%' + Upper(LTrim(RTrim(@category_name))) + '%' or  @category_name = ''))
    ORDER BY m.Id, vp.Id, c.Name, p.Name 

end