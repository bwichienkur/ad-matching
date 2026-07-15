using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwCampaign
    {
        public int CampaignId { get; set; }
        public int ClientAdAccountId { get; set; }
        public int? ParentCampaignId { get; set; }
        public string Name { get; set; } = null!;
        public int? ProductTypeId { get; set; }
        public string? ProductName { get; set; }
        public int? ProductClickRepId { get; set; }
        public string? AllocationType { get; set; }
        public decimal? Allocation { get; set; }
        public decimal DailyCap { get; set; }
        public decimal Fill { get; set; }
        public int ClickCount { get; set; }
        public decimal Spend { get; set; }
        public decimal? Cpc { get; set; }
        public int? LeadCount { get; set; }
        public decimal? Cvr { get; set; }
        public bool? Cpa { get; set; }
        public decimal? CpaValue { get; set; }
        public bool? IsStopped { get; set; }
        public DateTime? BeginStop { get; set; }
        public DateTime? EndStop { get; set; }
        public int? ChildCount { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsCapped { get; set; }
        public DateTime? CappedOutAt { get; set; }
    }
}
