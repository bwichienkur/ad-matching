using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwClientRelationship
    {
        public int ClientRelationshipId { get; set; }
        public string? Name { get; set; }
        public string ClientName { get; set; } = null!;
        public int InstitutionId { get; set; }
        public string? InstitutionName { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public int? UserManagerId { get; set; }
        public string? UserManager { get; set; }
        public bool? IsUserManagerDeleted { get; set; }
    }
}
