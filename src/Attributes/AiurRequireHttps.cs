using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Aiursoft.Pylon.Attributes
{
    public class AiurRequireHttps : RequireHttpsAttribute
    {
        public override void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (Values.SupportHttps && Values.ForceRequestHttps)
            {
                filterContext.HttpContext.Response.Headers.Add("Strict-Transport-Security","max-age=15552001; includeSubDomains; preload");
                base.OnAuthorization(filterContext);
            }
        }
    }
}
