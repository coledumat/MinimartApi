/*
return all VirtualCartCAtegoryVoucher  (a voucherNum of CategoryVoucher in a VirtualCar)
filters: minimart id or name / customer id or name / voucher id or Num_Voucher / category id or name / excludeProduct id or name
*/

CREATE   PROCEDURE [dbo].[SP_VirtualCartCategoryVoucher]
    @minimart_id int = 0,
	@minimart_name nchar(200) = '',
	@customer_id int = 0,
	@customer_fullName nchar(200) = '',
	@voucher_id int = 0,
	@voucher_num int = 0,
	--@vocher_type int = 0,      --0 ProductVoucher / 1 CategoryVoucher
	@category_id int = 0,             
	@category_name nchar(200) = '', 
	@exclude_product_id int = 0,              
	@exclude_product_name nchar(200) = ''    

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
		   vp.StartingWithXUnits,
		   vp.UnitOnDiscount,
		   vp.PercentageDiscount,
		   vp.TipoVoucher,
		   --
		   c.Id as categoryId,
		   LTrim(RTrim(c.Name)) as categoryName,
		   p.Id as ExcludeProductId, 
		   LTrim(RTrim(p.Name)) as ExcludeProduct,
		   p.Price
	  FROM VirtualCart_Voucher vv
	  join Minimart m on (m.Id = vv.Id_Minimart) 
	  join Customer cu on (cu.Id = vv.Id_Customer) 
	  --
	  join VoucherPromo vp on (vp.Id = vv.Id_Voucher)
	  --
	  join CategoryVoucherPromo cvp on (cvp.Id_Voucher = vp.Id)
	  join Category c on ( c.Id = cvp.id_Category)
      --
	  join CategoryVoucherPromo_ExcProduct cvep on (cvep.Id_Category = cvp.Id_Category and cvep.Id_Voucher = vp.Id)
	  join Product p on (p.Id = cvep.Id_Product)
     WHERE ((m.Id = @minimart_id or @minimart_id = 0) and (Upper(LTrim(RTrim(m.Name))) Like  '%' + Upper(LTrim(RTrim(@minimart_name))) + '%' or  @minimart_name = ''))
	   and ((cu.Id = @customer_id or @customer_id = 0) and (Upper(LTrim(RTrim(cu.Name)) + ' ' + LTrim(RTrim(cu.LastName))) Like  '%' + Upper(LTrim(RTrim(@customer_fullName))) + '%' or  @customer_fullName = ''))
       --
       and (vv.Id_Voucher = @voucher_id or @voucher_id = 0)
	   and (vv.Num_Voucher = @voucher_num or @voucher_num = 0)
	   --
	   and ((c.Id =@category_id or @category_id = 0) and (Upper(LTrim(RTrim(c.Name))) Like '%' + Upper(LTrim(RTrim(@category_name))) + '%' or  @category_name = ''))
	   --
	   and ((p.Id = @exclude_product_id or @exclude_product_id = 0) and (Upper(LTrim(RTrim(p.Name))) Like '%' + Upper(LTrim(RTrim(@exclude_product_name))) + '%' or  @exclude_product_name = ''))
    ORDER BY m.Id, cu.Id, vp.Id, c.Name, p.Name 

end