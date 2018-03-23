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
            Update(model);
        }

        public void Update(UserInfoViewModel model)
        {
            NickName = model.User.NickName;
            Sex = model.User.Sex;
            HeadImgUrl = model.User.HeadImgUrl;
            PreferedLanguage = model.User.PreferedLanguage;
            AccountCreateTime = model.User.AccountCreateTime;
            Email = model.User.Email;
            Bio = model.User.Bio;
            EmailConfirmed = model.User.EmailConfirmed;
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
