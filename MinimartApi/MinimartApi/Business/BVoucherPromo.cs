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
    /// Business Class VoucherPromo.
    /// Represents a voucher book
    /// </summary>
    public class BVoucherPromo : BusinessClass
    {
        // /////////////////////
        // Voucher Promo
        // /////////////////////

        /// <summary>
        /// return all VoucherPromo headers
        /// </summary>
        /// <param name="newVirtualCartVoucherPromo"></param>
        /// <returns></returns>
        public IEnumerable<VoucherPromoModel> listVoucherPromo(VirtualCartVoucherPromoModel newVirtualCartVoucherPromo)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //chek number of Voucher in range and other conditions
                var parameters = new DynamicParameters();
                parameters.Add("minimart_id", newVirtualCartVoucherPromo.MinimartId);
                parameters.Add("voucher_id", newVirtualCartVoucherPromo.VoucherId);
                var VoucherPromo = connection.Query<VoucherPromoModel>("SP_VoucherPromo", param: parameters, commandType: CommandType.StoredProcedure);
                return VoucherPromo;
            }
        }


        // /////////////////////
        // B Product Voucher Promo
        // /////////////////////

        /// <summary>
        /// Create a type of product Voucher (ProductVoucherPromo) 
        /// </summary>
        /// <param name="newProductVoucherPromo"></param>
        /// <returns></returns>
        public int CreateProductVoucherPromo( ProductVoucherPromoModel newProductVoucherPromo) 
        {
            //insert into VoucherPromo and ProductVoucherPromo Tables
            string sql = "INSERT INTO VoucherPromo ( Id_Minimart, VIniNumber, VEndNumber, Name, Description, " +
                                                     "StartDate, EndDate, WeekDays, StartingWithXUnits, UnitOnDiscount, PercentageDiscount) " +
                                           "VALUES ( @Id_Minimart, @VIniNumber, @VEndNumber, @Name, @Description, " +
                                                     "@StartDate, @EndDate, @WeekDays, @StartingWithXUnits, @UnitOnDiscount, @PercentageDiscount);";
            int affectedRows = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                affectedRows =  connection.Execute(sql, new { 
                                                            Id_Minimart = newProductVoucherPromo.MinimartId,
                                                            VIniNumber = newProductVoucherPromo.VIniNumber,
                                                            VEndNumber = newProductVoucherPromo.VEndNumber,
                                                            Name = newProductVoucherPromo.Name,
                                                            Description = newProductVoucherPromo.Description,
                                                            StartDate= newProductVoucherPromo.StartDate,
                                                            EndDate = newProductVoucherPromo.EndDate ,
                                                            WeekDays = string.Join(",",newProductVoucherPromo.WeekDays),
                                                            StartingWithXUnits= newProductVoucherPromo.StartingWithXUnits,
                                                            UnitOnDiscount = newProductVoucherPromo.UnitOnDiscount ,
                                                            PercentageDiscount = newProductVoucherPromo.PercentageDiscount 
                                                            });
                //read identity id_Voucher
                sql = "SELECT MAX(Id) FROM VoucherPromo WHERE Id_Minimart = @Id_Minimart;";
                var new_id_Voucher = connection.QuerySingle<int>(sql, new { Id_Minimart = newProductVoucherPromo.MinimartId});  //connection.Query<Customer>("Select Max(id) FROM VoucherPromo") //.ToList();

                if (affectedRows == 1 && new_id_Voucher > 0)
                {//insert included products
                    foreach (var product in newProductVoucherPromo.includeProducts)
                    {
                        sql = "INSERT INTO ProductVoucherPromo( Id_Voucher, Id_Product) " +
                                                      "VALUES ( @Id_Voucher, @Id_Product);";
                        affectedRows = connection.Execute(sql, new {
                                                                    Id_Voucher = new_id_Voucher,
                                                                    Id_Product = product.ProductId
                                                                    });
                    }
                }
            }

           return affectedRows;
        }

        /// <summary>
        /// List the products included in the Vouchers of a minimart
        /// </summary>
        /// <param name="minimartId">0 for all</param>
        /// <param name="minimartName">full o partial name</param>
        /// <param name="voucherId">id of type of product voucher. 0 for all</param>
        /// <param name="categoryId">0 for all</param>
        /// <param name="categoryName">full o partial name</param>
        /// <param name="productId">0 for all</param>
        /// <param name="productName">full o partial name</param>
        /// <returns></returns>
        public IEnumerable<ProductVoucherPromo> listProductVoucherPromos(int minimartId, string minimartName, 
                                                                         int voucherId,
                                                                         int categoryId, string categoryName, int productId, string productName)
        {
            using (IDbConnection connection = new SqlConnection(connectionString ))
            {
                var parameters = new DynamicParameters();
                parameters.Add("minimart_id", minimartId);
                parameters.Add("minimart_name", minimartName);
                parameters.Add("voucher_id", voucherId);
                parameters.Add("category_id", categoryId);
                parameters.Add("category_name", categoryName);
                parameters.Add("product_id", productId);
                parameters.Add("product_name", productName);

                //call sp SP_ProductVoucherPromo
                var virtualCartProductVouchers = connection.Query<ProductVoucherPromo>("SP_ProductVoucherPromo", param: parameters, commandType: CommandType.StoredProcedure);
                return virtualCartProductVouchers;
            }
        }

        /// <summary>
        /// Update a type of product Voucher (ProductVoucherPromo) 
        /// </summary>
        /// <param name="aProductVoucherPromo"></param>
        /// <returns></returns>
        public int UpdateProductVoucherPromo(int id, ProductVoucherPromoModel aProductVoucherPromo)
        {
            //insert into VoucherPromo and ProductVoucherPromo Tables
            string sql = "UPDATE VoucherPromo " +
                           " SET Id_Minimart = @id_Minimart, " +
                                "VIniNumber = @VIniNumber, " +
                                "VEndNumber = @VEndNumber, " +
                                "Name = @Name, " +
                                "Description = @Description, " +
                                "StartDate = @StartDate, " +
                                "EndDate = @EndDate, " +
                                "WeekDays = @WeekDays, " +
                                "StartingWithXUnits = @StartingWithXUnits, " +
                                "UnitOnDiscount = @UnitOnDiscount, " +
                                "PercentageDiscount = @PercentageDiscount) " +
                          "WHERE Id =@Id;";
            int affectedRows = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                affectedRows = connection.Execute(sql, new  { 
                                                            Id = id,
                                                            Id_Minimart = aProductVoucherPromo.MinimartId,
                                                            VIniNumber = aProductVoucherPromo.VIniNumber,
                                                            VEndNumber = aProductVoucherPromo.VEndNumber,
                                                            Name = aProductVoucherPromo.Name,
                                                            Description = aProductVoucherPromo.Description,
                                                            StartDate = aProductVoucherPromo.StartDate,
                                                            EndDate = aProductVoucherPromo.EndDate,
                                                            WeekDays = string.Join(",", aProductVoucherPromo.WeekDays),
                                                            StartingWithXUnits = aProductVoucherPromo.StartingWithXUnits,
                                                            UnitOnDiscount = aProductVoucherPromo.UnitOnDiscount,
                                                            PercentageDiscount = aProductVoucherPromo.PercentageDiscount
                                                            });
 
                if (affectedRows == 1 )
                {
                    //delete from VoucherPromo 
                    sql = "DELETE VoucherPromo " +
                           "WHERE id_Voucher = @id_Voucher;";
                    affectedRows = connection.Execute(sql, new {
                                                                id_Voucher = id
                                                                });
                    //insert included products
                    foreach (var product in aProductVoucherPromo.includeProducts)
                    {
                        sql = "INSERT INTO ProductVoucherPromo( id_Voucher, Id_Product) " +
                                                      "VALUES ( @id_Voucher, @Id_Product);";
                        affectedRows = connection.Execute(sql, new {
                                                                    id_Voucher = id,
                                                                    id_Product = product.ProductId
                                                                    });
                    }
                }
            }

            return affectedRows;
        }


        /// <summary>
        /// Delete a type of product Voucher (ProductVoucherPromo) 
        /// </summary>
        /// <param name="newProductVoucherPromo"></param>
        /// <returns></returns>
        public int DeleteProductVoucherPromo(int id)
        {   //delete included products
            string sql = " DELETE ProductVoucherPromo " +
                         "  WHERE id_Voucher = @id_Voucher;";
            int affectedRows = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                affectedRows = connection.Execute(sql, new  {
                                                            id_Voucher = id
                                                            });
            }

            //delete from VoucherPromo 
            sql = "DELETE VoucherPromo " +
                   "WHERE id_Voucher = @id_Voucher;";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                affectedRows = connection.Execute(sql, new {
                                                            //id_Minimart = id_Minimart,
                                                            id_Voucher = id
                                                            });
            }
            return affectedRows;
        }

        // ////////////////////////
        // B Category Voucher Promo
        // ////////////////////////

    }

}