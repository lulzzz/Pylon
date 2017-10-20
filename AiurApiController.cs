using AiursoftBase.Models;
using AiursoftBase.Models.API.OAuthAddressModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.Globalization;

namespace AiursoftBase
{
    public class AiurApiController : AiurController
    {
        public IActionResult SetSonLang(string culture, string returnUrl = "/")
        {
            try
            {
                Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            }
            catch (CultureNotFoundException)
            {
                return Json(new AiurProtocal { message = "Not a language.", code = ErrorType.InvalidInput });
            }
            return Redirect(returnUrl);
        }
    }
}
