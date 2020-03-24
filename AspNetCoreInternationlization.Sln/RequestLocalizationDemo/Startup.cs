using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace RequestLocalizationDemo
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
            // localization
            services.AddLocalization(options =>
            {
                options.ResourcesPath = "Resources";
            });
            //
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SupportedUICultures = new List<CultureInfo>
                {
                    new CultureInfo("bs"),
                    new CultureInfo("de"),
                    new CultureInfo("es"),
                    new CultureInfo("fr-FR"),
                    new CultureInfo("en-GB")
                };
                options.FallBackToParentUICultures = false;
                // default culture
                options.DefaultRequestCulture = new RequestCulture("en-GB");
                options.RequestCultureProviders.Insert(0,
                    new RouteDataRequestCultureProvider());
            });
            //
            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
                .AddDataAnnotationsLocalization();
            //services.AddControllersWithViews(options =>
            //{
            //    //options.EnableEndpointRouting = true;
            //    //
            //    options.RespectBrowserAcceptHeader = true; // false by default
            //    // xml
            //    //options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            //});
            //IMvcCoreBuilder mvcCoreBuilder = services.AddMvcCore(options =>
            //{
            //    options.EnableEndpointRouting= false;
            //    options.RespectBrowserAcceptHeader = true; // false by default
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            // request localization
            app.UseRequestLocalization();
            //
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
               
                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{controller=Enumerations}/{action=Genders}/{id?}");
                endpoints.MapControllerRoute(
                    name: "defaultWithCulture",
                    pattern: "{ui-culture?}/{controller=Enumerations}/{action=Genders}/{id?}");
            });
            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(
            //        name: "defaultWithCulture",
            //        template: "{ui-culture?}/{controller=Enumerations}/{action=Genders}/{id?}");
            //});
            ////
            //app.UseMvcWithDefaultRoute();
        }
    }
}
