using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RequestLocalizationDemo.Enumerations;
using RequestLocalizationDemo.Localization;
using RequestLocalizationDemo.Models;

namespace RequestLocalizationDemo.Controllers
{
    [MiddlewareFilter(typeof(LocalizationPipeline))]
    public class EnumerationsController : Controller
    {
        private IStringLocalizer<Gender> _genderLocalizer;

        public EnumerationsController(IStringLocalizer<Gender> genderLocalizer)
        {
            _genderLocalizer = genderLocalizer;
        }

        public IActionResult Genders()
        {
           
            List<SelectItem> selectList =
                new List<SelectItem>();
            Array values = Enum.GetValues(typeof(Gender));

            foreach (object value in values)
            {
                selectList.Add(new SelectItem
                {

                    //Name = Enum.GetName(typeof(Gender), value),
                    // localization
                    Name = _genderLocalizer[value.ToString()],
                    Value = (int)value
                });
            }
            IRequestCultureFeature feature =
                HttpContext.Features.Get<IRequestCultureFeature>();
            Console.WriteLine("+++++++++++=====+++++++++++");
            Console.WriteLine($"Current Culture : {feature.RequestCulture.Culture}");
            Console.WriteLine($"Current UI Culture : {feature.RequestCulture.UICulture}");
            Console.WriteLine($"Provider : {feature.Provider}");

            //Console.WriteLine("+++++++++++=====+++++++++++");
            //Console.WriteLine($"Current Culture : {CultureInfo.CurrentCulture}");
            //Console.WriteLine($"Current UI Culture : {CultureInfo.CurrentUICulture}");
            return Ok(selectList);
        }
    }
}