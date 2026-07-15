using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class CampaignSchedule
    {
        public int CampaignScheduleId { get; set; }
        public int CampaignId { get; set; }
        public int ScheduleOptionId { get; set; }
        public int? Bid { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public int? DisableAds { get; set; }
        public virtual ScheduleOption ScheduleOption { get; set; } = null!;
    }
}
