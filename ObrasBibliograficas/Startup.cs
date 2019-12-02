using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using ObrasBibliograficas.DI;
using ObrasBibliograficas.Domain.Interfaces;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.IO;

namespace ObrasBibliograficas.Api
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

            var stringConnection = Configuration.GetConnectionString("ConnectionString");
            Bootstrap.Configure(services, stringConnection);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region Configure Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = " Obras Bibliograficas",
                    Version = "v1",
                    Contact = new Contact
                    {
                        Name = "Alex Rocha",
                        Url = "https://github.com/alrocha003"
                    }
                });

                c.IncludeXmlComments(Path.Combine(PlatformServices.Default.Application.ApplicationBasePath,
                    $"{PlatformServices.Default.Application.ApplicationName}.xml"));
            });
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.Use(
                async (context, next) =>
                {
                    await next.Invoke();
                    var unitOfWork = (IUnityOfWork)context.RequestServices.GetService(typeof(IUnityOfWork));
                    await unitOfWork.Commit();

                });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseStaticFiles();
            app.UseCors(b => b
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseHttpsRedirection();
            app.UseMvc();

            app.UseSwagger();
        }

        //#region Resolve Swagger
        //public static IServiceCollection AddSwaggerGen( this IServiceCollection services,
        //        Action<SwaggerGenOptions> setupAction = null)
        //{
        //    // Add Mvc convention to ensure ApiExplorer is enabled for all actions
        //    services.Configure<MvcOptions>(c =>
        //        c.Conventions.Add(new SwaggerApplicationConvention()));

        //    // Register generator and it's dependencies
        //    services.AddTransient<ISwaggerProvider, SwaggerGenerator>();
        //    services.AddTransient<ISchemaGenerator, SchemaGenerator>();
        //    services.AddTransient<IApiModelResolver, JsonApiModelResolver>();

        //    // Register custom configurators that assign values from SwaggerGenOptions (i.e. high level config)
        //    // to the service-specific options (i.e. lower-level config)
        //    services.AddTransient<IConfigureOptions<SwaggerGeneratorOptions>, ConfigureSwaggerGeneratorOptions>();
        //    services.AddTransient<IConfigureOptions<SchemaGeneratorOptions>, ConfigureSchemaGeneratorOptions>();

        //    // Used by the <c>dotnet-getdocument</c> tool from the Microsoft.Extensions.ApiDescription.Server package.
        //    services.TryAddSingleton<IDocumentProvider, DocumentProvider>();

        //    if (setupAction != null) services.ConfigureSwaggerGen(setupAction);

        //    return services;
        //}
        //#endregion
    }
}
