using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CoreFaces.Status.Web
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
               
            }

         


           app.UseBranchWithServices("/admin",
           services =>
           {
               //services.AddTransient<IHiService, ResourceService>();
               services.AddMvc().ConfigureApplicationPartManager(manager =>
               {
                   manager.FeatureProviders.Clear();
                   manager.FeatureProviders.Add(new TypedControllerFeatureProvider<CoreFaces.Status.Web.Controllers.ValuesController>());
               });
           },
           appBuilder =>
           {
               appBuilder.UseMvc();
           });

        }
    }


    public class TypedControllerFeatureProvider<TController> : ControllerFeatureProvider where TController : ControllerBase
    {
        protected override bool IsController(TypeInfo typeInfo)
        {
            if (!typeof(TController).GetTypeInfo().IsAssignableFrom(typeInfo)) return false;
            return base.IsController(typeInfo);
        }
    }

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseBranchWithServices(this IApplicationBuilder app, PathString path,
            Action<IServiceCollection> servicesConfiguration, Action<IApplicationBuilder> appBuilderConfiguration)
        {
            var webHost = new WebHostBuilder().UseKestrel().ConfigureServices(servicesConfiguration).UseStartup<EmptyStartup>().Build();
            var serviceProvider = webHost.Services;
            var serverFeatures = webHost.ServerFeatures;

            var appBuilderFactory = serviceProvider.GetRequiredService<IApplicationBuilderFactory>();
            var branchBuilder = appBuilderFactory.CreateBuilder(serverFeatures);
            var factory = serviceProvider.GetRequiredService<IServiceScopeFactory>();

            branchBuilder.Use(async (context, next) =>
            {
                using (var scope = factory.CreateScope())
                {
                    context.RequestServices = scope.ServiceProvider;
                    await next();
                }
            });

            appBuilderConfiguration(branchBuilder);

            var branchDelegate = branchBuilder.Build();

            return app.Map(path, builder =>
            {
                builder.Use(async (context, next) =>
                {
                    await branchDelegate(context);
                });
            });
        }

        private class EmptyStartup
        {
            public void ConfigureServices(IServiceCollection services) { }

            public void Configure(IApplicationBuilder app) { }
        }
    }
}
