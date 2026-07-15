using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class Campaign
    {
        public Campaign()
        {
        }

        public int CampaignId { get; set; }
        public int ClientAdAccountId { get; set; }
        public int? ProductTypeId { get; set; }
        public int? TimeZoneId { get; set; }
        public int? StopsTimeZoneId { get; set; }
        public bool IsCapped { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }

    }
}
