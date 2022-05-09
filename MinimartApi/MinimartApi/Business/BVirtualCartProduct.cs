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
    public class BVirtualCartProduct
    {
        /*
          Business Class Product.
        */
        private string CONNECTION_STRING = "";

        public BVirtualCartProduct()
        {
            //TODO: read from de config file
            CONNECTION_STRING = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = C:\\MCO\\FUENTES\\PRUEBAS\\VISUALSTUDIO\\MMAPI\\MINIMARTAPI\\DB\\MINIMARTAPI.MDF;Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public IEnumerable<VirtualCartProduct> listar(int minimartId, string minimartName, 
                                                      int costumerId, string customerFullName, 
                                                      int productId, string productName, int categoryId, string categoryName)
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                var parameters = new DynamicParameters();
                parameters.Add("minimart_id", minimartId);
                parameters.Add("minimart_name", minimartName);
                parameters.Add("customer_id", costumerId);
                parameters.Add("customer_fullName", customerFullName);
                parameters.Add("product_id", productId);
                parameters.Add("product_name", productName);
                parameters.Add("category_id", categoryId);
                parameters.Add("category_name", categoryName);

                var virtualCartProducts = connection.Query<VirtualCartProduct>("SP_VirtualCartProduct", param: parameters, commandType: CommandType.StoredProcedure);
                return virtualCartProducts;
            }

        }






    }

}