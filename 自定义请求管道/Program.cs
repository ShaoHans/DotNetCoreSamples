using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace 请求管道原理
{
    class Program
    {
        private static List<Func<RequestDelegate, RequestDelegate>> _list = new List<Func<RequestDelegate, RequestDelegate>>();

        static void Main(string[] args)
        {
            
            Use((next) =>
            {
                return context =>
                {
                    Console.WriteLine("1...");
                    return next.Invoke(context);
                };
            });

            Use((next) =>
            {
                return context =>
                {
                    Console.WriteLine("2...");
                    return next.Invoke(context);
                };
            });

            RequestDelegate end = context =>
            {
                Console.WriteLine("End...");
                return Task.CompletedTask;
            };

            _list.Reverse();
            foreach (var middleware in _list)
            {
                end = middleware.Invoke(end);
            }

            end.Invoke(new Context());

            Console.ReadKey();
        }

        static void Use(Func<RequestDelegate,RequestDelegate> middleware)
        {
            _list.Add(middleware);
        }

        
    }
}
