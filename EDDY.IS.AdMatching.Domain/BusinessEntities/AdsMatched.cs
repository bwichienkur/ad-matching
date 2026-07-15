namespace EDDY.IS.AdMatching.Domain.BusinessEntities
{
    public class AdsMatched
    {
        public int AdId { get; set; }
        public string? AdName { get; set; }
        public string? AdDescription { get; set; }
        public string? AdHeader { get; set; }
        public string? AdClickUrl { get; set; }
        public string? AdDisplayUrl { get; set; }
        public int CampaignId { get; set; }
        public int VendorId { get; set; }
        public string? VendorName { get; set; }
        public string? InstitutionName { get; set; }
        public string? OriginalImageURL { get; set; }
        public string? SmallImageURL { get; set; }
        public string? MediumImageURL { get; set; }
        public string? LargeImageURL { get; set; }
        public double RealCPC { get; set; }
        public double BoostedCPC { get; set; }
        public int LineItemId { get; set; }
        public string? LineItemName { get; set; }
        public decimal RankMultiplier { get; set; }
        public decimal SchoolMultiplier { get; set; }
        public decimal CPC { get; set; }
        public decimal SourceBid { get; set; }
        public string CampaignName { get; set; }
        public string AdGroupName { get; set; }
        public int AdGroupId { get; set; }
        public int ClientAdAccountId { get; set; }
        public int ClientRelationshipId { get; set; }
        public string? PopularPrograms { get; set; }
        public int ProductTypeId { get; set; }
        public Guid ClientToken { get; set; }
        public List<decimal> DynamicBoostPercent { get; set; } = new List<decimal>();  

    }
}
