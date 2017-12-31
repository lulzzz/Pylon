using System;
using Aiursoft.Pylon.Models.API.OAuthViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;

namespace Aiursoft.Pylon.Models
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
            this.Email = model.User.Email;
            this.Bio = model.User.Bio;
            this.EmailConfirmed = model.User.EmailConfirmed;
        }

        [JsonProperty]
        public override string Id { get => base.Id; set => base.Id = value; }
        [JsonProperty]
        public override string Email { get => base.Email; set => base.Email = value; }
        [JsonProperty]
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }
        [JsonProperty]
        public virtual string Bio { get; set; }
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
