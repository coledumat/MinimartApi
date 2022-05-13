using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using MinimartApi.Business;
using MinimartApi.Models;

namespace MinimartApi.Controllers
{
    /// <summary>
    ///    CRUD methods of Customers.
    /// </summary>
    public class CustomerController : ApiController
    {
        
        private BCustomer customer;

        CustomerController()
        {
            customer = new BCustomer();

        }

        [HttpGet]
        [Route("api/customer/list")]
        public IEnumerable<Customer> GetCustomers(int customerId = 0, string customerFullName = "", string email = "") 
        {
            return customer.list(customerId, customerFullName, email);
        }


        [HttpPost]
        [Route("api/customer")]
        public int PostCustomer([FromBody] CustomerModel newCostumer)
        {   //add a new customer 

            int id = 0;
            //insert into Minimart_Product Table
            return id;
        }

        [HttpPut]
        [Route("api/customer/{Int Id}")]
        public void PutProduct(int Id, [FromBody] CustomerModel aCustomer)
        {   //update a customer 

        }

        [HttpDelete]
        [Route("api/customer")]
        public void DeleteProduct(int id)
        { //delete a customer

        }

    }
}
