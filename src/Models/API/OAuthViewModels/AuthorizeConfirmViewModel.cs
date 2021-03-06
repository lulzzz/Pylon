﻿using Aiursoft.Pylon.Models.Developer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Aiursoft.Pylon.Models.API.OAuthViewModels
{
    public class AuthorizeConfirmViewModel : IOAuthInfo
    {
        public virtual string AppName { get; set; }
        public virtual string UserNickName { get; set; }

        [Required]
        [Url]
        public string ToRedirect { get; set; }

        public string State { get; set; }
        public string AppId { get; set; }
        public string Scope { get; set; }
        public string ResponseType { get; set; }
        public string Email { get; set; }
        public string UserIcon { get; set; }

        public bool ViewOpenId { get; set; }
        public bool ViewPhoneNumber { get; set; }
        public bool ChangePhoneNumber { get; set; }
        public bool ConfirmEmail { get; set; }
        public bool ChangeBasicInfo { get; set; }
        public bool ChangePassword { get; set; }

        public string GetRegexRedirectUrl()
        {
            var url = new Uri(ToRedirect);
            string result = $@"{url.Scheme}://{url.Host}:{url.Port}{url.AbsolutePath}";
            return result;
        }
        public string GetRedirectRoot()
        {
            var url = new Uri(ToRedirect);
            return $@"{url.Scheme}://{url.Host}/";
        }
    }
}
