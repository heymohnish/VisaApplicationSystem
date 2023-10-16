using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace VisaApplicationSystem.Repository
{
    public class BaseDatabaseConnection
    {
        protected SqlConnection connection;

        protected void InitializeConnection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DBSC"].ToString();
            connection = new SqlConnection(connectionString);
        }
    }
}