using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinimartApi.Models
{
    public class Product
    {
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }
        public int ProductId { get; set; }
        public String ProductName { get; set; }
        public String ProductDescription { get; set; }
        public float Price { get; set; }
    }
}