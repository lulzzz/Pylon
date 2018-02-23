using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Aiursoft.Pylon.Models.API.UserAddressModels
{
    public class SetPhoneNumberAddressModel : WithAccessTokenAddressModel
    {
        [Required]
        public string OpenId { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
    }
}
