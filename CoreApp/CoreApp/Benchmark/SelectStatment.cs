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
    public class SelectStatment
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
        public void SelectAdo()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM dbo.Personen where ID = 1;", ADONET._connectionADO);
            command.ExecuteNonQuery();
        }

        [Benchmark]
        public void SelectNHibernate()
        {
            using (var _session = NHIBERNATE.GetSession())
            {
               var Person = _session.Get<Personen>(1);
            }
        }


        [Benchmark]
        public void SelectEF()
        {
            var Person = EFCORE._context.Personen.Find(1);
        }
    }
}
