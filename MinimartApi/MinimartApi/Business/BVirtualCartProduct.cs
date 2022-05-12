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
    /// <summary>
    /// Business Class VirtualCartProduct.
    /// Represents a product in the virtual cart
    /// </summary>
    public class BVirtualCartProduct : BusinessClass
    {

        /// <summary>
        /// List the products in the virtual cart of a minimart
        /// </summary>
        /// <param name="minimartId">0 for all</param>
        /// <param name="minimartName">full o partial name</param>
        /// <param name="customerId">0 for all</param>
        /// <param name="customerFullName">full o partial name</param>
        /// <param name="categoryId">0 for all</param>
        /// <param name="categoryName">full o partial name</param>
        /// <param name="productId">0 for all</param>
        /// <param name="productName">full o partial name</param>
        /// <returns>VirtualCartProduct</returns>
        public IEnumerable<VirtualCartProduct> list(int minimartId, string minimartName,
                                                    int customerId, string customerFullName,
                                                    int categoryId, string categoryName, int productId, string productName)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("minimart_id", minimartId);
                parameters.Add("minimart_name", minimartName);
                parameters.Add("customer_id", customerId);
                parameters.Add("customer_fullName", customerFullName);
                parameters.Add("category_id", categoryId);
                parameters.Add("category_name", categoryName);
                parameters.Add("product_id", productId);
                parameters.Add("product_name", productName);

                var virtualCartProducts = connection.Query<VirtualCartProduct>("SP_VirtualCartProduct", param: parameters, commandType: CommandType.StoredProcedure);
                return virtualCartProducts;
            }

        }

        /// <summary>
        /// List the products in the virtual cart of a minimart
        /// </summary>
        /// <param name="minimartId">0 for all</param>
        /// <param name="minimartName">full o partial name</param>
        /// <param name="customerId">0 for all</param>
        /// <param name="customerFullName">full o partial name</param>
        /// <param name="categoryId">0 for all</param>
        /// <param name="categoryName">full o partial name</param>
        /// <param name="productId">0 for all</param>
        /// <param name="productName">full o partial name</param>
        /// <returns>VirtualCartProductWDiscount</returns>
        public IEnumerable<VirtualCartProductWDiscount> listWDiscount( int miniMartId, string minimartName,
                                                                      int customerId, string customerFullName,
                                                                      int categoryId, string categoryName, int productId, string productName)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("minimart_id", miniMartId);
                parameters.Add("minimart_name", minimartName);
                parameters.Add("customer_id", customerId);
                parameters.Add("customer_fullName", customerFullName);
                parameters.Add("category_id", categoryId);
                parameters.Add("category_name", categoryName);
                parameters.Add("product_id", productId);
                parameters.Add("product_name", productName);

                var virtualCartProducts = connection.Query<VirtualCartProductWDiscount>("SP_VirtualCartProduct", param: parameters, commandType: CommandType.StoredProcedure);
                return virtualCartProducts;
            }
        }

        /// <summary>
        /// add a product to the virtual cart
        /// and discount stock from the minimart
        /// </summary>
        /// <param name="newVirtualCartProduct"></param>
        /// <returns></returns>
        public int CreateVirtualCartProduct(VirtualCartProductModel newVirtualCartProduct)
        {
            int affectedRows = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //chek stock in the minimart
                string sql = "SELECT Stock FROM Minimart_Product " +
                                     "WHERE Id_Minimart = @Id_Minimart " +
                                       "AND Id_Product = @Id_Product;";
                var stock = connection.QuerySingle<int>(sql, new  {
                                                                Id_Minimart = newVirtualCartProduct.MinimartId,
                                                                Id_Product = newVirtualCartProduct.ProductId,
                                                                });
                if (newVirtualCartProduct.Units > stock)
                {
                    throw new Exception("Insufficient stock (" + stock.ToString() + ')');
                }

                //put product in the virtual cart (a trigger in VirtualCart_Product decrease stock in Minimart_Product)
                sql = "INSERT INTO VirtualCart_Product ( Id_Minimart, Id_Customer, Id_Product, Units) " +
                                                  "VALUES ( @id_Minimart, @Id_Customer, @Id_Product, @Units);";

                affectedRows = connection.Execute(sql, new
                {
                    Id_Minimart = newVirtualCartProduct.MinimartId,
                    Id_Customer = newVirtualCartProduct.CustomerId,
                    Id_Product = newVirtualCartProduct.ProductId,
                    Units = newVirtualCartProduct.Units
                });
             }

            return affectedRows;
        }

        /// <summary>
        /// Remove a product from virtual cart
        /// and increase the stock of the minimart
        /// </summary>
        /// <param name="MinimartId"></param>
        /// <param name="CustomerId"></param>
        /// <param name="ProductId"></param>
        /// <returns></returns>
        public int DeleteVirtualCartProduct(int MinimartId, int CustomerId, int ProductId)
        {   
            string sql = " DELETE VirtualCart_Product " +
                           "WHERE Id_Minimart = @Id_Minimart " +
                             "AND Id_Customer = @Id_Customer " +
                             "AND Id_Product = @Id_Product;";  //(a trigger in VirtualCart_Product increase stock in Minimart_Product)

            int affectedRows = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                affectedRows = connection.Execute(sql, new {
                                                            Id_Minimart = MinimartId,
                                                            Id_Customer = CustomerId,
                                                            Id_Product = ProductId
                                                            });
            }

            return affectedRows;
        }
    }
}