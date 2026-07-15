using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwUserClaim
    {
        public int IsuserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string? Email { get; set; }
        public string? ClaimValue { get; set; }
        public string Name { get; set; } = null!;
    }
}
