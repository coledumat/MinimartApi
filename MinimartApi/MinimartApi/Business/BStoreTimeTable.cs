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
    public class BStoreTimeTable : BusinessClass
    {
        /*
          Business Class StoreTimeTable.
        */

        public IEnumerable<StoreTimeTable> list(int minimartId, string minimartName, string workingDay)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
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