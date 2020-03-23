using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json.Linq;

namespace StringLocalizersDemo.Localization
{
    public class JsonstringLocalizer : IStringLocalizer
    {
        #region fields

        private string _resourcesRelativePath;
        private string _typeRelativeNamespace;
        private CultureInfo _uiCultureInfo;
        private JObject _resourcesCache;

        #endregion

        #region ctor

        public JsonstringLocalizer(string resourcesRelativePath, string typeRelativeNamespace,
            CultureInfo uiCultureInfo)
        {
            _resourcesRelativePath = resourcesRelativePath;
            _typeRelativeNamespace = typeRelativeNamespace;
            _uiCultureInfo = uiCultureInfo;
        }

        #endregion



        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            JObject resources = GetResource();
            foreach (var pair in resources)
            {
                yield return new LocalizedString(pair.Key,
                    pair.Value.Value<string>());
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture)
        {
            return new JsonstringLocalizer(_resourcesRelativePath,
                _typeRelativeNamespace,
                culture);
        }

        public LocalizedString this[string name] => this[name, null];


        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                JObject resources = GetResource();

                string value = resources.Value<string>(name);

                bool resourcesNotFound =
                    string.IsNullOrWhiteSpace(value);

                if (resourcesNotFound)
                {
                    value = name;
                }
                else
                {
                    if (arguments != null)
                    {
                        value = string.Format(value, arguments);
                    }
                }

                return new LocalizedString(name,
                    value,
                    resourcesNotFound);
            }
        }

        JObject GetResource()
        {
            if (_resourcesCache == null)
            {
                string tag = _uiCultureInfo.Name;

                string typeRelative =
                    _typeRelativeNamespace.Replace(".", "/");

                string filePath =
                    $"{_resourcesRelativePath}{typeRelative}/{tag}.json";

                string json = File.Exists(filePath) ?
                    File.ReadAllText(filePath, Encoding.Unicode) :
                    "{}";
                _resourcesCache = JObject.Parse(json);
            }

            return _resourcesCache;

        }

    }
}
