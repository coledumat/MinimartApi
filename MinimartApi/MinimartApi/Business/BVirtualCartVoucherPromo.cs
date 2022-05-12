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
    /// Business Class BVirtualCartVoucherPromo.
    /// Represents a productVoucherPromo and categoryVoucherPromo in the virtual cart
    /// </summary>
    public class BVirtualCartVoucherPromo : BusinessClass
    {
        // /////////////////////////
        // Business  VoucherPromo
        // /////////////////////////

        /// <summary>
        /// add a VoucherPromo to the virtual cart (productVoucher or categoryVoucher)
        /// </summary>
        /// <param name="newVirtualCartVoucherPromo"></param>
        /// <returns></returns>
        public int CreateVirtualCartVoucherPromo(VirtualCartVoucherPromoModel newVirtualCartVoucherPromo)
        {
            //get header of VoucherPromo
            BVoucherPromo bVoucherPromo = new BVoucherPromo();
            List < VoucherPromoModel> headerPromoModel;
            headerPromoModel = bVoucherPromo.listVoucherPromo(newVirtualCartVoucherPromo).ToList()  ;

            //validate conditions
            if ( headerPromoModel.Count() < 0 )
            {
                throw new Exception("Invalid VoucherPromo - incorrect minimart_id or Voucher_Id"); 
            }
            if ( headerPromoModel[1].VIniNumber > newVirtualCartVoucherPromo.NumVoucher ||
                 headerPromoModel[1].VEndNumber < newVirtualCartVoucherPromo.NumVoucher )
            {
                throw new Exception("Invalid VoucherPromo - incorrect NumVoucher");
            }
            if (headerPromoModel[1].StartDate > DateTime.Now ||
                headerPromoModel[1].StartDate < DateTime.Now)
            {
                throw new Exception("Invalid VoucherPromo - out of date");
            }

            int affectedRows = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                //asign voucherPromo to Virtual cart 
                string sql = "INSERT INTO VirtualCart_Voucher ( Id_Minimart, Id_Customer, Id_Voucher, Num_Voucher) " +
                                                      "VALUES ( @id_Minimart, @Id_Customer, @Id_Voucher, @Num_Voucher);";

                affectedRows = connection.Execute(sql, new {
                                                            Id_Minimart = newVirtualCartVoucherPromo.MinimartId,
                                                            Id_Customer = newVirtualCartVoucherPromo.CustomerId,
                                                            Id_Voucher = newVirtualCartVoucherPromo.VoucherId,
                                                            Num_Voucher = newVirtualCartVoucherPromo.NumVoucher 
                                                            });
            }
            return affectedRows;
        }

        /// <summary>
        /// remove a VoucherPromo to the virtual cart (productVoucher or categoryVoucher)
        /// </summary>
        /// <param name="aVirtualCartVoucherPromo"></param>
        /// <returns></returns>
        public int DeleteVirtualCartVoucherPromo(VirtualCartVoucherPromoModel aVirtualCartVoucherPromo)
        {
            string sql = " DELETE VirtualCart_Voucher " +
                           "WHERE Id_Minimart = @Id_Minimart " +
                             "AND Id_Customer = @Id_Customer " +
                             "AND Id_Voucher = @Id_Voucher " +
                             "AND Num_Voucher = @Num_Voucher;"; 

            int affectedRows = 0;
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                affectedRows = connection.Execute(sql, new
                {
                    Id_Minimart = aVirtualCartVoucherPromo.MinimartId,
                    Id_Customer = aVirtualCartVoucherPromo.CustomerId,
                    Id_Voucher = aVirtualCartVoucherPromo.VoucherId,
                    Num_Voucher = aVirtualCartVoucherPromo.NumVoucher
                });
            }

            return affectedRows;
        }


        // /////////////////////////
        // Business  ProductVoucherPromo
        // /////////////////////////


        /// <summary>
        /// List the productVouchers in the virtual cart of a minimart
        /// </summary>
        /// <param name="minimartId">0 for all</param>
        /// <param name="minimartName">full o partial name</param>
        /// <param name="customerId">0 for all</param>
        /// <param name="customerFullName">full o partial name</param>
        /// <param name="voucherId">0 for all</param>
        /// <param name="voucherNum">0 for all</param>
        /// <param name="categoryId">0 for all</param>
        /// <param name="categoryName">full o partial name</param>
        /// <param name="productId">0 for all</param>
        /// <param name="productName">full o partial name</param>
        /// <returns>IEnumerable<VirtualCartProductVoucher></returns>
        public IEnumerable<VirtualCartProductVoucher> listProductVoucher(int minimartId, string minimartName, 
                                                             int customerId, string customerFullName,
                                                             int voucherId, int voucherNum,
                                                             int categoryId, string categoryName, int productId, string productName)
        {
            using (IDbConnection connection = new SqlConnection(connectionString ))
            {
                var parameters = new DynamicParameters();
                parameters.Add("minimart_id", minimartId);
                parameters.Add("minimart_name", minimartName);
                parameters.Add("customer_id", customerId);
                parameters.Add("customer_fullName", customerFullName);
                parameters.Add("voucher_id", voucherId);
                parameters.Add("voucher_num", voucherNum);
                parameters.Add("category_id", categoryId);
                parameters.Add("category_name", categoryName);
                parameters.Add("product_id", productId);
                parameters.Add("product_name", productName);

                var virtualCartProductVouchers = connection.Query<VirtualCartProductVoucher>("SP_VirtualCartProductVoucher", param: parameters, commandType: CommandType.StoredProcedure);
                return virtualCartProductVouchers;
            }

        }


        // /////////////////////////
        // Business  categoryVoucherPromo
        // /////////////////////////

        /// <summary>
        /// List the categoryVouchers in the virtual cart of a minimart
        /// </summary>
        /// <param name="minimartId">0 for all</param>
        /// <param name="minimartName">full o partial name</param>
        /// <param name="customerId">0 for all</param>
        /// <param name="customerFullName">full o partial name</param>
        /// <param name="voucherId">0 for all</param>
        /// <param name="voucherNum">0 for all</param>
        /// <param name="categoryId">0 for all</param>
        /// <param name="categoryName">full o partial name</param>
        /// <returns>IEnumerable<VirtualCartCategoryVoucher></returns>
        public IEnumerable<VirtualCartCategoryVoucher> listCategoryVoucher(int minimartId, string minimartName,
                                                             int customerId, string customerFullName,
                                                             int voucherId, int voucherNum,
                                                             int categoryId, string categoryName, int productId, string productName)
        {

            return new List<VirtualCartCategoryVoucher> { };
        }


    }

}