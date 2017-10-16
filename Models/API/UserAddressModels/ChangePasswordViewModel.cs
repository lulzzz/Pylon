using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AiursoftBase.Models.API.UserAddressModels
{
    public class ChangePasswordAddressModel : WithAccessTokenAddressModel
    {
        [Required]
        public string OpenId { get; set; }
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
}
