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
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public  class UpdateStatement
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
        public void UpdateEF()
        {
            var Person = EFCORE._context.Personen.Find(1);
            Person.Name = "Lukas";
            EFCORE._context.Personen.Update(Person);
            EFCORE._context.SaveChanges();
        }

        [Benchmark]
        public void UpdateAdo()
        {
            SqlCommand command = new SqlCommand("UPDATE dbo.Personen set Name = 'Lukas' where ID = 1;", ADONET._connectionADO);
            command.ExecuteNonQuery();
        }

        [Benchmark]
        public void UpdateNHibernate()
        {
            using (var _session = NHIBERNATE.GetSession())
            {
                var Person = _session.Get<Personen>(1);
                Person.Name = "Lukas";
                _session.Update(Person);
            }
        }

        [IterationCleanup] 
        public void Cleanup() {
            SqlCommand command = new SqlCommand("UPDATE dbo.Personen set Name = 'Attila' where ID = 1;", ADONET._connectionADO);
            command.ExecuteNonQuery();
        }
    }
}
