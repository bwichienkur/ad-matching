using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class ClientAdAccountStop
    {
        public int ClientAdAccountStopId { get; set; }
        public int ClientAdAccountId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? StopDate { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? StopTime { get; set; }
        public DateTime BeginStop { get; set; }
        public DateTime EndStop { get; set; }
        public string TimeZone { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }

    }
}
