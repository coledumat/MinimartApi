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


        private BMinimartProduct minimartProducts;
        private BMinimart minimarts;
        private BStoreTimeTable storeTimeTable;

        public MinimartController ()
        {
            minimartProducts = new BMinimartProduct();
            minimarts = new BMinimart();
            storeTimeTable = new BStoreTimeTable();
        }

        // ///////////
        // minimart
        // ///////////
        [HttpGet]
        [Route("api/minimart/list")]
        public IEnumerable<Minimart> GetMinimarts(int minimartId = 0, string minimartName = "")
        {   //list all minimarts 

            return minimarts.list(minimartId, minimartName);
        }


        [HttpPost]
        [Route("api/minimart")]
        public int PostMinimart([FromBody] Minimart newMinimart)
        {   //add a new minimart

            int id = 0;
            //insert into Minimart_Product Table
            return id;
        }

        [HttpPut]
        [Route("api/minimart")]
        public void PutMinimart([FromBody] Minimart aMinimart)
        {   //update a minimart

        }


        [HttpDelete]
        [Route("api/minimart")]
        public void DeleteMinimart(int id)
        { //delete a minimart

        }

        // ///////////////////////
        //Minimart StoreTimeTable
        // //////////////////////

        [HttpGet]
        [Route("api/storetimetable/list")]
        public IEnumerable<StoreTimeTable> GetStoretimetable(int minimartId = 0, string minimartName = "", string workingDay = "")
        {   //list all StroreTimeTable 

            return storeTimeTable.list(minimartId, minimartName, workingDay);
        }

        [HttpPost]
        [Route("api/storetimetable")]
        public int PostStoreTimeTable([FromBody] StoreTimeTable newStoreTimeTable)
        {   //add a new StoreTimeTable

            int id = 0;
            //insert into Minimart_Product Table
            return id;
        }

        [HttpPut]
        [Route("api/storetimetable")]
        public void PutStoreTimeTable([FromBody] StoreTimeTable aStoreTimeTable)
        {   //update a StoreTimeTable

        }


        [HttpDelete]
        [Route("api/storetimetable")]
        public void DeleteStoreTimeTable(int id)
        { //delete a StoreTimeTable

        }

        // ///////////////7
        //Minimart Product
        // ////////////////

        [HttpGet]
        [Route("api/minimart/product/list")]
        public IEnumerable<MinimartProduct> GetProducts(int minimartId = 0, string minimartName = "", int productId = 0, string productName = "", int categoryId = 0, string categoryName = "", Boolean lowStock = false) 
        {   //list all products available in the minimart (lowstock = false), or products with low stock (lowstock = true)

            return minimartProducts.list(minimartId, minimartName, productId, productName, categoryId, categoryName, lowStock);
        }


        [HttpPost]
        [Route("api/minimart/product")]
        public int PostProduct([FromBody] MinimartProduct newMinimartProduct)
        {   //add a new product and its stock in a minimart

            int id = 0;
            //insert into Minimart_Product Table
            return id;
        }

        [HttpPut]
        [Route("api/minimart/product")]
        public void PutProduct([FromBody] MinimartProduct aMinimartProduct)
        {   //update a product and its stock in a minimart

        }


        [HttpDelete]
        [Route("api/minimart/product")]
        public void DeleteProduct(int id)
        { //delete a product and its stock from a minimart

        }



        /*
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
        */
    }
}
