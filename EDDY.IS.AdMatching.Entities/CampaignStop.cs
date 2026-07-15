using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class CampaignStop
    {
        public int CampaignStopId { get; set; }
        public int CampaignId { get; set; }
        public DateTime BeginStop { get; set; }
        public DateTime EndStop { get; set; }
        public string TimeZone { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }

    }
}
