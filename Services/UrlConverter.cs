using AiursoftBase.Models;
using AiursoftBase.Models.API.OAuthAddressModels;

namespace AiursoftBase.Services
{

    public static class UrlConverter
    {
        private static AiurUrl _GenerateAuthUrl(AiurUrl destination, string state, bool? justTry)
        {
            var url = new AiurUrl(Values.ApiServerAddress, "oauth", "authorize", new AuthorizeAddressModel
            {
                appid = Values.CurrentAppId,
                redirect_uri = destination.ToString(),
                response_type = "code",
                scope = "snsapi_base",
                state = state,
                tryAutho = justTry
            });
            return url;
        }

        public static string UrlWithAuth(string serverRoot, string path, bool? justTry)
        {
            return _GenerateAuthUrl(new AiurUrl(serverRoot, "Auth", "AuthResult", new { }), path, justTry).ToString();
        }
    }
}
