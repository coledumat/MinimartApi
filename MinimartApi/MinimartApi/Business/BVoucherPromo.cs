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
        /// List the products included in the Vouchers (of type productVoucher) 
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
            //update into VoucherPromo and ProductVoucherPromo Tables
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
                    //delete old include products 
                    sql = "DELETE ProductVoucherPromo " +
                           "WHERE Id_Voucher = @Id_Voucher;";
                    affectedRows = connection.Execute(sql, new {
                                                                id_Voucher = id
                                                                });
                    //insert included products  
                    foreach (var product in aProductVoucherPromo.includeProducts)
                    {
                        sql = "INSERT INTO ProductVoucherPromo( Id_Voucher, Id_Product) " +
                                                      "VALUES ( @Id_Voucher, @Id_Product);";
                        affectedRows = connection.Execute(sql, new {
                                                                    Id_Voucher = id,
                                                                    Id_Product = product.ProductId
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

        /// <summary>
        /// List the products exclude in the Vouchers (of type categoryVoucher) 
        /// </summary>
        /// <param name="minimartId"></param>
        /// <param name="minimartName"></param>
        /// <param name="voucherId"></param>
        /// <param name="categoryId"></param>
        /// <param name="categoryName"></param>
        /// <param name="excludeProductId"></param>
        /// <param name="excludeProductName"></param>
        /// <returns></returns>
        public IEnumerable<ProductVoucherPromo> listCategoryVoucherPromos(int minimartId, string minimartName,
                                                                         int voucherId,
                                                                         int categoryId, string categoryName, int excludeProductId, string excludeProductName)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("minimart_id", minimartId);
                parameters.Add("minimart_name", minimartName);
                parameters.Add("voucher_id", voucherId);
                parameters.Add("category_id", categoryId);
                parameters.Add("category_name", categoryName);
                parameters.Add("exclude_product_id", excludeProductId);
                parameters.Add("exclude_product_name", excludeProductName);

                //call sp SP_ProductVoucherPromo
                var virtualCartProductVouchers = connection.Query<ProductVoucherPromo>("SP_CategoryVoucherPromo", param: parameters, commandType: CommandType.StoredProcedure);
                return virtualCartProductVouchers;
            }
        }


        /// <summary>
        /// return excluded products from a categoryVoucherPromo
        /// </summary>
        /// <param name="voucherId"></param>
        /// <param name="categoryId"></param>
        /// <param name="categoryName"></param>
        /// <param name="excludeProductId"></param>
        /// <param name="excludProductName"></param>
        /// <returns></returns>
        public IEnumerable<CategoryVoucherPromo_ExcProduct> listCategoryVoucherPromo_ExcProduct(int voucherId = 0, 
                                                                                                  int categoryId = 0, string categoryName = "", int excludeProductId = 0, string excludProductName = "")
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("voucher_id", voucherId);
                parameters.Add("category_id", categoryId);     // _category of the voucher_
                parameters.Add("category_name", categoryName);
                parameters.Add("exclude_product_id", excludeProductId); //TODO: excluded product have de same category of the voucher  _category of the voucher_
                parameters.Add("exclude_product_name", excludProductName);

                var categoryVoucherPromo_ExcProducts = connection.Query<CategoryVoucherPromo_ExcProduct>("SP_CategoryVoucherPromo_ExcProduct", param: parameters, commandType: CommandType.StoredProcedure);
                return categoryVoucherPromo_ExcProducts;
            }
        }




        /// <summary>
        /// Create a type of category Voucher (CategoryVoucherPromo) 
        /// </summary>
        /// <param name="newCategoryVoucherPromo"></param>
        /// <returns></returns>
        public int CreateCategoryVoucherPromo(CategoryVoucherPromoModel newCategoryVoucherPromo)
        {
            //insert into VoucherPromo and CategoryVoucherPromo and CategoryVoucherPromo_ExcProduct Tables
            string sql = "INSERT INTO VoucherPromo ( Id_Minimart, VIniNumber, VEndNumber, Name, Description, " +
                                                     "StartDate, EndDate, WeekDays, StartingWithXUnits, UnitOnDiscount, PercentageDiscount) " +
                                           "VALUES ( @Id_Minimart, @VIniNumber, @VEndNumber, @Name, @Description, " +
                                                     "@StartDate, @EndDate, @WeekDays, @StartingWithXUnits, @UnitOnDiscount, @PercentageDiscount);";
            int affectedRows = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                affectedRows = connection.Execute(sql, new
                {
                    Id_Minimart = newCategoryVoucherPromo.MinimartId,
                    VIniNumber = newCategoryVoucherPromo.VIniNumber,
                    VEndNumber = newCategoryVoucherPromo.VEndNumber,
                    Name = newCategoryVoucherPromo.Name,
                    Description = newCategoryVoucherPromo.Description,
                    StartDate = newCategoryVoucherPromo.StartDate,
                    EndDate = newCategoryVoucherPromo.EndDate,
                    WeekDays = string.Join(",", newCategoryVoucherPromo.WeekDays),
                    StartingWithXUnits = newCategoryVoucherPromo.StartingWithXUnits,
                    UnitOnDiscount = newCategoryVoucherPromo.UnitOnDiscount,
                    PercentageDiscount = newCategoryVoucherPromo.PercentageDiscount
                });

                //read identity id_Voucher
                sql = "SELECT MAX(Id) FROM VoucherPromo WHERE Id_Minimart = @Id_Minimart;";
                var new_id_Voucher = connection.QuerySingle<int>(sql, new { Id_Minimart = newCategoryVoucherPromo.MinimartId });  //connection.Query<Customer>("Select Max(id) FROM VoucherPromo") //.ToList();

                if (affectedRows == 1 && new_id_Voucher > 0)
                {   //insert category
                    sql = "INSERT INTO CategoryVoucherPromo( Id_Voucher, Id_Category) " +
                                                  "VALUES ( @Id_Voucher, @Id_Category);";
                    affectedRows = connection.Execute(sql, new{
                                                                Id_Voucher = new_id_Voucher,
                                                                Id_Category = newCategoryVoucherPromo.CategoryId,
                                                            });
                    //insert exclude products
                    foreach (var product in newCategoryVoucherPromo.excludeProducts )
                    {
                        sql = "INSERT INTO CategoryVoucherPromo_ExcProduct( Id_Voucher, Id_Category, Id_Product) " +
                                                      "VALUES ( @Id_Voucher,@Id_Category, @Id_Product);";
                        affectedRows = connection.Execute(sql, new
                        {
                            Id_Voucher = new_id_Voucher,
                            Id_Category = product.CategoryId,
                            Id_Product = product.ProductId
                        });
                    }
                }
            }

            return affectedRows;
        }

        /// <summary>
        /// Update a type of category Voucher (CategoryVoucherPromo)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="aCategoryVoucherPromo"></param>
        /// <returns></returns>
        public int UpdateCategoryVoucherPromo(int id, CategoryVoucherPromoModel aCategoryVoucherPromo)
        { //update VoucherPromo and CategoryVoucherPromo and CategoryVoucherPromo_ExcProduct

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
                affectedRows = connection.Execute(sql, new
                {
                    Id = id,
                    Id_Minimart = aCategoryVoucherPromo.MinimartId,
                    VIniNumber = aCategoryVoucherPromo.VIniNumber,
                    VEndNumber = aCategoryVoucherPromo.VEndNumber,
                    Name = aCategoryVoucherPromo.Name,
                    Description = aCategoryVoucherPromo.Description,
                    StartDate = aCategoryVoucherPromo.StartDate,
                    EndDate = aCategoryVoucherPromo.EndDate,
                    WeekDays = string.Join(",", aCategoryVoucherPromo.WeekDays),
                    StartingWithXUnits = aCategoryVoucherPromo.StartingWithXUnits,
                    UnitOnDiscount = aCategoryVoucherPromo.UnitOnDiscount,
                    PercentageDiscount = aCategoryVoucherPromo.PercentageDiscount
                });

                //update category
                sql = "Update CategoryVoucherPromo " +
                         "SET Id_Category = @Id_Category " +
                       "WHERE Id_Voucher = @Id_Voucher;";
                affectedRows = connection.Execute(sql, new  {
                                                            Id_Voucher = id,
                                                            Id_Category = aCategoryVoucherPromo.CategoryId 
                                                            });
                //update exclude products //TODO: Chek the category of the new exclude products
                if (affectedRows == 1)
                {
                    //delete exclude products 
                    sql = "DELETE CategoryVoucherPromo_ExcProduct " +
                           "WHERE Id_Voucher = @Id_Voucher;";
                    affectedRows = connection.Execute(sql, new
                    {
                        id_Voucher = id
                    });
                    //insert exclude products
                    foreach (var product in aCategoryVoucherPromo.excludeProducts)
                    {
                        sql = "INSERT INTO CategoryVoucherPromo_Exc ( Id_Voucher, Id_Category, Id_Product) " +
                                                      "VALUES ( @Id_Voucher, @Id_Category, @Id_Product);";
                        affectedRows = connection.Execute(sql, new
                        {
                            Id_Voucher = id,
                            Id_Category = aCategoryVoucherPromo.CategoryId,
                            Id_Product = product.ProductId
                        });
                    }
                }
            }

            return affectedRows;
        }


        /// <summary>
        ///  Delete a type of category Voucher (CategoryVoucherPromo) 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCategoryVoucherPromo(int id)
        {   //delete exclude products
            string sql = " DELETE CatagoryVoucherPromo_ExcProduct " +
                         "  WHERE id_Voucher = @id_Voucher;";
            int affectedRows = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                affectedRows = connection.Execute(sql, new {
                    id_Voucher = id
                });
            }
            //delete the category
            sql = " DELETE CatagoryVoucherPromo " +
             "  WHERE id_Voucher = @id_Voucher;";
            affectedRows = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                affectedRows = connection.Execute(sql, new {
                    id_Voucher = id
                });
            }
            //delete from VoucherPromo 
            sql = "DELETE VoucherPromo " +
                   "WHERE id_Voucher = @id_Voucher;";
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                affectedRows = connection.Execute(sql, new
                {
                    //id_Minimart = id_Minimart,
                    id_Voucher = id
                });
            }
            return affectedRows;
        }

    }

}