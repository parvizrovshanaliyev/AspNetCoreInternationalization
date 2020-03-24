using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Formatters;
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
            });
            //
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
                //
                options.RespectBrowserAcceptHeader = true; // false by default
                // xml
                //options.OutputFormatters.Add(new XmlSerializerOutputFormatter());

            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // request localization
            app.UseRequestLocalization();
            //
            app.UseMvcWithDefaultRoute();
        }
    }
}
