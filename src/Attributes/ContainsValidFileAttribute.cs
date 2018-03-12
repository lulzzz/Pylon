using Aiursoft.Pylon.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aiursoft.Pylon.Attributes
{
    public class ContainsValidFileAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            void ReturnError()
            {
                var arg = new AiurProtocal
                {
                    code = ErrorType.InvalidInput,
                    message = "You did not upload a valid file using HTTP Post."
                };
                context.Result = new JsonResult(arg);
                return;
            }
            base.OnActionExecuting(context);
            if(context.HttpContext.Request.Method.ToUpper().Trim()!="POST")
            {
                ReturnError();
                return;
            }
            if (context.HttpContext.Request.Form.Files.Count < 1)
            {
                ReturnError();
                return;
            }
            var file = context.HttpContext.Request.Form.Files.First();
            if (file == null)
            {
                ReturnError();
                return;
            }
            if (file.Length < 1 || file.Length > 1024 * 1024 * 1024)
            {
                ReturnError();
                return;
            }
        }
    }
}
