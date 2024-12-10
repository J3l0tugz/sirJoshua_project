using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace project_open
{
    public class DatabaseConnection
    {
        private static string DATABASE = "open_project_db";
        private static string SERVER = "(local)\\sqlexpress";


        protected static SqlConnection SqlConn()
        {
            return new SqlConnection($"SERVER={SERVER};DATABASE={DATABASE};Integrated Security=True;TrustServerCertificate=True");
        }
    }
}