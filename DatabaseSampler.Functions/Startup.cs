using System;
using DatabaseSampler.Application.Data;
using DatabaseSampler.Application.Interfaces;
using DatabaseSampler.Application.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Http;
//using Microsoft.Extensions.Logging;
using Npgsql;

[assembly: FunctionsStartup(typeof(DatabaseSampler.Functions.Startup))]
namespace DatabaseSampler.Functions
{
    //https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection 
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            //builder.Services.AddHttpClient();

            //builder.Services.AddSingleton((s) => {
            //    return new MyService();
            //});

            builder.Services.AddTransient<IPostgresSqlRepository, PostgresSqlRepository>();
            builder.Services.AddTransient<IPostgresSqlService, PostgresSqlService>();

            //builder.Services.AddSingleton<ILoggerProvider, MyLoggerProvider>();

            //Configure PostgresSql
            var pgConnectionString = Environment.GetEnvironmentVariable("PostgreSqlConnectionString");
            var pgPassword = Environment.GetEnvironmentVariable("PostgreSqlDbPassword");
            var pgBuilder = new NpgsqlConnectionStringBuilder(pgConnectionString)
            {
                Password = pgPassword
            };

            builder.Services.AddDbContext<StudentDbContext>(options =>
                options.UseNpgsql(pgBuilder.ConnectionString));
        }
    }
}
