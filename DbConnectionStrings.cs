using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aiursoft.Pylon
{
    public static class DbConnectionStrings
    {
        public static bool testing = false;
        public static void ConnectToAiursoftDatabase<T>(this IServiceCollection service, string dbName, bool debug) where T : DbContext
        {
            if (debug || testing)
            {
                Console.WriteLine("Using development database!");
                service.AddDbContext<T>(options =>
                    options.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Database={dbName}.Test;Trusted_Connection=True;MultipleActiveResultSets=true"));
            }
            else
            {
                Console.WriteLine("Using production database!");
                service.AddDbContext<T>(options =>
                    options.UseSqlServer($"Data Source=server.aiursoft.com;Initial Catalog={dbName};Integrated Security=True"));
            }
        }
    }
}
