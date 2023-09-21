using System.Data.SqlClient;
using System.Reflection;
using System.Reflection.PortableExecutable;
using BenchmarkDotNet.Running;
using CoreApp.DTOs;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftWindowsTCPIP;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

namespace CoreApp.Connections
{
    public class NHIBERNATE
    {
        private static ISessionFactory _sessionFactory;
        public void configandOpenNHibernate()
        {
            _sessionFactory = Config("Server =.; Database = benchmark; Trusted_Connection = True;");
            var a = _sessionFactory.OpenSession();
            a.Close();
        }
        public static void OpenNHibernate()
        {
            _sessionFactory = Config("Server =.; Database = benchmark; Trusted_Connection = True;");
        }
        private static ISessionFactory Config(string connect)
        {
            return Fluently.Configure().Database(MsSqlConfiguration.MsSql2012.ConnectionString(connect).ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Personen>()).ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false)).BuildSessionFactory();
        }

        public static ISession GetSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}
