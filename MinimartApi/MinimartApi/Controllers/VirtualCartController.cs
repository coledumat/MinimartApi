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
    public class VirtualCartController : ApiController
    {
        /*
        CRUD methods of Virtual Cart, 
        add/remove products discount minimart stock, 
        add/remove Voutchers discount for Categorie or Products and calculate discounts
       */

        private BVirtualCartProduct product;
        private BVirtualCartProductVoucher productVoucher;

        VirtualCartController()
        {
            product = new BVirtualCartProduct();
            productVoucher = new BVirtualCartProductVoucher();
        }

        [HttpGet]
        [Route("api/virtualcart/product/list")]
        public IEnumerable<VirtualCartProduct> GetProducts(int miniMartId = 0, string minimartName = "",
                                                           int customerId = 0, string customerFullName = "", 
                                                           int productId = 0, string productName = "", int categoryId = 0, string categoryName = "") 
        {
            return product.listar(miniMartId, minimartName, customerId, customerFullName, productId, productName, categoryId, categoryName);
        }



        [HttpGet]
        [Route("api/virtualcart/productvoucher/list")]
        public IEnumerable<VirtualCartProductVoucher> GetProducts(int miniMartId = 0, string minimartName = "",
                                                           int customerId = 0, string customerFullName = "",
                                                           int voucherId = 0, int voucherNum = 0,
                                                           int productId = 0, string productName = "", int categoryId = 0, string categoryName = "")
        {
            return productVoucher.listar(miniMartId, minimartName, customerId, customerFullName, voucherId, voucherNum, productId, productName, categoryId, categoryName);
        }





        // GET: api/VirtualCart
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/VirtualCart/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/VirtualCart
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/VirtualCart/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/VirtualCart/5
        public void Delete(int id)
        {
        }
    }
}
