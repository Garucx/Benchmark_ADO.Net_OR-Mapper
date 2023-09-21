using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CoreApp.Connections;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreApp.DTOs;

namespace CoreApp.Benchmark
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class ComplexSelect
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
        public void ComplexSelectEF()
        {
            var test = EFCORE._context.Personen.OrderBy(x => x.Name).Count();
        }

        [Benchmark]
        public void ComplexSelectAdo()
        {
            SqlCommand command = new SqlCommand("select Count(*) from dbo.Personen where name='TEST';", ADONET._connectionADO);
            command.ExecuteNonQuery();
        }

        [Benchmark]
        public void ComplexSelectNHibernate()
        {
            using (var _session = NHIBERNATE.GetSession())
            {
                var Person = _session.QueryOver<Personen>().Where(x => x.Name== "TEST").RowCount();
            }
        }

    }
}
