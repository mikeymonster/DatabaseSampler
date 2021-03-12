using System;
using System.Threading.Tasks;
using DatabaseSampler.Application.Data;
using DatabaseSampler.Application.Interfaces;
using DatabaseSampler.Application.Services;
//using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace DatabaseSampler.Functions
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = new HostBuilder()
                .ConfigureFunctionsWorkerDefaults()
                .ConfigureServices(s =>
                {
                    //s.AddSingleton<IHttpResponderService, DefaultHttpResponderService>();
                    s.AddTransient<IPostgresSqlRepository, PostgresSqlRepository>();
                    s.AddTransient<IPostgresSqlService, PostgresSqlService>();

                    //s.AddSingleton<ILoggerProvider, MyLoggerProvider>();

                    //Configure PostgresSql
                    var pgConnectionString = Environment.GetEnvironmentVariable("PostgreSqlConnectionString");
                    var pgPassword = Environment.GetEnvironmentVariable("PostgreSqlDbPassword");
                    var pgBuilder = new NpgsqlConnectionStringBuilder(pgConnectionString)
                    {
                        Password = pgPassword
                    };

                    s.AddDbContext<StudentDbContext>(options =>
                        options.UseNpgsql(pgBuilder.ConnectionString));

                    Console.WriteLine("Adding HTTP Client");
                    s.AddHttpClient<ILocationService, LocationService>();

                    s.AddTransient<ILocationService, LocationService>();
                })
                .Build();

            await host.RunAsync();
        }
    }
}
