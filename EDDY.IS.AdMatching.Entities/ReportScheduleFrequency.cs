using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class ReportScheduleFrequency
    {
        public ReportScheduleFrequency()
        {
            ReportSchedules = new HashSet<ReportSchedule>();
        }

        public int ReportScheduleFrequencyId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }

        public virtual ICollection<ReportSchedule> ReportSchedules { get; set; }
    }
}
