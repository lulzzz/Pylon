using Aiursoft.Pylon.Models;
using Aiursoft.Pylon.Models.ForApps.AddressModels;
using Aiursoft.Pylon.Services.ToAPIServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System;

namespace Aiursoft.Pylon
{
    public class AuthProcess
    {
        public static async Task<T> AuthApp<T>(Controller controller, AuthResultAddressModel model, UserManager<T> userManager, SignInManager<T> signInManager) where T : AiurUserBase, new()
        {
            var openId = await OAuthService.CodeToOpenIdAsync(model.code, await AppsContainer.AccessToken()());
            var userinfo = await OAuthService.OpenIdToUserInfo(AccessToken: await AppsContainer.AccessToken()(), openid: openId.openid);

            var current = await userManager.FindByIdAsync(userinfo.User.Id);
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
                var result = await userManager.CreateAsync(current);
            }
            else
            {
                current.Update(userinfo);
                await userManager.UpdateAsync(current);
            }
            await signInManager.SignInAsync(current, false);
            SetClientLang(controller, userinfo.User.PreferedLanguage);
            return current;
        }

        public static void SetClientLang(Controller controller, string culture)
        {
            controller.HttpContext.Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
        }
    }
}
