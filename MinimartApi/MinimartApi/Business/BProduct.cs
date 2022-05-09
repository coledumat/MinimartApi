using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

//mco [
using Dapper;
using System.Data;
using System.Data.SqlClient;

using MinimartApi.Models;
// mco ]

namespace MinimartApi.Business
{
    public class BProduct
    {
        /*
          Business Class Product Type.
        */

        //master
        //Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False
        //minimart
        //Data Source = (localdb)\MSSQLLocalDB;Initial Catalog = C:\MCO\FUENTES\PRUEBAS\VISUALSTUDIO\MMAPI\MINIMARTAPI\DB\MINIMARTAPI.MDF;Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False

        //private string connection_string = System.Configuration.ConfigurationManager.ConnectionStrings["Connection_name"].ToString(); //mco
        //private const string CONNECTION_STRING = "Server=.;Database=MinimartApi.mdf;Trusted_Connection=True;"; //mco

        //esta:
        //private const string CONNECTION_STRING = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = C:\\MCO\\FUENTES\\PRUEBAS\\VISUALSTUDIO\\MMAPI\\MINIMARTAPI\\DB\\MINIMARTAPI.MDF;Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        private string CONNECTION_STRING = "";

        public BProduct()
        {
            //TODO: read from de config file
            CONNECTION_STRING = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = C:\\MCO\\FUENTES\\PRUEBAS\\VISUALSTUDIO\\MMAPI\\MINIMARTAPI\\DB\\MINIMARTAPI.MDF;Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public IEnumerable<Product> listar(int productId, string productName, int categoryId, string categoryName)
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                var parameters = new DynamicParameters();
                parameters.Add("product_id", productId);
                parameters.Add("product_name", productName);
                parameters.Add("category_id", categoryId);
                parameters.Add("category_name", categoryName);

                var products = connection.Query<Product>("SP_Product", param: parameters, commandType: CommandType.StoredProcedure);
                return products;
            }

        }






    }

}