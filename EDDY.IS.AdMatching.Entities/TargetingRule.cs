using EDDY.IS.Common.Dto.RuleEngine;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class TargetingRule
    {
        public int TargetingRuleId { get; set; }
        [JsonIgnore]
        public string? RuleJson { get; set; }

        [NotMapped] 
        public QueryBuilderFilterRule RuleAsQueryBuilderFilterRule { get; set; } = null;

        public bool AllowNullLead { get; set; }
        public int? AdGroupId { get; set; }
        public int? CampaignId { get; set; }
        public bool IsOptimization { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsDynamicBid { get; set; }
        public decimal? DynamicBoostPercent { get; set; }

    }
}