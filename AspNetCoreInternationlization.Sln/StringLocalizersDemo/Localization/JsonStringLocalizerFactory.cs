using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace StringLocalizersDemo.Localization
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        #region fields
        private  string _resourcesRelativePath;
        #endregion

        #region ctor
        public JsonStringLocalizerFactory(IOptions<JsonLocalizationOptions> options)
        {
            _resourcesRelativePath =
                options.Value?.ResourcesPath ?? string.Empty;
        }
        #endregion

        public IStringLocalizer Create(Type resourceSource)
        {
            TypeInfo typeInfo =
                resourceSource.GetTypeInfo();

            AssemblyName assemblyName =
                new AssemblyName(typeInfo.Assembly.FullName);

            string baseNamespace =
                assemblyName.Name;

            string typeRelativeNamespace =
                typeInfo.FullName?.Substring(baseNamespace.Length);

            return new JsonStringLocalizer(_resourcesRelativePath,
                typeRelativeNamespace,
                CultureInfo.CurrentUICulture);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            throw new NotImplementedException();
        }
    }
}
