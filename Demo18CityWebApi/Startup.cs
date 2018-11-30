using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
//2
using Newtonsoft.Json.Serialization;
//1
using Swashbuckle.AspNetCore.Swagger;
//4
using Demo18CityWebApi.Model;

namespace Demo18CityWebApi
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
			//services.AddTransient() create per each use
            //services.AddScoped(); create per each request
            //services.AddSingleton(); create from start up

            //3
            services.AddMvc()
				.AddJsonOptions(o =>
                                 {
                                     if (o.SerializerSettings.ContractResolver != null)
                                     {
                                         var castedResolver = o.SerializerSettings.ContractResolver
                                                         as DefaultContractResolver;
                                         castedResolver.NamingStrategy = null;
                                     }
                                     o.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                                     o.SerializerSettings.MissingMemberHandling = Newtonsoft.Json.MissingMemberHandling.Ignore;
                                 });
			services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });

			services.AddDbContext<CityDBContext>();
			services.AddScoped<ICityDataStore, CityDataStore>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //1
			app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseMvc();
        }
    }
}
