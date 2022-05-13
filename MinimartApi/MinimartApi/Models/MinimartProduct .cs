using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinimartApi.Models
{

    public class MinimartProductModel
    {
       public int MinimartId { get; set; }
        public int ProductId { get; set; }
        //
        public int Stock { get; set; }
        public int MinimumStock { get; set; }
    }

    public class MinimartProduct: MinimartProductModel
    {
        //public int MinimartId { get; set; }
        public String MinimartName { get; set; }
        public String MinimartAdress { get; set; }
        //public int CategoryId { get; set; }
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }
        public String CategoryDescription { get; set; }
        
        public String ProductName { get; set; }
        public String ProductDescription { get; set; }
        public float Price { get; set; }
        //
        //public int Stock { get; set; }
        //public int MinimumStock { get; set; }

    }
}