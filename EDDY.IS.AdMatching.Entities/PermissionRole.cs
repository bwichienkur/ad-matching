using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class PermissionRole
    {
        public int PermissionId { get; set; }
        public int RoleId { get; set; }

        public virtual Permission Permission { get; set; } = null!;
    }
}
