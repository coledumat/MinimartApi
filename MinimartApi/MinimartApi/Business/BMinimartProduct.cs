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
    public class BMinimartProduct : BusinessClass
    {

        public IEnumerable<MinimartProduct> list(int minimartId, string minimartName, int categoryId, string categoryName, int productId, string productName,  Boolean lowStock)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("minimart_id", minimartId);
                parameters.Add("minimart_name", minimartName);
                parameters.Add("product_id", productId);
                parameters.Add("product_name", productName);
                parameters.Add("category_id", categoryId);
                parameters.Add("category_name", categoryName);
                parameters.Add("lowstock", lowStock);

                var miniMartProducts = connection.Query<MinimartProduct>("SP_MinimartProduct", param: parameters, commandType: CommandType.StoredProcedure);
                return miniMartProducts;
            }

        }






    }

}