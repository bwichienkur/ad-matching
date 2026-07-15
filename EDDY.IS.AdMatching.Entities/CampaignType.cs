using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class CampaignType
    {
        public CampaignType()
        {
            Campaigns = new HashSet<Campaign>();
        }

        public int CampaignTypeId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}
