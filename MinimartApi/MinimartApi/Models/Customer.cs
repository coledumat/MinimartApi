using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace MinimartApi.Models

{
    
    public class CustomerModel
    {
        [Required(ErrorMessage = "You must enter an Name")]
        [StringLength(50, ErrorMessage = "Maximum Name length: 200 characters")]
        public String Name { get; set; }

        [Required(ErrorMessage = "You must enter a Last Name")]
        [StringLength(50, ErrorMessage = "Maximum Last Name length: 200 characters")]
        public String LastName { get; set; }

        [Required(ErrorMessage = "You must enter an Adress")]
        [StringLength(50, ErrorMessage = "Maximum Adress length: 200 characters")]
        public String Adress { get; set; }
        
        [StringLength(50, ErrorMessage= "Maximum Email length: 50 characters")]
        [EmailAddress(ErrorMessage="Email incorrect format")]
        public String Email { get; set; }

        //TODO: add all properties 
    }

    public class Customer :CustomerModel
    {
        public int Id { get; set; }


    }


}