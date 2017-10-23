using Aiursoft.Pylon.Exceptions;
using Aiursoft.Pylon.Models;
using Aiursoft.Pylon.Models.API.ApiViewModels;
using Aiursoft.Pylon.Models.API.UserAddressModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aiursoft.Pylon.Services.ToAPIServer
{
    public static class UserService
    {
        public async static Task<AiurProtocal> ChangeProfileAsync(string OpenId, string AccessToken, string NewNickName, string NewIconAddress)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "User", "ChangeProfile", new ChangeProfileAddressModel
            {
                AccessToken = AccessToken,
                OpenId = OpenId,
                NewNickName = NewNickName,
                NewIconAddress = NewIconAddress
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<ValidateAccessTokenViewModel>(result);

            if (JResult.code != ErrorType.Success)
                throw new AiurUnexceptedResponse(JResult);
            return JResult;
        }

        public async static Task<AiurProtocal> ChangePasswordAsync(string OpenId, string AccessToken, string OldPassword, string NewPassword)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "User", "ChangePassword", new ChangePasswordAddressModel
            {
                AccessToken = AccessToken,
                OpenId = OpenId,
                OldPassword = OldPassword,
                NewPassword = NewPassword
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<ValidateAccessTokenViewModel>(result);

            if (JResult.code != ErrorType.Success && JResult.code != ErrorType.WrongKey)
                throw new AiurUnexceptedResponse(JResult);
            return JResult;
        }
    }
}
