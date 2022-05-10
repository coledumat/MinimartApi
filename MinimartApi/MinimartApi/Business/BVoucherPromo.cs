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
    public class BVoucherPromo : BusinessClass
    {
        /*
          Business Class VoucherPromo.
        */

        public int CreateProductVoucherPromo( ProductVoucherPromoModel newProductVoucherPromo) 
        {
            //insert into VoucherPromo and ProductVoucherPromo Tables
            string sql = "INSERT INTO VoucherPromo (id_Minimart, VIniNumber, VEndNumber, Name, Description, StartDate, EndDate, WeekDays, StartingWithXUnits, UnitOnDiscount, PercentageDiscount)" +
                         " Values (@id_Minimart, @VIniNumber, @VEndNumber, @Name, @Description, @StartDate, @EndDate, @WeekDays, @StartingWithXUnits, @UnitOnDiscount, @PercentageDiscount);";

            int affectedRows = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                affectedRows =  connection.Execute(sql, new { 
                                                            id_Minimart = newProductVoucherPromo.MinimartId ,
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

                //Console.WriteLine(affectedRows);

                //read identity id_Voucher
                sql = "SELECT MAX(Id) FROM VoucherPromo;";
                var new_id_Voucher = connection.QuerySingle<int>(sql, new {});  //connection.Query<Customer>("Select Max(id) FROM VoucherPromo") //.ToList();

                if (affectedRows == 1 && new_id_Voucher > 0)
                {
                    foreach (var product in newProductVoucherPromo.includeProducts)
                    {

                        //insert include products
                        sql = "INSERT INTO ProductVoucherPromo(id_Voucher, Id_Product)" +
                              " Values ( @id_Voucher, @Id_Product);";
                        affectedRows = connection.Execute(sql, new
                        {
                            id_Voucher = new_id_Voucher,
                            id_Product = product.ProductId
                        });
                    }
                }
            }

           return affectedRows;

        }


        
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

                var virtualCartProductVouchers = connection.Query<ProductVoucherPromo>("SP_ProductVoucherPromo", param: parameters, commandType: CommandType.StoredProcedure);
                return virtualCartProductVouchers;
            }

        }
      




    }

}