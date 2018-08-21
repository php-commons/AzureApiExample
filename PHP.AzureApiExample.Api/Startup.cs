using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PHP.AzureApiExample.Domain;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.PlatformAbstractions;
using PHP.AzureApiExample.Api.Validators;
using PHP.AzureApiExample.Domain.Services;
using Swashbuckle.AspNetCore.Swagger;

namespace PHP.AzureApiExample.Api
{
    /// <summary>
    /// The web host startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Public accessor for Configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {            
            services.AddMvc()
                .AddFluentValidation();
                
            services.AddValidators();
            services.AddScoped<IPersonRepository, PersonRepository>();

            services.AddSwaggerGen(
                c =>
                {
                    c.SwaggerDoc("persons", new Info
                    {                       
                        Title = "Person Web API",
                        Description = "Prototype Person Web API",
                        Version="v1"
                    });
                    var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "PHP.AzureApiExample.Api.xml");
                    c.IncludeXmlComments(filePath);
                });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => 
                c.SwaggerEndpoint("/swagger/persons/swagger.json", "Person Web Api")
                );
        }
    }
}
