using AiursoftBase.Exceptions;
using AiursoftBase.Models;
using AiursoftBase.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AiursoftBase.Attributes
{
    /// <summary>
    /// Request the signed in token or throw a NotAiurSignedInException
    /// </summary>
    public class AiurForceAuth : ActionFilterAttribute
    {
        private string _preferController { get; set; } = null;
        private string _preferAction { get; set; } = null;
        private bool? _justTry { get; set; } = false;
        private bool _PreferPageSet { get; set; } = false;


        private bool _hasAPreferPage => (true
            && !string.IsNullOrEmpty(_preferController)
            && !string.IsNullOrEmpty(_preferAction))
            || _PreferPageSet;

        private string _preferPage
        {
            get
            {
                if (string.IsNullOrEmpty(_preferController) && string.IsNullOrEmpty(_preferAction))
                {
                    return "/";
                }
                return new AiurUrl(string.Empty, _preferController, _preferAction, new { }).ToString();
            }
        }

        public AiurForceAuth()
        {

        }

        public AiurForceAuth(string preferController, string preferAction, bool justTry)
        {
            this._preferController = preferController;
            this._preferAction = preferAction;
            this._justTry = justTry ? true as bool? : null;
            this._PreferPageSet = true;
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var controller = context.Controller as Controller;
            var show = context.HttpContext.Request.Query[Values.directShowString.Key];
            //Not signed in
            if (!controller.User.Identity.IsAuthenticated)
            {
                if (_hasAPreferPage)
                {
                    if (show == Values.directShowString.Value && _justTry == true)
                    {
                        return;
                    }
                    context.Result = _Redirect(context, _preferPage, _justTry);
                }
                else
                {
                    context.Result = _Redirect(context, controller.Request.Path.Value, null);
                }
            }
            //Signed in, let him go to prefered page directly.
            else if (_hasAPreferPage && !controller.Request.Path.Value.ToLower().StartsWith(_preferPage.ToLower()))
            {
                context.HttpContext.Response.Redirect(_preferPage);
            }
            //Signed in and no prefered page, Display current page.
            else
            {
                return;
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            throw new System.NotImplementedException();
        }

        private RedirectResult _Redirect(ActionExecutingContext context, string page, bool? justTry)
        {
            var r = context.HttpContext.Request;
            string serverPosition = $"{r.Scheme}://{r.Host}";
            string url = UrlConverter.UrlWithAuth(serverPosition, page, justTry);
            return new RedirectResult(url);
        }
    }
}
