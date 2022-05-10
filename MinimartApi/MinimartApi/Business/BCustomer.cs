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
    public class BCustomer
    {
        /*
          Business Class Customer.
        */

        private string CONNECTION_STRING = "";

        public BCustomer()
        {
            //TODO: read from de config file
            CONNECTION_STRING = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = C:\\MCO\\FUENTES\\PRUEBAS\\VISUALSTUDIO\\MMAPI\\MINIMARTAPI\\DB\\MINIMARTAPI.MDF;Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        }

        public IEnumerable<Customer> list(int customerId, string customerFullName, string email)
        {
            using (IDbConnection connection = new SqlConnection(CONNECTION_STRING))
            {
                var parameters = new DynamicParameters();
                parameters.Add("customer_id", customerId );
                parameters.Add("customer_fullName", customerFullName );
                parameters.Add("email", email);

                var customers = connection.Query<Customer>("SP_Customer", param: parameters, commandType: CommandType.StoredProcedure);
                return customers;
            }

        }






    }

}