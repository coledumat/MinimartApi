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
    public class BStoreTimeTable
    {
        /*
          Business Class StoreTimeTable.
        */

        private string CONNECTION_STRING = "";

        public BStoreTimeTable()
        {
            //TODO: read from de config file
            CONNECTION_STRING = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = C:\\MCO\\FUENTES\\PRUEBAS\\VISUALSTUDIO\\MMAPI\\MINIMARTAPI\\DB\\MINIMARTAPI.MDF;Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public IEnumerable<StoreTimeTable> list(int minimartId, string minimartName, string workingDay)
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                var parameters = new DynamicParameters();
                parameters.Add("Minimart_id", minimartId);
                parameters.Add("Minimart_Name", minimartName);
                parameters.Add("WorkingDay", workingDay);

                var storetimetable = connection.Query<StoreTimeTable>("SP_StoreTimeTable", param: parameters, commandType: CommandType.StoredProcedure);
                return storetimetable;
            }

        }






    }

}