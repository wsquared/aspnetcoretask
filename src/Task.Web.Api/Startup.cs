using System.Security.Claims;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.OptionsModel;
using Newtonsoft.Json.Serialization;

using Task.Core;
using Task.Data.Contracts.Dapper;
using Task.Data.Contracts.Factory;
using Task.Data.Core;
using Task.Data.Dapper;
using Task.Data.Factory;

namespace Task
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<Auth0Settings>(Configuration.GetSection("Auth0"));
            services.Configure<DataSettings>(Configuration.GetSection("Data"));

            // Add framework services.
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver =
                    new CamelCasePropertyNamesContractResolver();
                })
                .AddDataAnnotationsLocalization();

            // DI Bindings
            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<ISqlServerConnectionFactory, SqlServerConnectionFactory>();

            // Cors for development
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Allow cors when developing
            if (env.IsDevelopment())
            {
                app.UseCors(builder => builder.WithOrigins("http://localhost:3000"));
            }

            // Auth0
            var logger = loggerFactory.CreateLogger("Auth0");
            var settings = app.ApplicationServices.GetService<IOptions<Auth0Settings>>();

            app.UseJwtBearerAuthentication(options =>
            {
                options.Audience = settings.Value.ClientId;
                options.Authority = $"https://{settings.Value.Domain}";
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        logger.LogError("Authentication failed.", context.Exception);
                        return System.Threading.Tasks.Task.FromResult(0);
                    },
                    OnValidatedToken = context =>
                    {
                        var claimsIdentity = context.AuthenticationTicket.Principal.Identity as ClaimsIdentity;
                        claimsIdentity?.AddClaim(new Claim("id_token",
                            context.Request.Headers["Authorization"][0].Substring(context.AuthenticationTicket.AuthenticationScheme.Length + 1)));

                        // OPTIONAL: you can read/modify the claims that are populated based on the JWT
                        // claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, claimsIdentity.FindFirst("name").Value));
                        return System.Threading.Tasks.Task.FromResult(0);
                    }
                };
            });

            app.UseIISPlatformHandler();

            app.UseStaticFiles();

            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
