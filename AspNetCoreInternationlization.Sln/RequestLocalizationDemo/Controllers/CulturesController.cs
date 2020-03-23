using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace RequestLocalizationDemo.Controllers
{
    public class CulturesController : Controller
    {
        public IActionResult Set(string uiCulture)
        {
            RequestCulture requestCulture =
                new RequestCulture(uiCulture);

            string coockieValue =
                CookieRequestCultureProvider.MakeCookieValue(requestCulture);

            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                coockieValue);
            return Ok();
        }
    }
}