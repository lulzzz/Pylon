using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiursoftBase.Attributes
{
    public class AiurNoCache : ActionFilterAttribute
    {
        public AiurNoCache()
        {
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            context.HttpContext.Response.Headers.Add("Cache-Control", "no-cache");
            context.HttpContext.Response.Headers.Add("Expires", "-1");
        }
    }
}
