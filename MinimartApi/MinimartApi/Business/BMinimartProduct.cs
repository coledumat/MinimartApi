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

        /// <summary>
        /// create stock of a product in a minimart
        /// </summary>
        /// <param name="newMinimartProduct"></param>
        /// <returns></returns>
        public int CreateMinimartProduct(MinimartProductModel newMinimartProduct)
        {
            int affectedRows = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                string sql = "INSERT INTO Minimart_Product ( Id_Minimart, Id_Product, Stock, MinimumStock) " +
                                                  "VALUES ( @id_Minimart, @Id_Product, @Stock,@MinimumStock);";

                affectedRows = connection.Execute(sql, new
                {
                    Id_Minimart = newMinimartProduct.MinimartId,
                    Id_Product = newMinimartProduct.ProductId,
                    Stock = newMinimartProduct.Stock,
                    MinimumStock = newMinimartProduct.MinimumStock
                });
            }

            return affectedRows;
        }


    }

}