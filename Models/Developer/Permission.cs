using System;

namespace AiursoftBase.Models.Developer
{
    public class Permission
    {
        public virtual int PermissionId { get; set; }
        public virtual string PermissionName { get; set; }
        public virtual bool DeleteAble { get; set; }
    }
}