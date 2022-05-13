using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace MinimartApi.Models
{
	/// <summary>
	/// Generic Voucher
	/// </summary>
	public abstract class VoucherPromoModel
	{
	
		[Required( ErrorMessage="MinimartId Required")]
		public int MinimartId { get; set; }
	
		[Required(ErrorMessage = "VIniNumber Required")]
		public int VIniNumber { get; set; }
		
		[Required(ErrorMessage = "VEndNumber Required")]
		public int VEndNumber { get; set; }
		
		[Required(ErrorMessage = "Name Required")]
		public string Name { get; set; }
		
		public String Description { get; set; }
		
		[Required(ErrorMessage = "StartDate Required")]
		public DateTime StartDate { get; set; }
		
		[Required(ErrorMessage = "EndDate Required")]
		public DateTime EndDate { get; set; }
		
		public List<DayOfWeek> WeekDays { get; set; }
		
		[Required(ErrorMessage = "StartingWithXUnits Required")]
		public int StartingWithXUnits { get; set; }
		
		[Required(ErrorMessage = "UnitOnDiscount Required")]
		public int UnitOnDiscount { get; set; }
		
		[Required(ErrorMessage = "PercentageDiscount Required")]
		public int PercentageDiscount { get; set; }
	}

	/// <summary>
	/// Product Voucher
	/// </summary>
	public class ProductVoucherPromoModel : VoucherPromoModel
	{
		[Required(ErrorMessage = "includeProducts Required")]
		public List<ProductVoucherModel> includeProducts;
	}

	/// <summary>
	/// Category Voucher
	/// </summary>
	public class CategoryVoucherPromoModel : VoucherPromoModel
	{
		[Required(ErrorMessage = "id_Category Required")]
		public int CategoryId;

		public List<ProductVoucherModel> excludeProducts;
	}


	public class VoucherPromo //: ProductVoucherPromoModel
	{
		public int MinimartId { get; set; }
		public String MinimartName { get; set; }
		public int VoucherId { get; set; }
		public string VoucherName { get; set; }
		public String VoucherDescription { get; set; }
		public String VIniNumber { get; set; }
		public String VEndNumber { get; set; }
		//
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public String WeekDays { get; set; }
		public int StartingWithXUnits { get; set; }
		public int UnitOnDiscount { get; set; }
		public int PercentageDiscount { get; set; }
		//public int TipoVoucher { get; set; }
		}
		
	public class ProductVoucherPromo: VoucherPromo  
	{
		//Included Products
		//public List<Product>  includedProducts { get; set; }  [
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int ProductId { get; set; } 
		public string ProductName { get; set; }
		public float Price { get; set; }
		//]
		
	}

	public class CategoryVoucherPromo : VoucherPromo
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }

		//Excluded Products
		//TODO: Convert in a field 
		//public List<Product> excludedProducts { get; set; } [
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public float Price { get; set; }
		//]
	}



	public class CategoryVoucherPromo_ExcProduct
	{
		public int VoucherId { get; set; }
		public string VoucherName { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int ExcludeProductId { get; set; }
		public string ExcludeProductName { get; set; }
	}
}