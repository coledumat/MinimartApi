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
    public class BCategory : BusinessClass
    {
        /*
          Business Class Category.
        */

        public IEnumerable<Category> list(int categoryId, string categoryName)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
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