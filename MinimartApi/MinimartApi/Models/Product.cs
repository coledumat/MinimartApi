using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinimartApi.Models
{
    public class ProductModel
    {
        public int CategoryId { get; set; }
        public String ProductName { get; set; }
        public String ProductDescription { get; set; }
        public float Price { get; set; }
    }

    public class ProductVoucherModel : ProductModel
    {
        public int ProductId { get; set; }
    }

    public class Product: ProductVoucherModel
    {
        public String CategoryName { get; set; }
    }
}