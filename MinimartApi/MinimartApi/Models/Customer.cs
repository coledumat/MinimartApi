using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MinimartApi.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String LastName { get; set; }
        public String Adress { get; set; }
        public String Email { get; set; }

        //TODO: add all properties 
    }
}