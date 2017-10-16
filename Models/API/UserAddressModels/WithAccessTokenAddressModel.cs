using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AiursoftBase.Models.API.UserAddressModels
{
    public class WithAccessTokenAddressModel
    {
        [Required]
        public string AccessToken { get; set; }
    }
}
