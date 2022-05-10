using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinimartApi.Models
{
	public class VoucherPromo
	{
		public int MinimartId { get; set; }
		public String MinimartName { get; set; }
		public int CustomerId { get; set; }
		public String CustomerFullName { get; set; }
		public int VoucherId { get; set; }
		public string VoucherName { get; set; }
		public String VoucherDescription { get; set; }
		public int NumVoucher { get; set; }
		//
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public String WeekDays { get; set; }
		public int StartingWhitXUnits { get; set; }
		public int UnitOnDiscount { get; set; }
		public int PercentageDiscount { get; set; }
		//public int TipoVoucher { get; set; }
		}
		
	public class ProductVoucherPromo: VoucherPromo
	{ 
		//Included Products
		//public List<Product>  Products { get; set; }
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int ProductId { get; set; } 
		public string ProductName { get; set; }
		public float Price { get; set; }
		
	}

	public class CategoryVoucherPromo : VoucherPromo
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		//Excluded Products
		//public List<Product>  Products { get; set; }
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public float Price { get; set; }
	}

}