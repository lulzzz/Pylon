using AiursoftBase.Exceptions;
using AiursoftBase.Services;
using AiursoftBase.Services.ToAPIServer;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AiursoftBase
{
    public static class Values
    {
        public static string ProjectName = "Aiursoft";
        public static string CorpPhoneNumber = "(+86) 8368-5000";
        public static string Schema = "https";
        public static bool ForceRequestHttps = false;
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
        public static string MessageQueueServerAddress { get; private set; } = Schema + "://messagequeue." + Domain;
        public static string MessageQueueListenAddress { get; private set; } = WSSchema + "://messagequeue." + Domain;
        public static string HrServerAddress { get; private set; } = Schema + "://hr." + Domain;
        public static string WWWServerAddress { get; private set; } = Schema + "://www." + Domain;
        public static string ForumServerAddress { get; private set; } = Schema + "://forum." + Domain;
        public static string KahlaServerAddress { get; private set; } = Schema + "://kahla.server." + Domain;
        public static string KahlaAddress { get; private set; } = Schema + "://kahla" + Domain;
        public static string KahlaAppAddress { get; private set; } = Schema + "://kahla.app." + Domain;
        public static string CurrentAppId { get; private set; } = string.Empty;
        public static string CurrentAppSecret { get; private set; } = string.Empty;
        public static int AppsIconBucketId { get; set; } = 1;
        public static int UsersIconBucketId { get; set; } = 2;
        public static int KahlaFileBucketId { get; set; } = 6;
        public static KeyValuePair<string, string> directShowString => new KeyValuePair<string, string>("show", "direct");

        public static IApplicationBuilder UseAiursoftAuthentication(this IApplicationBuilder app, string appId, string appSecret, string ServerAddress = "")
        {
            if (string.IsNullOrWhiteSpace(appId))
            {
                throw new NotAValidAiurArgumentException(nameof(appId));
            }
            if (string.IsNullOrWhiteSpace(appSecret))
            {
                throw new NotAValidAiurArgumentException(nameof(appSecret));
            }
            if (!string.IsNullOrWhiteSpace(ServerAddress))
            {
                ApiServerAddress = ServerAddress;
            }
            CurrentAppId = appId;
            CurrentAppSecret = appSecret;
            return app;
        }
    }
}
