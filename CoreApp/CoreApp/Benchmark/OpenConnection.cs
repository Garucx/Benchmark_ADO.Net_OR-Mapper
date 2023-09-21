using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreApp.Connections;
using BenchmarkDotNet.Running;
using System.Security.Cryptography;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace CoreApp.Benchmark
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Method)]
    [RankColumn]
    public class OpenConnection
    {
        [Benchmark]
        public void AdoConnectionOpen()
        {
            SqlConnection connection = new SqlConnection("Server =.; Database = benchmark; Trusted_Connection = True;");
            ADONET AdoNet = new ADONET();
            AdoNet.OpenConnection(connection);
            connection.Close();
        }

        [Benchmark]
        public void NHibernateOpen()
        {
            NHIBERNATE session = new NHIBERNATE();
            session.configandOpenNHibernate();
            
        }

        [Benchmark]
        public void EFOpen()
        {
            EFCORE.OpenEF();
            EFCORE._context.Database.OpenConnection();
            EFCORE._context.Database.CloseConnection();
        }
    }

 
}
