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
    public class BVirtualCartProductVoucher
    {
        /*
          Business Class Product.
        */

        private string CONNECTION_STRING = "";

        public BVirtualCartProductVoucher()
        {
            //TODO: read from de config file
            CONNECTION_STRING = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = C:\\MCO\\FUENTES\\PRUEBAS\\VISUALSTUDIO\\MMAPI\\MINIMARTAPI\\DB\\MINIMARTAPI.MDF;Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public IEnumerable<VirtualCartProductVoucher> list(int minimartId, string minimartName, 
                                                             int costumerId, string customerFullName,
                                                             int voucherId, int voucherNum,
                                                             int categoryId, string categoryName, int productId, string productName)
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                var parameters = new DynamicParameters();
                parameters.Add("minimart_id", minimartId);
                parameters.Add("minimart_name", minimartName);
                parameters.Add("customer_id", costumerId);
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






    }

}