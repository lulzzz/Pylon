using System;

namespace Aiursoft.Pylon.Models.Developer
{
    public class Permission
    {
        public virtual int PermissionId { get; set; }
        public virtual string PermissionName { get; set; }
        public virtual bool DeleteAble { get; set; }
    }
}