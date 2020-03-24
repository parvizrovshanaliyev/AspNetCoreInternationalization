using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;

namespace RequestLocalizationDemo.Localization
{
    public class LocalizationPipeline
    {
        public void Conigure(IApplicationBuilder app,
            IOptions<RequestLocalizationOptions> options)
        {
            app.UseRequestLocalization(options.Value);
        }
    }
}
