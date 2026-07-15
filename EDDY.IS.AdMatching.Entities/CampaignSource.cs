using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class CampaignSource
    {
        public int CampaignSourceId { get; set; }
        public int CampaignId { get; set; }
        public int SourceId { get; set; }
        public decimal? BidMultiplier { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
    }
}
