using Aiursoft.Pylon.Exceptions;
using Aiursoft.Pylon.Services;
using Aiursoft.Pylon.Services.ToAPIServer;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Aiursoft.Pylon.Middlewares;

namespace Aiursoft.Pylon
{
    public static class Extends
    {
        public static string CurrentAppId { get; private set; } = string.Empty;
        public static string CurrentAppSecret { get; private set; } = string.Empty;
        public static CultureInfo[] GetSupportedLanguages()
        {
            var SupportedCultures = new CultureInfo[]
            {
                new CultureInfo("en"),
                new CultureInfo("zh")
            };
            return SupportedCultures;
        }
        public static IApplicationBuilder UseAiursoftAuthenticationFromConfiguration(this IApplicationBuilder app, IConfiguration configuration, string appName = "api")
        {
            var AppId = configuration[$"{appName}AppId"];
            var AppSecret = configuration[$"{appName}AppSecret"];
            Console.WriteLine($"Got AppId={AppId}, AppSecret={AppSecret}");
            if (string.IsNullOrWhiteSpace(AppId) || string.IsNullOrWhiteSpace(AppSecret))
            {
                throw new InvalidOperationException("Did not get appId and appSecret from configuration!");
            }
            return app.UseAiursoftAuthentication(AppId, AppSecret);
        }
        public static IApplicationBuilder UseAiursoftSupportedCultures(this IApplicationBuilder app, string defaultLanguage = "en")
        {
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultLanguage),
                SupportedCultures = GetSupportedLanguages(),
                SupportedUICultures = GetSupportedLanguages()
            });
            return app;
        }
        public static IApplicationBuilder UseAiursoftAuthentication(this IApplicationBuilder app, string appId, string appSecret)
        {
            if (string.IsNullOrWhiteSpace(appId))
            {
                throw new InvalidOperationException(nameof(appId));
            }
            if (string.IsNullOrWhiteSpace(appSecret))
            {
                throw new InvalidOperationException(nameof(appSecret));
            }
            CurrentAppId = appId;
            CurrentAppSecret = appSecret;
            return app;
        }
        public static IApplicationBuilder UseEnforceHttps(this IApplicationBuilder app)
        {
            return app.UseMiddleware<EnforceHttpsMiddleware>();
        }
        public static IApplicationBuilder UseHandleKahlaOptions(this IApplicationBuilder app)
        {
            return app.UseMiddleware<HandleKahlaOptionsMiddleware>();
        }
        public static IApplicationBuilder UseLanguageSwitcher(this IApplicationBuilder app)
        {
            return app.UseMiddleware<SwitchLanguageMiddleware>();
        }
        public static IServiceCollection ConfigureLargeFileUploadable(this IServiceCollection services)
        {
            return services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = int.MaxValue;
            });
        }
    }
}
