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
        public async static Task<AiurProtocal> ChangeProfileAsync(string OpenId, string AccessToken, string NewNickName, string NewIconAddress, string NewBio)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "User", "ChangeProfile", new ChangeProfileAddressModel
            {
                AccessToken = AccessToken,
                OpenId = OpenId,
                NewNickName = NewNickName,
                NewIconAddress = NewIconAddress,
                NewBio = NewBio
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<AiurProtocal>(result);

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
            var JResult = JsonConvert.DeserializeObject<AiurProtocal>(result);

            if (JResult.code != ErrorType.Success && JResult.code != ErrorType.WrongKey)
                throw new AiurUnexceptedResponse(JResult);
            return JResult;
        }
        public async static Task<AiurValue<string>> ViewPhoneNumberAsync(string OpenId, string AccessToken)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "User", "ViewPhoneNumber", new ViewPhoneNumberAddressModel
            {
                AccessToken = AccessToken,
                OpenId = OpenId
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<AiurValue<string>>(result);
            if (JResult.code != ErrorType.Success)
                throw new AiurUnexceptedResponse(JResult);
            return JResult;
        }
        public async static Task<AiurProtocal> SetPhoneNumberAsync(string OpenId, string AccessToken, string PhoneNumber)
        {
            var HTTPContainer = new HTTPService();
            var url = new AiurUrl(Values.ApiServerAddress, "User", "SetPhoneNumber", new SetPhoneNumberAddressModel
            {
                AccessToken = AccessToken,
                OpenId = OpenId,
                Phone = PhoneNumber
            });
            var result = await HTTPContainer.Get(url);
            var JResult = JsonConvert.DeserializeObject<AiurProtocal>(result);
            if (JResult.code != ErrorType.Success)
                throw new AiurUnexceptedResponse(JResult);
            return JResult;
        }
    }
}
