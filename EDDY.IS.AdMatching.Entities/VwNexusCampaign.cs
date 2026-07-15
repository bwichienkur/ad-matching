using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwNexusCampaign
    {
        public long CampaignId { get; set; }
        public Guid TrackId { get; set; }
        public int ChannelId { get; set; }
        public string CampaignName { get; set; } = null!;
        public int? SubChannelId { get; set; }
        public int? MarketingUnitId { get; set; }
        public int HasDirectory { get; set; }
        public string? DirectoryName { get; set; }
    }
}
