using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace StringLocalizersDemo.Services
{
    public interface IHelperService
    {
        string GetHelpFor(string serviceName);
    }

    public class HelperService : IHelperService
    {
        private IStringLocalizerFactory _factory;
        public HelperService(IStringLocalizerFactory factory)
        {
            _factory = factory;
        }

        public string GetHelpFor(string serviceName)
        {
            string serviceClassName =
                $"{serviceName}Service";
            Type serviceType = Assembly
                .GetEntryAssembly()
                .ExportedTypes
                .Where(x => x.Name.Equals(serviceClassName,
                    StringComparison.CurrentCultureIgnoreCase))
                .FirstOrDefault();

            if (serviceType == null)
            {
                return $"Help is not available for {serviceName}.";
            }

            IStringLocalizer localizer =
                _factory.Create(serviceType);

            IEnumerable<LocalizedString> resources =
                localizer.GetAllStrings();



            IEnumerable<string> keys =
                resources.Select(x => x.Name);

            
            return $"Available keys {string.Join(",", keys)}";
            // return "resources not found.";
        }
    }
}
