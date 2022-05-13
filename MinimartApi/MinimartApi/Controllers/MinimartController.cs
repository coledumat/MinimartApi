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
        [Route("api/minimart/{int Id}")]
        public void PutMinimart(int Id,[FromBody] Minimart aMinimart)
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
        public int PostStoreTimeTable([FromBody] StoreTimeTableModel newStoreTimeTable)
        {   //add a new StoreTimeTable

            int id = 0;
            //insert into Minimart_Product Table
            return id;
        }

        [HttpPut]
        [Route("api/storetimetable/{int Id}")]
        public void PutStoreTimeTable(int Id, [FromBody] StoreTimeTableModel aStoreTimeTable)
        {   //update a StoreTimeTable

        }


        [HttpDelete]
        [Route("api/storetimetable/{int Id}")]
        public void DeleteStoreTimeTable(int id)
        { //delete a StoreTimeTable

        }

        // ///////////////7
        //Minimart Product
        // ////////////////

        [HttpGet]
        [Route("api/minimart/product/list")]
        public IEnumerable<MinimartProduct> GetProducts(int minimartId = 0, string minimartName = "", int categoryId = 0, string categoryName = "", int productId = 0, string productName = "", Boolean lowStock = false) 
        {   //list all products available in the minimart (lowstock = false), or products with low stock (lowstock = true)

            return minimartProducts.list(minimartId, minimartName, categoryId, categoryName, productId, productName, lowStock);
        }

        /// <summary>
        /// add a list of new product and its stock in a minimart
        /// </summary>
        /// <param name="newMinimartProducts"></param>
        /// <returns></returns>
        [HttpPost ]
        [Route("api/minimart/products")]
        public IHttpActionResult PostProducts([FromBody] List<MinimartProductModel> newMinimartProducts)
        {
            //chek parameters
            if (newMinimartProducts == null)
            {
                return BadRequest("Initialize parameters");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            int result;
            foreach (var product in newMinimartProducts)
            { 
                result = minimartProducts.CreateMinimartProduct(product);
                if (result != 1)
                    return InternalServerError();
            }
            return Ok();
        }

        /// <summary>
        /// add a new product and its stock in a minimart
        /// </summary>
        /// <param name="newMinimartProduct"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/minimart/product")]
        public IHttpActionResult PostProduct([FromBody] MinimartProductModel newMinimartProduct)
        {
            //chek parameters
            if (newMinimartProduct == null)
            {
                return BadRequest("Initialize parameters");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            int result = minimartProducts.CreateMinimartProduct(newMinimartProduct);
            if (result == 1)
                return Ok();
            else
                return InternalServerError();
        }

        [HttpPut]
        [Route("api/minimart/product")]
        public void PutProduct([FromBody] MinimartProductModel aMinimartProduct)
        {   //update a product and its stock in a minimart

        }


        [HttpDelete]
        [Route("api/minimart/product")]
        public void DeleteProduct(int MinimartId, int ProductId)
        { //delete a product and its stock from a minimart

        }


    }
}
