using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Dapper;
using System.Data;
using System.Data.SqlClient;

using MinimartApi.Models;

namespace MinimartApi.Business
{
    public class BProduct : BusinessClass
    {
        /*
          Business Class Product Type.
        */

        public IEnumerable<Product> list(int categoryId, string categoryName, int productId, string productName)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("category_id", categoryId);
                parameters.Add("category_name", categoryName);
                parameters.Add("product_id", productId);
                parameters.Add("product_name", productName);

                var products = connection.Query<Product>("SP_Product", param: parameters, commandType: CommandType.StoredProcedure);
                return products;
            }

        }






    }

}