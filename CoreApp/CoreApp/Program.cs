using System;
using CoreApp.Benchmark;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Validators;
using CoreApp.Benchmark;
using Microsoft.Extensions.Configuration;
using CoreApp.Connections;
using CoreApp.DTOs;
using NHibernate;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using BenchmarkDotNet.Attributes;

namespace CoreApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var config = new ManualConfig()
                    .WithOptions(ConfigOptions.DisableOptimizationsValidator)
                    .AddValidator(JitOptimizationsValidator.DontFailOnError)
                    .AddLogger(ConsoleLogger.Default)
                    .AddColumnProvider(DefaultColumnProviders.Instance);

            AppConfiguration.GiveArgs(args);
            AppConfiguration.ConfigurationConfig();

            //BenchmarkRunner.Run<InsertWithQuery>(config);
            //BenchmarkRunner.Run<OpenConnection>(config);
            //BenchmarkRunner.Run<InsertStatement>(config);
            BenchmarkRunner.Run<SelectStatment>(config);
            //BenchmarkRunner.Run<UpdateStatement>(config);
            //BenchmarkRunner.Run<ComplexSelect>(config);
            //BenchmarkRunner.Run<DropStatement>(config);



            Console.ReadKey();
        }
    }
}

