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
    public class BCategory
    {
        /*
          Business Class Category.
        */

        private string CONNECTION_STRING = "";

        public BCategory()
        {
            //TODO: read from de config file
            CONNECTION_STRING = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = C:\\MCO\\FUENTES\\PRUEBAS\\VISUALSTUDIO\\MMAPI\\MINIMARTAPI\\DB\\MINIMARTAPI.MDF;Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public IEnumerable<Category> list(int categoryId, string categoryName)
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                var parameters = new DynamicParameters();
                parameters.Add("category_id", categoryId);
                parameters.Add("category_name", categoryName);

                var categories = connection.Query<Category>("SP_Category", param: parameters, commandType: CommandType.StoredProcedure);
                return categories;
            }

        }






    }

}