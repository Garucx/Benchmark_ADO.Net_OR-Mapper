using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftAntimalwareAMFilter;
using Microsoft.Extensions.Configuration;

namespace CoreApp
{
    public class AppConfiguration
    {
        private static string[] _args { get; set; }
        public static IConfiguration _config;
        public static void GiveArgs(string[] args)
        {
            _args = args;
        }
        public static void ConfigurationConfig()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .AddCommandLine(_args)
                .Build();
        }
    }
}
