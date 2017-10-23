using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aiursoft.Pylon.Attributes
{
    public class AiurAllowTargetOrigin: ActionFilterAttribute
    {
        public string Origin { get; set; }
        public AiurAllowTargetOrigin(string origin)
        {
            Origin = origin;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Origin", Origin);
            context.HttpContext.Response.Headers.Add("Access-Control-Allow-Credentials","true");
        }
    }
}
