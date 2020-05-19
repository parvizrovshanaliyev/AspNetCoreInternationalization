using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace RequestLocalizationTwo.Controllers
{
    public class CulturesController : Controller
    {
        public IActionResult Set(string uiCulture)
        {
            RequestCulture requestCulture =
                new RequestCulture(uiCulture);

            string cookieValue =
                CookieRequestCultureProvider.MakeCookieValue(requestCulture);

            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                cookieValue);

            return Ok();
        }
    }
}