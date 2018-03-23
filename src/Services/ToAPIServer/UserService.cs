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
        public async static Task<AiurProtocal> ChangeProfileAsync(string openId, string accessToken, string newNickName, string newIconAddress, string newBio)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "User", "ChangeProfile", new ChangeProfileAddressModel
            {
                AccessToken = accessToken,
                OpenId = openId,
                NewNickName = newNickName,
                NewIconAddress = newIconAddress,
                NewBio = newBio
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<AiurProtocal>(result);

            if (JResult.code != ErrorType.Success)
                throw new AiurUnexceptedResponse(JResult);
            return JResult;
        }

        public async static Task<AiurProtocal> ChangePasswordAsync(string openId, string accessToken, string oldPassword, string newPassword)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "User", "ChangePassword", new ChangePasswordAddressModel
            {
                AccessToken = accessToken,
                OpenId = openId,
                OldPassword = oldPassword,
                NewPassword = newPassword
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<AiurProtocal>(result);

            if (JResult.code != ErrorType.Success && JResult.code != ErrorType.WrongKey)
                throw new AiurUnexceptedResponse(JResult);
            return JResult;
        }
        public async static Task<AiurValue<string>> ViewPhoneNumberAsync(string openId, string accessToken)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "User", "ViewPhoneNumber", new ViewPhoneNumberAddressModel
            {
                AccessToken = accessToken,
                OpenId = openId
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<AiurValue<string>>(result);
            if (JResult.code != ErrorType.Success)
                throw new AiurUnexceptedResponse(JResult);
            return JResult;
        }

        public async static Task<AiurProtocal> SetPhoneNumberAsync(string penId, string accessToken, string phoneNumber)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "User", "SetPhoneNumber", new SetPhoneNumberAddressModel
            {
                AccessToken = accessToken,
                OpenId = penId,
                Phone = phoneNumber
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<AiurProtocal>(result);
            if (JResult.code != ErrorType.Success)
                throw new AiurUnexceptedResponse(JResult);
            return JResult;
        }

        public async static Task<AiurValue<int>> VerifyEmailAsync(string openId, string accessToken, bool verified)
        {
            var httpContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "User", "VerifyEmail", new { });
            var form = new AiurUrl(string.Empty, new VerifyEmailAddressModel
            {
                OpenId = openId,
                AccessToken = accessToken,
                Verified = verified
            });
            var result = await httpContainer.Post(url, form);
            var jResult = JsonConvert.DeserializeObject<AiurValue<int>>(result);
            if (jResult.code != ErrorType.Success)
                throw new AiurUnexceptedResponse(jResult);
            return jResult;
        }
    }
}
