using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AiursoftBase.Models.Developer
{
    public class DeveloperUser : AiurUserBase
    {
        [InverseProperty(nameof(App.Creater))]
        public virtual List<App> MyApps { get; set; } = new List<App>();
    }
}
