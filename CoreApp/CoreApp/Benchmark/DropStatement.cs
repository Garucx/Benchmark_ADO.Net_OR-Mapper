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
    public class DropStatement
    {
        [GlobalSetup]
        public void Setup()
        {
            ADONET.OpenAdo();
            NHIBERNATE.OpenNHibernate();
            EFCORE.OpenEF();
            EFCORE._context.Database.OpenConnection();
        }

        [IterationSetup]
        public void IterationSetup()
        {
            using (var _session = NHIBERNATE.GetSession())
            {
                _session.Save(_person);
                _session.Flush();
            }
        }
        public Personen _person { get; set; } = new DTOs.Personen { Adresse = "Hetzendorfer Straße", Email = "attila.molnar@techtalk.at", Name = "attila" };

       [Benchmark]

        public void ADONETDELETE()
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Personen WHERE Name = @Name", ADONET._connectionADO);
            cmd.Parameters.AddWithValue("Name", "attila");
            cmd.ExecuteNonQuery();
        }

        [Benchmark]
        public void HibernateDELETE()
        {
            using (var _session = NHIBERNATE.GetSession())
            {
                _session.Delete(_person);
                _session.Flush();
            }
        }

        [Benchmark]
        public void EFCOREDELETE()
        {
            EFCORE._context.Personen.Remove(_person);
            EFCORE._context.SaveChanges();
        }
    }
}
