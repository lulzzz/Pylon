using AiursoftBase.Models;
using AiursoftBase.Models.API.OAuthAddressModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase
{
    public class AiurController : Controller
    {
        public IActionResult SignoutRootServer(AiurUrl ToRedirect)
        {
            var r = HttpContext.Request;
            string serverPosition = $"{r.Scheme}://{r.Host}{ToRedirect}";
            var toRedirect = new AiurUrl(Values.ApiServerAddress, "oauth", "UserSignout", new UserSignoutAddressModel
            {
                ToRedirect = serverPosition
            });
            return Redirect(toRedirect.ToString());
        }

        protected JsonResult Protocal(ErrorType errorType, string errorMessage)
        {
            return Json(new AiurProtocal
            {
                code = errorType,
                message = errorMessage
            });
        }
    }
}
