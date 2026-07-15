using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class CampaignSpend
    {
        public int CampaignSpendId { get; set; }
        public int CampaignId { get; set; }
        public DateTime Date { get; set; }
        public int? ClickCount { get; set; }
        public decimal? Spend { get; set; }
        public int? MonthlyClickCount { get; set; }
        public decimal? MonthlySpend { get; set; }

        public virtual Campaign Campaign { get; set; } = null!;
    }
}
