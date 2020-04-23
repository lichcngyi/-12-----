using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace EfConfigProvider
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            var config1 = builder.Build();
            
            var builder2 = new ConfigurationBuilder()
            .AddEfConfig(option => option.UseSqlServer(config1.GetConnectionString("conn1")));

            var config2 = builder2.Build();


            Console.WriteLine(config2["key1"]);
            Console.WriteLine(config2["key2"]);


            Console.WriteLine("Hello World!");

            Console.ReadKey();
        }
    }
}
