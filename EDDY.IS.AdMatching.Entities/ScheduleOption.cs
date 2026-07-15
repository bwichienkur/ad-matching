using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class ScheduleOption
    {
        public ScheduleOption()
        {
            CampaignSchedules = new HashSet<CampaignSchedule>();
        }

        public int ScheduleOptionId { get; set; }
        public string Name { get; set; } = null!;
        public string DayOfWeek { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<CampaignSchedule> CampaignSchedules { get; set; }
    }
}
