using Aiursoft.Pylon.Exceptions;
using Aiursoft.Pylon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aiursoft.Pylon.Attributes
{
    public class ForceValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var controller = context.Controller as Controller;
            if (!controller.ModelState.IsValid)
            {
                var arg = new AiurProtocal
                {
                    code = ErrorType.InvalidInput,
                    message = "Input not valid!"
                };
                context.Result = new JsonResult(arg);
            }
        }
    }
}
