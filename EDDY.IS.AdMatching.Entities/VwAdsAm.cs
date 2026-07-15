namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwAdsAm
    {
        public string Id { get; set; }
        public int ClientAdAccountId { get; set; }
        public int CampaignId { get; set; }
        public int AdGroupId { get; set; }
        public int AdId { get; set; }
        public Guid AdGuid { get; set; }
        public int ClientRelationshipId { get; set; }
        public string Name { get; set; }
        public string? BackupUrl { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public string? DisplayUrl { get; set; }
        public int AdImageSizeTypeId { get; set; }
        public string ImageUrl { get; set; }
        public string AdGroupName { get; set; }
        public string CampaignName { get; set; }
        public decimal? Cpc { get; set; }
        public decimal RankMultiplier { get; set; }
        public decimal SchoolMultiplier { get; set; }
        public int? CampaignLevelId { get; set; }
        public string ClickUrl { get; set; }
        public string? PopularPrograms { get; set; }
        public int? ProductTypeId { get; set; }
        public Guid ClientToken { get; set; }
        public string ClientAccountName { get; set; }
        public bool IsStaticAd { get; set; }
    }
}
