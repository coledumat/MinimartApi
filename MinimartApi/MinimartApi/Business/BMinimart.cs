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
    public class BMinimart
    {
        /*
          Business Class Minimart.
        */

        private string CONNECTION_STRING = "";

        public BMinimart()
        {
            //TODO: read from de config file
            CONNECTION_STRING = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = C:\\MCO\\FUENTES\\PRUEBAS\\VISUALSTUDIO\\MMAPI\\MINIMARTAPI\\DB\\MINIMARTAPI.MDF;Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public IEnumerable<Minimart> list(int minimartId, string minimartName)
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
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