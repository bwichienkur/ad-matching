using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwUser
    {
        public string Id { get; set; } = null!;
        public int IsuserId { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public byte[]? ProfilePicture { get; set; }
        public string? Email { get; set; }
        public string UserName { get; set; } = null!;
    }
}
