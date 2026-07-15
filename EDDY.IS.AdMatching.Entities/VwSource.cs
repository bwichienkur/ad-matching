using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwSource
    {
        public int SourceId { get; set; }
        public string SourceName { get; set; } = null!;
        public string? Description { get; set; }
        public string? SourceGroups { get; set; }
        public string? SubSources { get; set; }
        public string? Products { get; set; }
        public int Searches { get; set; }
        public decimal? Revenue { get; set; }
        public string Status { get; set; } = null!;
        public string ClientAccess { get; set; } = null!;
        public string InstitutionInclusionExclusion { get; set; } = null!;
        public bool IsEnabled { get; set; }
    }
}
