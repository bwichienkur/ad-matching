using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class CampaignLevel
    {
        public CampaignLevel()
        {
            Campaigns = new HashSet<Campaign>();
        }

        public int CampaignLevelId { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsEnabled { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
