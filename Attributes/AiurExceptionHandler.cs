using AiursoftBase.Exceptions;
using AiursoftBase.Models;
using AiursoftBase.Services;
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
                case NotAiurSignedInException exp:
                    var r = context.HttpContext.Request;
                    string serverPosition = $"{r.Scheme}://{r.Host}";
                    string url = UrlConverter.UrlWithAuth(serverPosition, exp.SignInRedirectPath, exp.JustTry);
                    context.ExceptionHandled = true;
                    context.HttpContext.Response.Redirect(url);
                    break;
                case AiurUnexceptedResponse exp:
                    Redirect(exp.Response.code, exp.Response.message);
                    break;
                case ModelStateNotValidException exp:
                    Redirect(ErrorType.InvalidInput, "Input not valid!");
                    break;
                case WrongAccessTokenException exp:
                    Redirect(ErrorType.Unauthorized, "We can not validate your access token!");
                    break;
                case TimeOutAccessTokenException exp:
                    Redirect(ErrorType.Unauthorized, "Your access token is already Timeout!");
                    break;
                case Exception exp:
                    throw exp;
                    //Redirect(ErrorType.UnknownError, exp.Message);
                default:
                    break;
            }
            void Redirect(ErrorType errorType, string message)
            {
                var arg = new AiurProtocal
                {
                    code = ErrorType.Unauthorized,
                    message = message
                };
                context.ExceptionHandled = true;
                context.HttpContext.Response.Redirect(new AiurUrl(string.Empty, "api", "exception", arg).ToString());
            }
        }
    }
}
