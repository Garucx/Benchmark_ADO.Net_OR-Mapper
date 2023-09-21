using Npgsql;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Connections
{
    public class ADONET
    { 
        public static SqlConnection _connectionADO;

        public void OpenConnection(SqlConnection connection)
        {
            _connectionADO = connection;
            _connectionADO.Open();
            _connectionADO.Close();
        }

        public static void OpenAdo()
        {
            _connectionADO = new SqlConnection("Server=.;Database=benchmark;Trusted_Connection=True;");
            _connectionADO.Open();
        }

        public static void CloseAdo()
        {
            _connectionADO.Dispose();
        }
    }
}
