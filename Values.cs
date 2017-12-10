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

namespace Aiursoft.Pylon
{
    public static class Values
    {
        public static string ProjectName = "Aiursoft";
        public static string CorpPhoneNumber = "(+86) 8368-5000";
        public static string Schema = "https";
        public static bool ForceRequestHttps = true;
        public static string WSSchema = "wss";
        public static bool SupportHttps => Schema.ToLower() == "https";
        public static string Domain { get; private set; } = "aiursoft.com";
        public static string Empty { get; private set; } = Schema + "://" + Domain;
        public static string DeveloperServerAddress { get; private set; } = Schema + "://developer." + Domain;
        public static string ApiServerAddress { get; private set; } = Schema + "://api." + Domain;
        public static string AccountServerAddress { get; private set; } = Schema + "://account." + Domain;
        public static string OssServerAddress { get; private set; } = Schema + "://oss." + Domain;
        public static string CdnServerAddress { get; private set; } = Schema + "://cdn." + Domain;
        public static string WikiServerAddress { get; private set; } = Schema + "://wiki." + Domain;
        public static string StargateServerAddress { get; private set; } = Schema + "://stargate." + Domain;
        public static string StargateListenAddress { get; private set; } = WSSchema + "://stargate." + Domain;
        public static string HrServerAddress { get; private set; } = Schema + "://hr." + Domain;
        public static string WWWServerAddress { get; private set; } = Schema + "://www." + Domain;
        public static string ForumServerAddress { get; private set; } = Schema + "://forum." + Domain;
        public static string KahlaServerAddress { get; private set; } = Schema + "://kahla.server." + Domain;
        public static string KahlaAddress { get; private set; } = Schema + "://kahla" + Domain;
        public static string CompanyAddress { get; private set; } = Schema + "://company." + Domain;
        public static string KahlaAppAddress { get; private set; } = Schema + "://kahla.app." + Domain;
        public static string CurrentAppId { get; private set; } = string.Empty;
        public static string CurrentAppSecret { get; private set; } = string.Empty;
        public static int AppsIconBucketId { get; set; } = 1;
        public static int UsersIconBucketId { get; set; } = 2;
        // public static int KahlaFileBucketId { get; set; } = 6;
        public static KeyValuePair<string, string> directShowString => new KeyValuePair<string, string>("show", "direct");

        public static CultureInfo[] GetSupportedLanguages()
        {
            var SupportedCultures = new CultureInfo[]
            {
                new CultureInfo("en"),
                new CultureInfo("zh")
            };
            return SupportedCultures;
        }
        public static IApplicationBuilder UseAiursoftAuthenticationFromConfiguration(this IApplicationBuilder app, IConfiguration configuration,string appName = "api")
        {
            var AppId = configuration[$"{appName}AppId"];
            var AppSecret = configuration[$"{appName}AppSecret"];
            Console.WriteLine($"Got AppId={AppId}, AppSecret={AppSecret}");
            if (string.IsNullOrWhiteSpace(AppId) || string.IsNullOrWhiteSpace(AppSecret))
            {
                throw new InvalidOperationException("Did not get appId and appSecret from configuration!");
            }
            return app.UseAiursoftAuthentication(AppId,AppSecret);
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
    }
}
