using AiursoftBase.Models;
using AiursoftBase.Models.ForApps.AddressModels;
using AiursoftBase.Services.ToAPIServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AiursoftBase
{
    public class AuthProcess
    {
        public static async Task<T> AuthApp<T>(Controller _controller, AuthResultAddressModel model, UserManager<T> _userManager, SignInManager<T> _signInManager) where T : AiurUserBase, new()
        {
            var openId = await OAuthService.CodeToOpenIdAsync(model.code, await AppsContainer.AccessToken()());
            var userinfo = await OAuthService.OpenIdToUserInfo(AccessToken: await AppsContainer.AccessToken()(), openid: openId.openid);

            var current = await _userManager.FindByIdAsync(userinfo.User.Id);
            if (current == null)
            {
                current = new T()
                {
                    Id = userinfo.User.Id,
                    NickName = userinfo.User.NickName,
                    Sex = userinfo.User.Sex,
                    HeadImgUrl = userinfo.User.HeadImgUrl,
                    UserName = userinfo.User.Id,
                    PreferedLanguage = userinfo.User.PreferedLanguage,
                    AccountCreateTime = userinfo.User.AccountCreateTime
                };
                var result = await _userManager.CreateAsync(current);
            }
            else
            {
                current.Update(userinfo);
                await _userManager.UpdateAsync(current);
            }
            await _signInManager.SignInAsync(current, false);
            return current;
        }
    }
}
