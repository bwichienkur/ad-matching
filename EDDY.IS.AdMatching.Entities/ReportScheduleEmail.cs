using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class ReportScheduleEmail
    {
        public int ReportScheduleEmailId { get; set; }
        public int ReportScheduleId { get; set; }
        public string EmailAddress { get; set; } = null!;

        public virtual ReportSchedule ReportSchedule { get; set; } = null!;
    }
}
