using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;

namespace RequestLocalizationDemo.Localization
{
    public class CountryDomainRequestCultureProvider : IRequestCultureProvider
    {
        #region Implementation of IRequestCultureProvider

        public Task<ProviderCultureResult> DetermineProviderCultureResult(
            HttpContext httpContext)
        {
            ProviderCultureResult result = null;

            Dictionary<string, string> map=
                new Dictionary<string, string>
                {
                    {"ba","bs"},
                    {"es","es"},
                    {"fr","fr-FR"},
                    {"en","en-GB"},
                    {"us","en-US"},
                    {"de","de-DE"},
                };

            string domain =
                httpContext.Request.Host.Host.Split('.').Last();

            if (map.ContainsKey(domain))
            {
                result= new ProviderCultureResult(map[domain]);
            }

            return Task.FromResult(result);
        }

        #endregion
    }
}
