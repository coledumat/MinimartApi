using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinimartApi.Models
{
    public class VirtualCartCategoryVoucher
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
		public String  WeekDays { get; set; }
		public int StartingWithXUnits { get; set; }
		public int UnitOnDiscount { get; set; }
		public int PercentageDiscount { get; set; }
		//public int TipoVoucher { get; set; }
		//
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		//
		//Excluded Products
		//public int ExcludeProductId { get; set; }
		//public string ExcludeProductName { get; set; }
		//public float Price { get; set; } //informative 
        //public List<Product>  Products { get; set; }

	}


}