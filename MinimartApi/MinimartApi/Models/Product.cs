using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinimartApi.Models
{
    public class ProductModel
    {
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public String ProductName { get; set; }
        public String ProductDescription { get; set; }
        public float Price { get; set; }
    }


    public class Product: ProductModel
    {
        public String CategoryName { get; set; }
    }
}