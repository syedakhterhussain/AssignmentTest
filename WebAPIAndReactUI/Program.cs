using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIAndReactUI.Data;

namespace WebAPIAndReactUI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var dbHost = CreateHostBuilder(args).Build();

            using (var scope = dbHost.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<CovidDataContext>();
                    new CovidDbInitializer().CreateDb(context);
                   
                }
                catch (Exception ex)
                {

                }
            }

            dbHost.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
