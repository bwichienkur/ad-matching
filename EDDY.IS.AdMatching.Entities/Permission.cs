using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class Permission
    {
        public Permission()
        {
            PermissionRoles = new HashSet<PermissionRole>();
        }

        public int PermissionId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<PermissionRole> PermissionRoles { get; set; }
    }
}
