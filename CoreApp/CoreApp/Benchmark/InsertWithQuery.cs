using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CoreApp.Connections;
using CoreApp.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreApp.Benchmark
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Method)]
    [RankColumn]
    public class InsertWithQuery
    {
        
        [GlobalSetup]
        public void Setup()
        {
            ADONET.OpenAdo();
            NHIBERNATE.OpenNHibernate();
            EFCORE.OpenEF();
            EFCORE._context.Database.OpenConnection();
        }
    
        [Benchmark] 
        public void InsertNHibernate()
        {
            using (var _session = NHIBERNATE.GetSession())
            {
                using (var transaction = _session.BeginTransaction())
                {
                    _session.Query<Personen>("INSERT INTO dbo.Personen (Name,Email,Adresse) VALUES ('Attila','attila.molnar@techtalk.at','Hetzendorfer Straße');");
                    transaction.Commit();
                }
            }
        }
        [Benchmark]
        public void InsertEF()
        {
            EFCORE._context.Database.ExecuteSqlRaw("INSERT INTO dbo.Personen (Name,Email,Adresse) VALUES ('Attila','attila.molnar@techtalk.at','Hetzendorfer Straße');");
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            EFCORE._context.Database.CloseConnection();
            SqlCommand command = new SqlCommand("Delete from dbo.Personen where name='Attila';", ADONET._connectionADO);
            command.ExecuteNonQuery();
        }
    }
}
