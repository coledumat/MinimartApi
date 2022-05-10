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
    public class BMinimart : BusinessClass
    {
        /*
          Business Class Minimart.
        */

        public IEnumerable<Minimart> list(int minimartId, string minimartName)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Minimart_id", minimartId);
                parameters.Add("Minimart_Name", minimartName);

                var minimarts = connection.Query<Minimart>("SP_Minimart", param: parameters, commandType: CommandType.StoredProcedure);
                return minimarts;
            }

        }






    }

}