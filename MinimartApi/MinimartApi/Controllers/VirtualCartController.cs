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
        add/remove products to a virtualCart and update stock from minimart, 
        add/remove Voutchers discount (for Category or Products) to a virtualCart 
        and calculate discounts
       */

        private BVirtualCartProduct vcproduct;
        private BVirtualCartProductVoucher vcproductVoucher;

        VirtualCartController()
        {
            vcproduct = new BVirtualCartProduct();
            vcproductVoucher = new BVirtualCartProductVoucher();
        }

        // //////////////////////////
        //virtualcart product
        // ////////////////////////77

        [HttpGet]
        [Route("api/virtualcart/product/list")]
        public IEnumerable<VirtualCartProduct> GetProducts(int miniMartId = 0, string minimartName = "",
                                                           int customerId = 0, string customerFullName = "", 
                                                           int productId = 0, string productName = "", int categoryId = 0, string categoryName = "") 
        { //return products in the virtualchart and subtotal by product
            return vcproduct.list(miniMartId, minimartName, customerId, customerFullName, productId, productName, categoryId, categoryName);
        }

        [HttpGet]
        [Route("api/virtualcart/product/listWDiscount")]
        public IEnumerable<VirtualCartProductWDiscount> GetProductDiscount(int miniMartId = 0, string minimartName = "",
                                                          int customerId = 0, string customerFullName = "",
                                                          int productId = 0, string productName = "", int categoryId = 0, string categoryName = "",
                                                          int voucherId = 0, int voucherNum = 0
                                                          )
        { //return subtotal by product an subtotal by product with discount

            //search list of VirtualCartProduct
            IEnumerable<VirtualCartProductWDiscount> virtualCartProductWDiscount = vcproduct.listWDiscount(miniMartId, minimartName, customerId, customerFullName, productId, productName, categoryId, categoryName, voucherId, voucherNum);

            //serch list of VirtualCartProductVouchers
            IEnumerable<VirtualCartProductVoucher> virtualCartProductVoucher = vcproductVoucher.list(miniMartId, minimartName, customerId, customerFullName, voucherId, voucherNum, productId, productName, categoryId, categoryName);

            //for each VirtualCartProduct calculate the discount //cumulative discounts
            foreach ( var productWDiscount in virtualCartProductWDiscount )
            {
                //filter if there is the product in some Voucher
                var virtualCartProductVoucher_aux =  from a in virtualCartProductVoucher
                                                     where a.ProductId == productWDiscount.ProductId 
                                                     select a;
                float discount = 0;
                foreach (var productVoucher in virtualCartProductVoucher_aux)
                {//for each voucher apply its discount
                    if (productVoucher.StartingWhitXUnits <= productWDiscount.Units)
                    {
                        discount += ((productWDiscount.Price * productVoucher.PercentageDiscount / 100) * (productWDiscount.Units / productVoucher.StartingWhitXUnits));
                    }
                }
                productWDiscount.SubtotalWithDiscount = productWDiscount.SubtotalProduct - discount;
            }
            return virtualCartProductWDiscount;
        }

        [HttpPost]
        [Route("api/virtualcart/product")]
        public int PostProduct([FromBody] VirtualCartProduct newProduct)
        {   //add a new product in the virtualcart. Discount from minimart stock

            int id = 0;
            //insert into Minimart_Product Table
            return id;
        }

        [HttpPut]
        [Route("api/virtualcart/product")]
        public void PutProduct([FromBody] VirtualCartProduct aProduct)
        {   //update a product in the virtualcart

        }

        [HttpDelete]
        [Route("api/virtualcart/product")]
        public void DeleteProduct(int id)
        { //delete a product from the virtualcart

        }

        // /////////////////////////
        //virtualcart productVoucher
        // /////////////////////////


        [HttpGet]
        [Route("api/virtualcart/productvoucher/list")]
        public IEnumerable<VirtualCartProductVoucher> GetProducts(int miniMartId = 0, string minimartName = "",
                                                                  int customerId = 0, string customerFullName = "",
                                                                  int voucherId = 0, int voucherNum = 0,
                                                                  int categoryId = 0, string categoryName = "", int productId = 0, string productName = "")
        { //return products in a vituarcartProductVoucher
            return vcproductVoucher.list(miniMartId, minimartName, customerId, customerFullName, voucherId, voucherNum, categoryId, categoryName, productId, productName);
        }


        [HttpPost]
        [Route("api/virtualcart/productvoucher")]
        public int PostProductVoucher([FromBody] VirtualCartProduct newProductVouvher)
        {   //add a new productVoucher in the virtualcart

            int id = 0;
            //insert into Minimart_Product Table
            return id;
        }

        [HttpPut]
        [Route("api/virtualcart/productvoucher")]
        public void PutProductVoucher([FromBody] VirtualCartProductVoucher aProductVoucher)
        {   //update a productVoucher in the virtualcart

        }

        [HttpDelete]
        [Route("api/virtualcart/productvoucher")]
        public void DeleteProductVouvher(int id)
        { //delete a productVoucher from the virtualcart

        }


    }
}
