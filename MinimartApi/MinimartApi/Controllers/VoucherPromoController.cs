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
    /// <summary>
    ///  promotional voucher management
    ///  VoucherPromo and its specializations: CategoryVucherPromo and ProductVucherPromo
    /// </summary>
    public class VoucherPromoController : ApiController
    {
        //Business Classes reference
        private BVoucherPromo vouchersPromos;

        VoucherPromoController()
        {
            vouchersPromos = new BVoucherPromo();
        }

        // /////////////////////
        // Product Voucher Promo
        // /////////////////////

        /// <summary>
        /// List the products included in the Vouchers of type product
        /// </summary>
        /// <param name="miniMartId">0 for all</param>
        /// <param name="minimartName">full o partial name</param>
        /// <param name="voucherId">id of type of product voucher. 0 for all</param>
        /// <param name="categoryId">0 for all</param>
        /// <param name="categoryName">full o partial name</param>
        /// <param name="productId">0 for all</param>
        /// <param name="productName">full o partial name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/VoucherPromo/product/list")]
        public IEnumerable<ProductVoucherPromo> GetProductVouchers(int miniMartId = 0, string minimartName = "",
                                                                   int voucherId = 0, 
                                                                   int categoryId = 0, string categoryName = "", int productId = 0, string productName = "")
        { 
            return vouchersPromos.listProductVoucherPromos(miniMartId, minimartName, voucherId, categoryId, categoryName, productId, productName);
        }

        /// <summary>
        /// Create a new type of ProductVoucherPromo 
        /// </summary>
        /// <param name="newProductVoucherPromo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/VoucherPromo/product")]
        public IHttpActionResult PostProductVoucherPromo([FromBody] ProductVoucherPromoModel newProductVoucherPromo)
        {   
            //chek parameters
            if ( newProductVoucherPromo == null ) {
                return BadRequest("Initialize parameters");
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

        /// <summary>
        /// update a VoucherPromo of ProductVoucherPromo type
        /// </summary>
        /// <param name="aProductVoucherPromo"></param>
        [HttpPut]
        [Route("api/VoucherPromo/product/{int id}")]
        public IHttpActionResult PutProductVoucherPromo(int id, [FromBody] ProductVoucherPromoModel aProductVoucherPromo)
        {
            //chek parameters
            if (aProductVoucherPromo == null)
            {
                return BadRequest("Initialize parameters");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            int result = vouchersPromos.UpdateProductVoucherPromo(id, aProductVoucherPromo);

            if (result == 1)
                return Ok();
            else
                return InternalServerError();
        }

        /// <summary>
        /// delete a VoucherPromo of ProductVoucherPromo type
        /// </summary>
        /// <param name="id_Minimart"></param>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("api/VoucherPromo/product/{id int}")]
        public IHttpActionResult DeleteProductVoucherPromo( int id)
        {
            //chek parameters
            if (id == 0)
            {
                return BadRequest("Initialize parameters");
            }

            int result = vouchersPromos.DeleteProductVoucherPromo(id);

            if (result == 1)
                return Ok();
            else
                return InternalServerError();
        }

        // ////////////////
        //category Voucher Promo
        // ////////////////

        /// <summary>
        /// List category and the products excluded in the Vouchers of type category
        /// </summary>
        /// <param name="miniMartId"></param>
        /// <param name="minimartName"></param>
        /// <param name="voucherId"></param>
        /// <param name="categoryId"></param>
        /// <param name="categoryName"></param>
        /// <param name="excludeProductId"></param>
        /// <param name="excludeProductName"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/VoucherPromo/category/list")]
        public IEnumerable<ProductVoucherPromo> GetCategoryVouchers(int miniMartId = 0, string minimartName = "",
                                                                int voucherId = 0,
                                                                int categoryId = 0, string categoryName = "", int excludeProductId = 0, string excludeProductName = "")
        {
            return vouchersPromos.listCategoryVoucherPromos(miniMartId, minimartName, voucherId, categoryId, categoryName, excludeProductId, excludeProductName);
        }

        /// <summary>
        /// Create a new type of CategoryVoucherPromo 
        /// </summary>
        /// <param name="newCategoryVoucherPromo"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/VoucherPromo/category")]
        public IHttpActionResult PostCategoryVoucherPromo([FromBody] CategoryVoucherPromoModel newCategoryVoucherPromo)
        {   /// insert into VoucherPromo, CategoryVoucherPromo and CategoryVoucherPromo_ExcProduct Tables

            //chek parameters
            if (newCategoryVoucherPromo == null)
            {
                return BadRequest("Initialize parameters");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            int result = vouchersPromos.CreateCategoryVoucherPromo(newCategoryVoucherPromo);

            if (result == 1)
                return Ok();
            else
                return InternalServerError();
        }

        /// <summary>
        /// update a VoucherPromo of ProductVoucherPromo type
        /// </summary>
        /// <param name="aCategoryVoucherPromo"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("api/VoucherPromo/category/{int id}")]
        public IHttpActionResult PutCategoryVoucherPromo(int id,[FromBody] CategoryVoucherPromoModel aCategoryVoucherPromo)
        {   //chek parameters
            if (aCategoryVoucherPromo == null)
            {
                return BadRequest("Initialize parameters");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            int result = vouchersPromos.UpdateCategoryVoucherPromo(id, aCategoryVoucherPromo);

            if (result == 1)
                return Ok();
            else
                return InternalServerError();
        }

        /// <summary>
        /// delete a VoucherPromo of categoryVoucherPromo type
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("api/VoucherPromo/category/{int id}")]
        public IHttpActionResult DeleteCategoryVoucherPromo(int id)
        { 
          //chek parameters
            if (id == 0)
            {
                return BadRequest("Initialize parameters");
            }

            int result = vouchersPromos.DeleteCategoryVoucherPromo(id);

            if (result == 1)
                return Ok();
            else
                return InternalServerError();
        }
    }

 }

