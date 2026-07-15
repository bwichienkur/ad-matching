using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwAdGroupAd
    {
        public int AdId { get; set; }
        public string Name { get; set; } = null!;
        public int ClientAdAccountId { get; set; }
        public string ClientAccountName { get; set; } = null!;
        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = null!;
        public string? Assignments { get; set; }
        public string Headline { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string DisplayUrl { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public bool IsEnabled { get; set; }
        public string? CreatedDate { get; set; }
        public string? Status { get; set; }
        public int AdGroupId { get; set; }
        public string? AdGroupName { get; set; }
    }
}
