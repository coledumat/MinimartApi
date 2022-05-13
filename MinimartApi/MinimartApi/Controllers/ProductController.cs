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
    ///  CRUD methods of Categories and Products.
    /// </summary>
    //<RoutePrefix("api/data/MinimartApi")> 'mco
    public class ProductController : ApiController
    {
        //references to business classes
        private BProduct products;
        private BCategory categories;

        ProductController  ()
        {
            products = new BProduct();
            categories = new BCategory();
        }

        // ///////////
        //Category
        // ///////////

        [HttpGet]
        [Route("api/category/list")]
        public IEnumerable<Category> GetCategories(int productId = 0, string productName = "", int categoryId = 0, string categoryName = "") //mco
        {
            return categories.list(categoryId, categoryName);
        }

        [HttpPost]
        [Route("api/category")]
        public int PostCategory([FromBody] CategoryModel newCategory)
        {   //add a new category

            int id = 0;
            //insert into Minimart_Product Table
            return id;
        }

        [HttpPut]
        [Route("api/category/{Int Id}")]
        public void PutCategory(int Id, [FromBody] CategoryModel aCategory)
        {   //update a category 

        }


        [HttpDelete]
        [Route("api/category")]
        public void DeleteCategory(int id)
        { //delete a category

        }

        // /////////////
        //Type Product
        // ////////////

        [HttpGet]
        [Route("api/product/list")]
        public IEnumerable<Product> GetProducts( int productId = 0, string productName = "", int categoryId = 0, string categoryName ="") //mco
        {
            return products.list(categoryId, categoryName, productId, productName);
        }

        [HttpPost]
        [Route("api/product")]
        public int PostProduct([FromBody] ProductModel newProduct)
        {   //add a new type of product 

            int id = 0;
            //insert into Minimart_Product Table
            return id;
        }

        [HttpPut]
        [Route("api/product/{int Id}")]
        public void PutProduct(int Id, [FromBody] Product aProduct)
        {   //update a type of product 

        }

        [HttpDelete]
        [Route("api/product/{int Id}")]
        public void DeleteProduct(int id)
        { //delete a type of product

        }

    }
}
