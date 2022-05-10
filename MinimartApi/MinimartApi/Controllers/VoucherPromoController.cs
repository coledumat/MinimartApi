using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using MinimartApi.Models;

namespace MinimartApi.Controllers
{
    public class VoucherPromoController : ApiController
    {
        /*
        CRUD methods of VoucherPromo (CategoryVoucherPromo and ProductVucherPromo).
       */

        // ////////////////
        //product Voucher Promo
        // ////////////////


        [HttpPost]
        [Route("api/VoucherPromo/product")]
        public int PostProductVoucherPromo([FromBody] ProductVoucherPromo newProductVoucherPromo)
        {   //add a new type of ProductVoucherPromo 

            int id = 0;
            //insert into VoucherPromo and ProductVoucherPromo Tables
            return id;
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
