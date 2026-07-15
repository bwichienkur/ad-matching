using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class ReportSchedule
    {
        public ReportSchedule()
        {
            ReportScheduleEmails = new HashSet<ReportScheduleEmail>();
        }

        public int ReportScheduleId { get; set; }
        public string Name { get; set; } = null!;
        public int ReportScheduleFrequencyId { get; set; }
        public TimeSpan DeliveryTime { get; set; }
        public int? WeeklyDeliveryDay { get; set; }
        public int? MonthlyDeliveryDay { get; set; }
        public DateTime? CustomDeliveryDate { get; set; }
        public int ReportTypeId { get; set; }
        public string? ReportDetailsJson { get; set; }
        public bool IsDelivered { get; set; }
        public DateTime? LastDeliveryDate { get; set; }
        public string? ContactEmail { get; set; }
        public bool? IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ReportScheduleFrequency ReportScheduleFrequency { get; set; } = null!;
        public virtual ICollection<ReportScheduleEmail> ReportScheduleEmails { get; set; }
    }
}
