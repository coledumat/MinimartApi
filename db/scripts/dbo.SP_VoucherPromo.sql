/*
return all VouvherPromo  (only header)
filters: minimart id or name / voucher id
*/

CREATE   PROCEDURE [dbo].[SP_VoucherPromo]
    @minimart_id int = 0,
	@minimart_name nchar(200) = '',
	@voucher_id int = 0
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
		   vp.TipoVoucher
	  FROM VoucherPromo vp
	  join Minimart m on (m.Id = vp.Id_Minimart) 
     WHERE ((m.Id = @minimart_id or @minimart_id = 0) and (Upper(LTrim(RTrim(m.Name))) Like  '%' + Upper(LTrim(RTrim(@minimart_name))) + '%' or  @minimart_name = ''))
       --
       and (vp.Id = @voucher_id or @voucher_id = 0)
    ORDER BY m.Id, vp.Id

end