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
                base.OnAuthorization(filterContext);
            }
        }
    }
}
