using AiursoftBase.Exceptions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace AiursoftBase.Attributes
{
    public class AiurForceWebSocket : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if (!context.HttpContext.WebSockets.IsWebSocketRequest)
            {
                throw new WrongProtocolException();
            }
        }
    }
}
