using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Haushaltsbuch.WebApi.Haushaltsbuch
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args: args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args: args)
                .ConfigureWebHostDefaults(configure: webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
