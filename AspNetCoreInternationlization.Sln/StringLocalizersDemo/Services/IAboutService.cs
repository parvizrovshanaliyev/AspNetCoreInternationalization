using System;
using Microsoft.Extensions.Localization;

namespace StringLocalizersDemo.Services
{
    public interface IAboutService
    {
        string Reply(string searchTerm);
    }

    public class AboutService : IAboutService
    {
        private readonly IStringLocalizer<IAboutService> _localizer;

        public AboutService(IStringLocalizer<IAboutService> localizer)
        {
            _localizer = localizer;
        }
        public string Reply(string searchTerm)
        {
            LocalizedString resource = _localizer[searchTerm];

            if (resource.ResourceNotFound)
            {
                return _localizer["help"];
            }
            return resource;
        }
    }
}
