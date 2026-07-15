using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwSubSource
    {
        public int SubSourceId { get; set; }
        public string SubSourceName { get; set; } = null!;
        public int AdProviderId { get; set; }
        public string AdProviderName { get; set; } = null!;
        public double? RevShare { get; set; }
        public int SourceId { get; set; }
        public string SourceName { get; set; } = null!;
        public int Searches { get; set; }
        public decimal? Revenue { get; set; }
        public bool IsEnabled { get; set; }
    }
}
