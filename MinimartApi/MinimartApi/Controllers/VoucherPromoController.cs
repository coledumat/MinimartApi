using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using MinimartApi.Models;
using MinimartApi.Business;


namespace MinimartApi.Controllers
{
    public class VoucherPromoController : ApiController
    {
        /*
        CRUD methods of VoucherPromo (CategoryVoucherPromo and ProductVucherPromo).
       */

        private BVoucherPromo vouchersPromos;

        VoucherPromoController()
        {
            vouchersPromos = new BVoucherPromo();
        }



        // ////////////////
        //product Voucher Promo
        // ////////////////

        [HttpGet]
        [Route("api/VoucherPromo/product/list")]
        public IEnumerable<ProductVoucherPromo> GetProductVouchers(int miniMartId = 0, string minimartName = "",
                                                                         int voucherId = 0, 
                                                                         int categoryId = 0, string categoryName = "", int productId = 0, string productName = "")
        { //return products in a vituarcartProductVoucher
            return vouchersPromos.listProductVoucherPromos(miniMartId, minimartName, voucherId, categoryId, categoryName, productId, productName);
        }


        [HttpPost]
        [Route("api/VoucherPromo/product")]
        public IHttpActionResult PostProductVoucherPromo([FromBody] ProductVoucherPromoModel newProductVoucherPromo)
        {   //add a new type of ProductVoucherPromo 
            
            if ( newProductVoucherPromo == null ) {
                return BadRequest("Inicializar parametros");
            }

            if (! ModelState.IsValid) {
                return BadRequest(modelState:ModelState);
            }

            int result = vouchersPromos.CreateProductVoucherPromo( newProductVoucherPromo);

            if (result == 1)
                return Ok();
            else
                return InternalServerError(); 
        }

        [HttpPut]
        [Route("api/VoucherPromo/product")]
        public void PutProductVoucherPromo([FromBody] ProductVoucherPromo aProductVoucherPromo)
        {   //update a VoucherPromo of ProductVoucherPromo type

        }

        [HttpDelete]
        [Route("api/VoucherPromo/product")]
        public void DeleteProductVoucherPromo(int id)
        { //delete a VoucherPromo of ProductVoucherPromo type

        }

        // ////////////////
        //category Voucher Promo
        // ////////////////


        [HttpPost]
        [Route("api/VoucherPromo/category")]
        public int PostCategoryVoucherPromo([FromBody] CategoryVoucherPromo newCategoryVoucherPromo)
        {   //add a new type of CategoryVoucherPromo 

            int id = 0;
            //insert into VoucherPromo, CategoryVoucherPromo and CategoryVoucherPromo_ExcProduct Tables
            return id;
        }

        [HttpPut]
        [Route("api/VoucherPromo/category")]
        public void PutCategoryVoucherPromo([FromBody] CategoryVoucherPromo aCategoryVoucherPromo)
        {   //update a VoucherPromo of categoryVoucherPromo type

        }

        [HttpDelete]
        [Route("api/VoucherPromo/category")]
        public void DeleteCategoryVoucherPromo(int id)
        { //delete a VoucherPromo of categoryVoucherPromo type

        }

        /*
        // GET: api/Voucher
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Voucher/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Voucher
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Voucher/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Voucher/5
        public void Delete(int id)
        {
        }
        */
    }
}
