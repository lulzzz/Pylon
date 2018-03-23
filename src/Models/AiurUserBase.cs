using System;
using Aiursoft.Pylon.Models.API.OAuthViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

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
            this.HeadImgUrl = model.User.HeadImgUrl;
            this.PreferedLanguage = model.User.PreferedLanguage;
            this.AccountCreateTime = model.User.AccountCreateTime;
            this.Email = model.User.Email;
            this.Bio = model.User.Bio;
            this.EmailConfirmed = model.User.EmailConfirmed;
            this.PhoneNumber = model.User.PhoneNumber;
        }

        [JsonProperty]
        public override string Id { get => base.Id; set => base.Id = value; }
        [JsonProperty]
        public override string Email { get => base.Email; set => base.Email = value; }
        [JsonProperty]
        public override bool EmailConfirmed { get => base.EmailConfirmed; set => base.EmailConfirmed = value; }
        [NotMapped]
        public override bool PhoneNumberConfirmed { get => !string.IsNullOrEmpty(PhoneNumber); }
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
