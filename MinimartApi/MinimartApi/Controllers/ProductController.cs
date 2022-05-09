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


    //<RoutePrefix("api/data/MinimartApi")> 'mco
    public class ProductController : ApiController
    {
        /*
         CRUD methods of Categories and Products.
        */

        //master
        //Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        //minimart
        //Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = C:\MCO\FUENTES\PRUEBAS\VISUALSTUDIO\MMAPI\MINIMARTAPI\DB\MINIMARTAPI.MDF;Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False

        //private string connection_string = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_name"].ToString(); //mco
        //private const string CONNECTION_STRING = "Server=.;Database=MinimartApi.mdf;Trusted_Connection=True;"; //mco

        //esta:
        //private const string CONNECTION_STRING = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = C:\\MCO\\FUENTES\\PRUEBAS\\VISUALSTUDIO\\MMAPI\\MINIMARTAPI\\DB\\MINIMARTAPI.MDF;Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";


        private BProduct product;

        ProductController  ()
        {
            product = new BProduct();
        
        }

        // GET: api/MiniMart
        /*
        <myAuthorize(Funcion:=openASW.Seguridad.Functions.ASW_Pago_Sin_Factura)>
        <HttpGet>
        <Route("consultadeuda/{Suministro:int}")>
       */
        [HttpGet]
        [Route("api/product/list")]
        public IEnumerable<Product> GetProducts( int productId = 0, string productName = "", int categoryId = 0, string categoryName ="") //mco
        {
            return product.listar(productId, productName, categoryId, categoryName);
        }



        // GET: api/MiniMart
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/MiniMart/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/MiniMart
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/MiniMart/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/MiniMart/5
        public void Delete(int id)
        {
        }
    }
}
