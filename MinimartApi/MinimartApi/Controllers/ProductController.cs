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


    //<RoutePrefix("api/data/MinimartApi")> 'mco
    public class ProductController : ApiController
    {
        /*
         CRUD methods of Categories and Products.
        */

        private BProduct product;

        ProductController  ()
        {
            product = new BProduct();
        
        }


        //Type Product
        // ////////////

        [HttpGet]
        [Route("api/product/list")]
        public IEnumerable<Product> GetProducts( int productId = 0, string productName = "", int categoryId = 0, string categoryName ="") //mco
        {
            return product.listar(productId, productName, categoryId, categoryName);
        }


        [HttpPost]
        [Route("api/product")]
        public int PostProduct([FromBody] Product newProduct)
        {   //add a new type of product 

            int id = 0;
            //insert into Minimart_Product Table
            return id;
        }

        [HttpPut]
        [Route("api/product")]
        public void PutProduct([FromBody] Product aProduct)
        {   //update a type of product 

        }


        [HttpDelete]
        [Route("api/product")]
        public void DeleteProduct(int id)
        { //delete a type of product

        }

        /*
        // GET: api/MiniMart
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MiniMart/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MiniMart
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MiniMart/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MiniMart/5
        public void Delete(int id)
        {
        }
        */
    }
}
