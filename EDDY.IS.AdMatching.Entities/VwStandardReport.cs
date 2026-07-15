namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwStandardReport
    {
        public string AdName { get; set; } = null!;
        public string? AdGroupName { get; set; }
        public string? CampaignName { get; set; }
        public string ClientAccountName { get; set; } = null!;
        public int? Clicks { get; set; }
        public double? Ctr { get; set; }
        public decimal? Spend { get; set; }
        public decimal? Cpc { get; set; }
        public int? UipixelCount { get; set; }
        public double? Cruip { get; set; }
        public decimal? Cpu { get; set; }
    }
}
