using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Aiursoft.Pylon.Models.Developer
{
    public class AppPermission
    {
        public virtual int AppPermissionId { get; set; }

        public virtual int PermissionId { get; set; }
        [ForeignKey(nameof(PermissionId))]
        public virtual Permission Permission { get; set; }

        public string AppId { get; set; }
        [ForeignKey(nameof(AppId))]
        [JsonIgnore]
        public virtual App App { get; set; }
    }
}
