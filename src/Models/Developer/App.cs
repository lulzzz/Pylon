﻿using Aiursoft.Pylon.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Aiursoft.Pylon.Models.Developer
{
    public class App
    {
        [Obsolete(error: true, message: "This method is only for framework!")]
        public App() { }
        public App(string seed, string name, string description, Category category, Platform platform)
        {
            this.AppId = (seed + DateTime.Now.ToString()).GetMD5();
            this.AppSecret = (seed + this.AppId + DateTime.Now.ToString() + StringOperation.RandomString(15)).GetMD5();
            this.AppName = name;
            this.AppDescription = description;
            this.AppCategory = category;
            this.AppPlatform = platform;
        }
        public virtual string AppId { get; set; }
        [JsonIgnore]
        public virtual string AppSecret { get; set; }
        public virtual string AppName { get; set; }
        public virtual string AppIconAddress { get; set; }
        public virtual string AppDescription { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy - MM - dd}")]
        public virtual DateTime AppCreateTime { get; set; } = DateTime.Now;

        public virtual Category AppCategory { get; set; }
        public virtual Platform AppPlatform { get; set; }

        public virtual bool EnableOAuth { get; set; } = true;
        /// <summary>
        /// Force the user to input his password even when he is already signed in.
        /// </summary>
        public virtual bool ForceInputPassword { get; set; }
        public virtual bool ForceConfirmation { get; set; } = true;
        public virtual bool DebugMode { get; set; }
        public virtual string AppDomain { get; set; }

        public bool ViewOpenId { get; set; } = true;
        public bool ViewPhoneNumber { get; set; }
        public bool ChangePhoneNumber { get; set; }
        public bool ConfirmEmail { get; set; }
        public bool ChangeBasicInfo { get; set; }
        public bool ChangePassword { get; set; }

        public virtual string CreaterId { get; set; }
        [ForeignKey(nameof(CreaterId))]
        [JsonIgnore]
        public virtual DeveloperUser Creater { get; set; }

        [Url]
        [Display(Name = "Privacy Statement Url")]
        public virtual string PrivacyStatementUrl { get; set; }
        [Url]
        [Display(Name = "License Url")]
        public virtual string LicenseUrl { get; set; }

        [DisplayFormat(DataFormatString = "{0:\\ d' days'\\ h' hours'}")]
        [NotMapped]
        public virtual TimeSpan TimeExists => DateTime.Now - this.AppCreateTime;
        public virtual string ToRegularTime()
        {
            return StringOperation.FormatTimeAgo(TimeExists);
        }
    }
}
