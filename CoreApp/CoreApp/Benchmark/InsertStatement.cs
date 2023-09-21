using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using CoreApp.Connections;
using CoreApp.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace CoreApp.Benchmark
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.Method)]
    [RankColumn]
    public class InsertStatement
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
        public void InsertADO()
        {
            SqlCommand command = new SqlCommand("INSERT INTO dbo.Personen (Name,Email,Adresse) VALUES ('Attila','attila.molnar@techtalk.at','Hetzendorfer Straße');",ADONET._connectionADO);
            command.ExecuteNonQuery();
        }
        [Benchmark]
        public void InsertNHibernate()
        {
            Personen person = new Personen { Adresse= "Hetzendorfer Straße",Email= "attila.molnar@techtalk.at",Name="Attila"};
            using (var _session = NHIBERNATE.GetSession())
            {
                using(var transaction = _session.BeginTransaction())
                {
                    _session.SaveOrUpdate(person);
                    transaction.Commit();
                }
            }
        }

        [Benchmark]
        public void InsertEF()
        {
            EFCORE._context.Add(new Personen { Adresse = "Hetzendorfer Straße", Name = "Attila", Email = "attila.molnar@techtalk.at" });
            EFCORE._context.SaveChanges();
        }

        [GlobalCleanup]
        public void Cleanup()
        {
            SqlCommand command = new SqlCommand("Delete from dbo.Personen where name='Attila';", ADONET._connectionADO);
            command.ExecuteNonQuery();
        }

    }
}
