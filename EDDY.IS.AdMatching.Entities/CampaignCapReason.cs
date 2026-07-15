using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class CampaignCapReason
    {
        public CampaignCapReason()
        {
            Campaigns = new HashSet<Campaign>();
            ClientAdAccountBudgets = new HashSet<ClientAdAccountBudget>();
        }

        public int CampaignCapReasonId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;

        public virtual ICollection<Campaign> Campaigns { get; set; }
        public virtual ICollection<ClientAdAccountBudget> ClientAdAccountBudgets { get; set; }
    }
}
