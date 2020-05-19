using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace RequestLocalizationTwo
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


            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.SupportedUICultures = new List<CultureInfo>
                {
                    new CultureInfo("bs"),
                    new CultureInfo("de-DE"),
                    new CultureInfo("es"),
                    new CultureInfo("fr-FR"),
                    new CultureInfo("en-GB"),
                    new CultureInfo("en-US")
                };
                //options.FallBackToParentUICultures = false;
                // default culture
                options.DefaultRequestCulture = new RequestCulture("en-GB");
                //options.RequestCultureProviders.Insert(0,
                //    new RouteDataRequestCultureProvider());
            });


            services.AddControllersWithViews()
                .AddNewtonsoftJson();
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseRequestLocalization();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Enumerations}/{action=Genders}/{id?}");
            });
        }
    }
}
