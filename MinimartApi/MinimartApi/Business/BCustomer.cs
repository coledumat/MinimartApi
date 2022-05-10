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
    public class BCustomer : BusinessClass
    {
        /*
          Business Class Customer.
        */

        public IEnumerable<Customer> list(int customerId, string customerFullName, string email)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
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