using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class TimeZone
    {
        public TimeZone()
        {
            Campaigns = new HashSet<Campaign>();
        }

        public int TimeZoneId { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}