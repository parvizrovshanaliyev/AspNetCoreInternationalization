using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace ImplementingLocalization.Controllers
{
    public class CultureController : Controller
    {
        [HttpPost]
        public IActionResult Set(string uiCulture, string returnUrl)
        {
            // current culture
            IRequestCultureFeature feature =
                HttpContext.Features.Get<IRequestCultureFeature>();
            // new request
            RequestCulture requestCulture =
                new RequestCulture(feature.RequestCulture.Culture,
                    new CultureInfo(uiCulture));
            // create cookie
            string cookieValue =
                CookieRequestCultureProvider.MakeCookieValue(requestCulture);
            // cookie name
            string cookieName =
                CookieRequestCultureProvider.DefaultCookieName;
            //
            Response.Cookies.Append(cookieName, cookieValue);

            return LocalRedirect(returnUrl);
        }
    }
}