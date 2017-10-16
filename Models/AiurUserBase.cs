using System;
using AiursoftBase.Models.API.OAuthViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace AiursoftBase.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class AiurUserBase : IdentityUser
    {
        public AiurUserBase() { }
        public AiurUserBase(UserInfoViewModel model)
        {
            this.Update(model);
        }

        public void Update(UserInfoViewModel model)
        {
            this.NickName = model.User.NickName;
            this.Sex = model.User.Sex;
            this.PreferedLanguage = model.User.PreferedLanguage;
            this.HeadImgUrl = model.User.HeadImgUrl;
            this.AccountCreateTime = model.User.AccountCreateTime;
        }

        [JsonProperty]
        public override string Id { get => base.Id; set => base.Id = value; }
        [JsonProperty]
        public virtual string NickName { get; set; }
        [JsonProperty]
        public virtual string Sex { get; set; }
        [JsonProperty]
        public virtual string HeadImgUrl { get; set; } = $@"{Values.DeveloperServerAddress}/images/appdefaulticon.png";
        [JsonProperty]
        public virtual string PreferedLanguage { get; set; } = "UnSet";
        [JsonProperty]
        public virtual DateTime AccountCreateTime { get; set; } = DateTime.Now;
    }
}
