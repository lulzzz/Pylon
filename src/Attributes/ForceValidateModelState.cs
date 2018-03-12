using Aiursoft.Pylon.Exceptions;
using Aiursoft.Pylon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
            var controller = context.Controller as Controller;
            if (!controller.ModelState.IsValid)
            {
                var list = new List<string>();
                foreach (var value in controller.ModelState)
                {
                    foreach (var error in value.Value.Errors)
                    {
                        list.Add(error.ErrorMessage);
                    }
                }
                var arg = new AiurCollection<string>(list)
                {
                    code = ErrorType.InvalidInput,
                    message = "Your input contains several errors!"
                };
                context.Result = new JsonResult(arg);
            }
            base.OnActionExecuting(context);
        }
    }
}
