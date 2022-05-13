/*
return all CategoryVoucherPromo_ExcProduct  
filters: minimart id or name / customer id or name / voucher id or Num_Voucher / category id or name / excludeProduct id or name
*/

CREATE   PROCEDURE [dbo].[SP_CategoryVoucherPromo_ExcProduct]
	@voucher_id int = 0,
	@category_id int = 0,             
	@category_name nchar(200) = '', 
	@exclude_product_id int = 0,              
	@exclude_product_name nchar(200) = ''    

AS

begin 
	SELECT vp.Id as VoucherId,
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
		   p.Id as ExcludeProductId, 
		   LTrim(RTrim(p.Name)) as ExcludeProduct,
		   p.Price
	  FROM VoucherPromo vp 
	  --
	  join CategoryVoucherPromo cvp on (cvp.Id_Voucher = vp.Id)
	  join Category c on ( c.Id = cvp.id_Category)
      --
	  join CategoryVoucherPromo_ExcProduct cvep on (cvep.Id_Category = cvp.Id_Category and cvep.Id_Voucher = vp.Id)
	  join Product p on (p.Id = cvep.Id_Product)
     WHERE (vp.Id = @voucher_id or @voucher_id = 0)
	   --
	   and ((c.Id =@category_id or @category_id = 0) and (Upper(LTrim(RTrim(c.Name))) Like '%' + Upper(LTrim(RTrim(@category_name))) + '%' or  @category_name = ''))
	   --
	   and ((p.Id = @exclude_product_id or @exclude_product_id = 0) and (Upper(LTrim(RTrim(p.Name))) Like '%' + Upper(LTrim(RTrim(@exclude_product_name))) + '%' or  @exclude_product_name = ''))
    ORDER BY vp.Id, c.Name, p.Name 

end