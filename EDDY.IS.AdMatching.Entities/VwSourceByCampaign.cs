using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwSourceByCampaignAms
    {
        public long? SourceByCampaignIdAMS { get; set; }
        public int SourceId { get; set; }
        public int CampaignId { get; set; }
        public decimal? BidMultiplier { get; set; }
        public int ClientAdAccountId { get; set; }

    }
}
