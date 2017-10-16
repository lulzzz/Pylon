using AiursoftBase.Exceptions;
using AiursoftBase.Models;
using AiursoftBase.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase.Attributes
{
    /// <summary>
    /// This will stop current action with any Aiursoft exceptions.
    /// </summary>
    public class AiurExceptionHandlerAttribute : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            base.OnException(context);
            switch (context.Exception)
            {
                // When Aiur http service get unexcepted response from aiur services.
                case AiurUnexceptedResponse exp:
                    ReturnJson(exp.Response.code, exp.Response.message);
                    break;
            }
            void ReturnJson(ErrorType errorType, string message)
            {
                var arg = new AiurProtocal
                {
                    code = errorType,
                    message = message
                };
                context.ExceptionHandled = true;
                context.Result = new JsonResult(arg);
            }
        }
    }
}
