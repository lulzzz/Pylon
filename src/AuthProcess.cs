﻿using Aiursoft.Pylon.Models;
using Aiursoft.Pylon.Models.ForApps.AddressModels;
using Aiursoft.Pylon.Services.ToAPIServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System;
using Aiursoft.Pylon.Models.API.OAuthViewModels;

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
                current = new T();
                current.Update(userinfo);
                var result = await userManager.CreateAsync(current);
                if (!result.Succeeded)
                {
                    throw new InvalidOperationException($"The user info ({userinfo.User.Id}) we get could not register to our database.");
                }
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
