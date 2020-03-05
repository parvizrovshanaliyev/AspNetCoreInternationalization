using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;

namespace StringLocalizersDemo.Services
{
    public interface IDepartmentService
    {
        string GetInfo(string name);
    }

    public class DepartmentService : IDepartmentService
    {
        IStringLocalizer _localizer;

        public DepartmentService(IStringLocalizer<DepartmentService> localizer)
        {
            _localizer = localizer;
        }

        public string GetInfo(string name)
        {
            LocalizedString value = _localizer[name];

            if (value.ResourceNotFound)
            {
                return _localizer["help"];
            }
            return value;
        }
    }
}
