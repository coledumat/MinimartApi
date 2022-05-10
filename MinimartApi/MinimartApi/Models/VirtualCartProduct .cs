using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinimartApi.Models
{
    public class VirtualCartProduct
    {
        public int MinimartId { get; set; }
        public String MinimartName { get; set; }
        public int CustomerId { get; set; }
        public String CustomerFullName { get; set; }
        //
        public int CategoryId { get; set; }
        public String CategoryName { get; set; }
        public int ProductId { get; set; }
        public String ProductName { get; set; }
        public float Price { get; set; }
        //
        public int Units { get; set; }
        public float SubtotalProduct { get { return Price * Units; } }
    }

    public class VirtualCartProductWDiscount : VirtualCartProduct
    {
        public float SubtotalWithDiscount { get; set; }
        //public int VoucherId { get; set; }    //can be more of one
        //public int VoucherNum { get; set; }

    }


    }