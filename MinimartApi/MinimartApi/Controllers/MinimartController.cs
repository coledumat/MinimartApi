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
    public class MinimartController : ApiController
    {
        /*
            CRUD methods of Minimarts, StoreTimeTable and its stock of products.
        */


        private BMinimartProduct minimartProduct;

        public MinimartController ()
        {
            minimartProduct = new BMinimartProduct();

        }

        [HttpGet]
        [Route("api/minimart/product/list")]
        public IEnumerable<MinimartProduct> GetProducts(int minimartId = 0, string minimartName = "", int productId = 0, string productName = "", int categoryId = 0, string categoryName = "", Boolean lowStock = false) //mco
        {
            return minimartProduct.listar(minimartId, minimartName, productId, productName, categoryId, categoryName, lowStock);
        }



        // GET: api/Minimart
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Minimart/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Minimart
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Minimart/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Minimart/5
        public void Delete(int id)
        {
        }
    }
}
