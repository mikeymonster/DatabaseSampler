using DatabaseSampler.Application.Data;
using DatabaseSampler.Application.DataGenerator;
using DatabaseSampler.Application.Interfaces;
using DatabaseSampler.Application.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;

namespace DatabaseSampler
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc();
            services.AddControllersWithViews();

            services.AddTransient<IDataGenerator, BogusDataGenerator>();

            services.AddHttpClient<ILocationService, LocationService>();

            services.AddTransient<IPostgresSqlRepository, PostgresSqlRepository>();
            services.AddTransient<IPostgresSqlService, PostgresSqlService>();

            //Configure SQL Server
            var sqlConnectionString = Configuration["SqlConnectionString"];
            services.AddDbContext<LocationDbContext>(options =>
                options.UseSqlServer(sqlConnectionString, builder => builder
                    .UseNetTopologySuite()
                    .EnableRetryOnFailure()), 
                ServiceLifetime.Transient);

            //Configure PostgresSql
            var pgConnectionString = Configuration["PostgreSql:ConnectionString"];
            var pgPassword = Configuration["PostgreSql:DbPassword"];
            var pgBuilder = new NpgsqlConnectionStringBuilder(pgConnectionString)
            {
                Password = pgPassword
            };

            services.AddDbContext<StudentDbContext>(options => 
                options.UseNpgsql(pgBuilder.ConnectionString));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllers();
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
