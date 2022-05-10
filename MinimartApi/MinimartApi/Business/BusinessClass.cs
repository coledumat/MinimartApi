using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;

namespace MinimartApi.Business
{
    public class BusinessClass
    {

        //private const string CONNECTION_STRING = "Server=.;Database=MinimartApi.mdf;Trusted_Connection=True;"; 

        //master
        // private const string CONNECTION_STRING = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = master; Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"

        //minimart:                                     
        //private const string CONNECTION_STRING = "Data Source = (localdb)\\MSSQLLocalDB;Initial Catalog = C:\\MCO\\FUENTES\\PRUEBAS\\VISUALSTUDIO\\MMAPI\\MINIMARTAPI\\DB\\MINIMARTAPI.MDF;Integrated Security = True; Connect Timeout = 30; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        protected string connectionString = "";

        public BusinessClass()
        {
            connectionString = ConfigurationManager.ConnectionStrings["MinimartDBConection"].ToString();
        }

    }
}