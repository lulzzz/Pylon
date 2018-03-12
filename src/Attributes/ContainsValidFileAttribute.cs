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
        private string _ErrorRedirect { get; set; }
        public ContainsValidFileAttribute(string ErrorRedirect)
        {
            _ErrorRedirect = ErrorRedirect;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            // Not a post method
            if(context.HttpContext.Request.Method.ToUpper().Trim()!="POST")
            {
                context.Result = new RedirectResult(_ErrorRedirect);
                return;
            }
            // No file
            if (context.HttpContext.Request.Form.Files.Count < 1)
            {
                context.Result = new RedirectResult(_ErrorRedirect);
                return;
            }
            var file = context.HttpContext.Request.Form.Files.First();
            // File is null
            if (file == null)
            {
                context.Result = new RedirectResult(_ErrorRedirect);
                return;
            }
            // Not in valid size
            if (file.Length < 1 || file.Length > Values.MaxFileSize)
            {
                context.Result = new RedirectResult(_ErrorRedirect);
                return;
            }
        }
    }
}
