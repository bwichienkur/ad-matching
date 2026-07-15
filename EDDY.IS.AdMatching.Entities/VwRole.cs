using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwRole
    {
        public int IsuserId { get; set; }
        public string RoleId { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Id { get; set; } = null!;
    }
}
