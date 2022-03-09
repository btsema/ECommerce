using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using System.IO;

namespace API
{
    public class Startup
    {
        public string DatabaseConnectionString { get; set; }
        public string RedisConnectionString { get; set; }
        public string RedisPassword { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            DatabaseConnectionString = Configuration["ConnectionString:DefaultConnection"];
            RedisConnectionString = Configuration["RedisConnectionString:RedisConnection"];
            RedisPassword = Configuration["RedisConnectionString:RedisPassword"];
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mapping));

            services.AddControllers();

            services.AddDbContext<StoreContext>(options => options.UseSqlServer(DatabaseConnectionString));

            services.AddDbContext<AppIdentityDbContext>(options => options.UseSqlServer(DatabaseConnectionString));

            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var configuration = new ConfigurationOptions
                {
                    EndPoints = { RedisConnectionString },
                    Password = RedisPassword
                };

                return ConnectionMultiplexer.Connect(configuration);
            });

            services.AddApplicationServices();

            services.AddIdentityServices(Configuration);

            services.AddSwaggerDocumentation();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200");
                });
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ExceptionHanlderMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseSwaggerDocumentation();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Files")
                ),
                RequestPath = "/files"
            });

            app.UseCors("CorsPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapFallbackToController("Index", "Fallback");
            });
        }
    }
}
