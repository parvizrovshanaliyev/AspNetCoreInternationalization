using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using RequestLocalizationDemo.Enumerations;
using RequestLocalizationDemo.Models;

namespace RequestLocalizationDemo.Controllers
{
    public class EnumerationsController : Controller
    {
        private IStringLocalizer<Gender> _genderLocalizer;

        public EnumerationsController(IStringLocalizer<Gender> genderLocalizer)
        {
            _genderLocalizer = genderLocalizer;
        }

        public IActionResult Genders()
        {
            Console.WriteLine("+++++++++++=====+++++++++++");
            Console.WriteLine($"Current Culture : {CultureInfo.CurrentCulture}");
            Console.WriteLine($"Current UI Culture : {CultureInfo.CurrentUICulture}");
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

            return Ok(selectList);
        }
    }
}