using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RequestLocalizationTwo.Enumerations;
using RequestLocalizationTwo.Models;

namespace RequestLocalizationTwo.Controllers
{
    //[MiddlewareFilter(typeof(LocalizationPipeline))]
    public class EnumerationsController : Controller
    {
        private readonly IStringLocalizer<Gender> _genderLocalizer;

        public EnumerationsController(IStringLocalizer<Gender> genderLocalizer)
        {
            _genderLocalizer = genderLocalizer;
        }

        public IActionResult Genders()
        {
            Console.WriteLine("+++++++++++=====+++++++++++");
            Console.WriteLine($"Current Culture : {CultureInfo.CurrentCulture}");
            Console.WriteLine($"Current UI Culture : {CultureInfo.CurrentUICulture}");

            Array values = Enum.GetValues(typeof(Gender));

            List<SelectItem> selectList = (from object value in values
                select new SelectItem
                {
                    //Name = Enum.GetName(typeof(Gender), value),
                    // localization
                    Name = _genderLocalizer[value.ToString()],
                    Value = (int) value
                }).ToList();

            //IRequestCultureFeature feature =
            //    HttpContext.Features.Get<IRequestCultureFeature>();
            //Console.WriteLine("+++++++++++=====+++++++++++");
            //Console.WriteLine($"Current Culture : {feature.RequestCulture.Culture}");
            //Console.WriteLine($"Current UI Culture : {feature.RequestCulture.UICulture}");
            //Console.WriteLine($"Provider : {feature.Provider}");
            
            return Ok(selectList);
        }
    }
}