using System;
using Microsoft.Extensions.Configuration;

namespace JsonFileConfiguration
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("Class.json");
            var configruation = builder.Build();

            Console.WriteLine($"{configruation["ClassName"]}");
            Console.WriteLine($"{configruation["Students:0:Name"]}");
            Console.WriteLine($"{configruation["Students:0:Age"]}");
            Console.WriteLine($"{configruation["Students:1:Name"]}");
            Console.WriteLine($"{configruation["Students:1:Age"]}");
            Console.ReadKey();
        }
    }
}
