using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MusalaApi.Model;
using MusalaApi.Repository;
using Newtonsoft.Json.Serialization;

namespace MusalaApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //In Producton connectionString have to move to system environment
            services.AddDbContext<ApiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MusalaApiConnection")));
            services.AddScoped<IGatewayRepository, GatewayRepository>();

            services.AddMvc(setupAction =>
            {
                setupAction.ReturnHttpNotAcceptable = true;
                //setupAction.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

            })
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddMvcOptions(o => o.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter()))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

        }
           

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(appBuilder =>
                    {
                        appBuilder.Run(async context =>
                        {
                            context.Response.StatusCode = 500;
                            await context.Response.WriteAsync("An unexpected fault happened.");

                        });
                    });
            }
           /// app.UseResponseCaching();

            app.UseMvc();
        }
    }
}
