using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwAdGroup
    {
        public int AdGroupId { get; set; }
        public string? Name { get; set; }
        public int ClientAdAccountId { get; set; }
        public int? CampaignLevelId { get; set; }
        public int CampaignId { get; set; }
        public string? DestinationUrl { get; set; }
        public string CampaignName { get; set; } = null!;
        public int? AdCount { get; set; }
        public decimal? Bid { get; set; }
        public int Impressions { get; set; }
        public decimal AveragePosition { get; set; }
        public int Clicks { get; set; }
        public decimal Ctr { get; set; }
        public decimal Spend { get; set; }
        public decimal Cpc { get; set; }
        public int UipixelCount { get; set; }
        public decimal Cruip { get; set; }
        public decimal Cpuip { get; set; }
        public int TotalLeads { get; set; }
        public decimal Crtl { get; set; }
        public decimal Cptl { get; set; }
        public decimal TotalLeadsRev { get; set; }
        public decimal Tlroas { get; set; }
        public int GoodLead { get; set; }
        public decimal Crgl { get; set; }
        public decimal Cpgl { get; set; }
        public int Apps { get; set; }
        public bool IsEnabled { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
