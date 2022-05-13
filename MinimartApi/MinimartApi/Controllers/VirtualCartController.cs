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
    /// Virtual Cart management
    /// 
    /// add/remove products to a virtual cart and update its stock in the minimart
    /// add/remove Voutchers discount(Category or Products) to a virtual cart
    /// calculate discounts by product and by category
    /// </summary>
    public class VirtualCartController : ApiController
    {

        //references to business classes
        private BVirtualCartProduct vcproduct;
        private BVirtualCartVoucherPromo vcVoucherPromo;
        private BVoucherPromo voucherPromo;

        VirtualCartController()
        {
            vcproduct = new BVirtualCartProduct();
            vcVoucherPromo = new BVirtualCartVoucherPromo();
            voucherPromo = new BVoucherPromo(); 
        }

        // //////////////////////////
        //virtualcart product
        // //////////////////////////

        /// <summary>
        /// lists the products in the virtual cart and calculates subtotal per product
        /// </summary>
        /// <param name="miniMartId">0 for all</param>
        /// <param name="minimartName">full o partial name</param>
        /// <param name="customerId">0 for all</param>
        /// <param name="customerFullName">full o partial name</param>
        /// <param name="categoryId">0 for all</param>
        /// <param name="categoryName">full o partial name</param>
        /// <param name="productId">0 for all</param>
        /// <param name="productName">full o partial name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/virtualcart/product/list")]
        public IEnumerable<VirtualCartProduct> GetProducts(int miniMartId = 0, string minimartName = "",
                                                           int customerId = 0, string customerFullName = "",
                                                           int categoryId = 0, string categoryName = "", int productId = 0, string productName = "" ) 
        { 
            return vcproduct.list(miniMartId, minimartName, customerId, customerFullName, categoryId, categoryName, productId, productName);
        }

        /// <summary>
        /// lists the products in the virtual cart and calculates subtotal per product and discounts
        /// </summary>
        /// <param name="miniMartId">0 for all</param>
        /// <param name="minimartName">full o partial name</param>
        /// <param name="customerId">0 for all</param>
        /// <param name="customerFullName">full o partial name</param>
        /// <param name="categoryId">0 for all</param>
        /// <param name="categoryName">full o partial name</param>
        /// <param name="productId">0 for all</param>
        /// <param name="productName">full o partial name</param>
        /// 
        /// <param name="voucherId">0 for all</param>
        /// <param name="voucherNum">0 for all</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/virtualcart/product/listWDiscount")]
        public IEnumerable<VirtualCartProductWDiscount> GetProductDiscount(int miniMartId = 0, string minimartName = "",
                                                                           int customerId = 0, string customerFullName = "",
                                                                           int productId = 0, string productName = "", int categoryId = 0, string categoryName = "",
                                                                           int voucherId = 0, int voucherNum = 0
                                                                           )
        { 

            //search list of VirtualCartProduct
            IEnumerable<VirtualCartProductWDiscount> virtualCartProductWDiscount = // vcproduct.list(miniMartId, minimartName, customerId, customerFullName, productId, productName, categoryId, categoryName);
                                                                                   vcproduct.listWDiscount(miniMartId, minimartName, customerId, customerFullName, productId, productName, categoryId, categoryName);
            /// product discount voucher
            /// /////////////////////////
            
            //serch list of VirtualCartProductVouchers
            IEnumerable <VirtualCartProductVoucher> virtualCartProductVoucher = vcVoucherPromo.listProductVoucher(miniMartId, minimartName, customerId, customerFullName, voucherId, voucherNum, categoryId, categoryName, productId, productName);

            //for each VirtualCartProduct calculate the discount //cumulative discounts
            foreach ( var productWDiscount in virtualCartProductWDiscount )
            {
                //filter if there is the product in some Voucher, and validate conditions
                var virtualCartProductVoucher_aux =  from v in virtualCartProductVoucher
                                                     where v.MinimartId == productWDiscount.MinimartId 
                                                         & v.CustomerId == productWDiscount.CustomerId 
                                                         & v.ProductId == productWDiscount.ProductId 
                                                         & DateTime.Now >= v.StartDate & DateTime.Now <= v.EndDate 
                                                         & v.WeekDays.Contains(DateTime.Now.DayOfWeek.ToString())  
                                                     select v;
                float discount = 0;
                int discount_voucherId = 0;
                int discunt_voucherNUM = 0;
                foreach (var productVoucher in virtualCartProductVoucher_aux)
                {//for each voucher apply its discount
                    if (productVoucher.StartingWithXUnits <= productWDiscount.Units)
                    {
                        discount += ((productWDiscount.Price * productVoucher.PercentageDiscount / 100) * (productWDiscount.Units / productVoucher.StartingWithXUnits));
                        //discount_voucherId = productVoucher.VoucherId; //can be more of one
                        //discunt_voucherNUM = productVoucher.NumVoucher;
                        productWDiscount.Voucher_Id_Num += "(" + discount_voucherId.ToString() + "," + discunt_voucherNUM.ToString() + ");";
                    }
                }
                productWDiscount.SubtotalWithDiscount = productWDiscount.SubtotalProduct - discount;

            }

            /// category discount voucher
            /// /////////////////////////

            //serch list of VirtualCartProductVouchers
            IEnumerable<VirtualCartCategoryVoucher> virtualCartCategoryVoucher = vcVoucherPromo.listCategoryVoucher(miniMartId, minimartName, customerId, customerFullName, voucherId, voucherNum, categoryId, categoryName); //, productId, productName);
            
            //for each VirtualCartProduct calculate the discount //cumulative discounts
            foreach (var productWDiscount in virtualCartProductWDiscount)
            {
                //get the categoryVoucherPomo that apply to de product
                var virtualCartCategotyVoucher_aux = from v in virtualCartCategoryVoucher
                                                    where v.MinimartId == productWDiscount.MinimartId
                                                        & v.CustomerId == productWDiscount.CustomerId
                                                        & v.CategoryId == productWDiscount.CategoryId 
                                                        & DateTime.Now >= v.StartDate & DateTime.Now <= v.EndDate
                                                        & v.WeekDays.Contains(DateTime.Now.DayOfWeek.ToString())
                                                    select v;

                //serch excluded products
               var excluded = voucherPromo.listCategoryVoucherPromo_ExcProduct(voucherId, categoryId, categoryName,productWDiscount.ProductId);

               if (excluded.Count() == 0)
                {//if not exclude
                    float discount = 0;
                    int discount_voucherId = 0;
                    int discunt_voucherNUM = 0;
                    foreach (var categoryVoucher in virtualCartCategotyVoucher_aux)
                    {//for each voucher apply its discount
                        if (categoryVoucher.StartingWithXUnits <= productWDiscount.Units)
                        {
                            discount += ((productWDiscount.Price * categoryVoucher.PercentageDiscount / 100) * (productWDiscount.Units / categoryVoucher.StartingWithXUnits));
                            productWDiscount.Voucher_Id_Num += "(" + discount_voucherId.ToString() + "," + discunt_voucherNUM.ToString() + ");";
                        }
                    }
                    productWDiscount.SubtotalWithDiscount = productWDiscount.SubtotalWithDiscount - discount;
                }

            }
            return virtualCartProductWDiscount;
        }

        /// <summary>
        /// add a new product in the virtualcart, and discount units from minimart stock
        /// </summary>
        /// <param name="newProduct"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/virtualcart/product")]
        public IHttpActionResult PostProduct([FromBody] VirtualCartProductModel newvirtualCartProduct)
        {
            //chek parameters
            if (newvirtualCartProduct == null)
            {
                return BadRequest("Initialize parameters");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            int result = vcproduct.CreateVirtualCartProduct(newvirtualCartProduct);
            if (result == 1)
                return Ok();
            else
                return InternalServerError();
        }

        /// <summary>
        /// update a product in the virtualcart
        /// </summary>
        /// <param name="aProduct"></param>
        [HttpPut]
        [Route("api/virtualcart/product/{int id}")]
        public IHttpActionResult PutProduct(int id,[FromBody] VirtualCartProductModel avirtualCartProduct)
        {
            return InternalServerError(new Exception("method not implemented"));
            //TODO: trigger for update in VirtualCart_Product
        }

        /// <summary>
        /// delete a product from the virtualcart
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        [Route("api/virtualcart/product")]
        public IHttpActionResult DeleteProduct(int MinimartId, int CustomerId, int ProductId)
        {
            //chek parameters
            if (MinimartId == 0 || CustomerId == 0 || ProductId == 0)
            {
                return BadRequest("Initialize parameters");
            }
            int result = vcproduct.DeleteVirtualCartProduct(MinimartId, CustomerId, ProductId);
            if (result == 1)
                return Ok();
            else
                return InternalServerError();
        }


        // /////////////////////////
        //virtualcart VoucherPromo
        // /////////////////////////

        /// <summary>
        /// add a new VoucherPromo in the virtualcart
        /// </summary>
        /// <param name="newProductVouvher"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/virtualcart/voucherPromo")]
        public IHttpActionResult PostProductVoucher([FromBody] VirtualCartVoucherPromoModel newVirtualCartVoucherPromo)
        {

            //chek parameters
            if (newVirtualCartVoucherPromo == null)
            {
                return BadRequest("Initialize parameters");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }

            int result = vcVoucherPromo.CreateVirtualCartVoucherPromo(newVirtualCartVoucherPromo);  //veify if the numVoucher is in the valid range
            if (result == 1)
                return Ok();
            else
                return InternalServerError();
        }

        /// <summary>
        /// update a aVoucherPromo in the virtualcart
        /// </summary>
        /// <param name="aVirtualCartVoucherPromo"></param>
        [HttpPut]
        [Route("api/virtualcart/voucherPromo")]
        public IHttpActionResult PutVoucherProduct([FromBody] VirtualCartVoucherPromoModel aVirtualCartVoucherPromo)
        {
            return InternalServerError(new Exception("method not implemented"));
        }

        /// <summary>
        /// delete a voucherPromo from the virtualcart
        /// </summary>
        /// <param name="VoucherNum"></param>
        [HttpDelete]
        [Route("api/virtualcart/voucherPromo")]
        public IHttpActionResult DeleteVoucherPromo([FromBody] VirtualCartVoucherPromoModel aVirtualCartVoucherPromo)
        {
            //chek parameters
            if (aVirtualCartVoucherPromo == null)
            {
                return BadRequest("Initialize parameters");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(modelState: ModelState);
            }
            int result = vcVoucherPromo.DeleteVirtualCartVoucherPromo(aVirtualCartVoucherPromo);
            if (result == 1)
                return Ok();
            else
                return InternalServerError();
        }

        // /////////////////////////
        //virtualcart productVoucher
        // /////////////////////////

        /// <summary>
        /// lists the productVouchers in the virtual cart 
        /// </summary>
        /// <param name="miniMartId">0 for all</param>
        /// <param name="minimartName">full o partial name</param>
        /// <param name="customerId">0 for all</param>
        /// <param name="customerFullName">full o partial name</param>
        /// <param name="voucherId">0 for all</param>
        /// <param name="voucherNum">0 for all</param>
        /// <param name="categoryId">0 for all</param>
        /// <param name="categoryName">full o partial name</param>
        /// <param name="productId">0 for all</param>
        /// <param name="productName">full o partial name</param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/virtualcart/productvoucher/list")]
        public IEnumerable<VirtualCartProductVoucher> GetProductVouchers(int miniMartId = 0, string minimartName = "",
                                                                          int customerId = 0, string customerFullName = "",
                                                                          int voucherId = 0, int voucherNum = 0,
                                                                          int categoryId = 0, string categoryName = "", int productId = 0, string productName = "")
        { //return products in a vituarcartProductVoucher
            return vcVoucherPromo.listProductVoucher (miniMartId, minimartName, customerId, customerFullName, voucherId, voucherNum, categoryId, categoryName, productId, productName);
        }

 
        // /////////////////////////
        //virtualcart CategoryVoucher
        // /////////////////////////

        //TODO: 
        //[Route("api/virtualcart/categoryvoucher/list")]
        //public IEnumerable<VirtualCartCategoryVoucher> GetCategoryVouchers(
    }
}
