using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using StringLocalizersDemo.Localization;
using StringLocalizersDemo.Services;

namespace StringLocalizersDemo
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            #region Services

            services.AddScoped<IAboutService, AboutService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IHelperService, HelperService>();

            // lokalizasiya servisini qoşuruq.
            // lokalizasiya üçün tərcümə fayllarının Resources qovluğunda
            // yerləşdiyini göstəririk
            // xml resources
            //services.AddLocalization(x => x.ResourcesPath = "Resources");
            //
            // json resources
            services.AddSingleton<IStringLocalizerFactory, JsonStringLocalizerFactory>();
            services.AddTransient(typeof(IStringLocalizer<>), typeof(StringLocalizer<>));
            services.Configure<JsonLocalizationOptions>(options =>
            {
                options.ResourcesPath = "JsonResources"; 
            });
            //
            services.AddMvc()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    // about
                    if (context.Request.Query.ContainsKey("about"))
                    {
                        string searchTerm =
                            context.Request.Query["about"];
                        IAboutService service =
                            context.RequestServices.GetService<IAboutService>();

                        string content = service.Reply(searchTerm);
                        await context.Response.WriteAsync(content);
                        return;
                    }

                    // department
                    if (context.Request.Query.ContainsKey("department"))
                    {
                        string department =
                            context.Request.Query["department"];
                        IDepartmentService service =
                            context.RequestServices.GetService<IDepartmentService>();

                        string content = service.GetInfo(department);
                        await context.Response.WriteAsync(content);
                        return;
                    }

                    // help
                    if (context.Request.Query.ContainsKey("help"))
                    {
                        string serviceName =
                            context.Request.Query["help"];
                        IHelperService service =
                            context.RequestServices.GetService<IHelperService>();

                        string content = service.GetHelpFor(serviceName);
                        await context.Response.WriteAsync(content);
                        return;
                    }
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
