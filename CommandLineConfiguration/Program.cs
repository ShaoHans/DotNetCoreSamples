using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace CommandLineConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            var dict = new Dictionary<string, string>
            {
                { "school","哈佛"},
                { "phone","15893256"}
            };

            var builder = new ConfigurationBuilder()
                .AddInMemoryCollection(dict)
                .AddCommandLine(args);

            var configuration = builder.Build();
            foreach (var item in configuration.AsEnumerable())
            {
                Console.WriteLine($"key={item.Key},value={item.Value}");
            }
            //Console.WriteLine($"name={configuration["name"]},age={configuration["age"]}");

            Console.ReadKey();
        }
    }
}
