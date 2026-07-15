using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class AdProvider
    {
        public AdProvider()
        {
            SubSources = new HashSet<SubSource>();
        }

        public int AdProviderId { get; set; }
        public string Name { get; set; } = null!;
        public string ServiceUrl { get; set; } = null!;
        public string TestServiceUrl { get; set; } = null!;
        public string? RequestAction { get; set; }
        public string? ServiceToken { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public virtual ICollection<SubSource> SubSources { get; set; }
    }
}
