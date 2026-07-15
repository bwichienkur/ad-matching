namespace EDDY.IS.AdMatching.Entities
{
    public partial class CampaignDestinationUrl
    {
        public int CampaignDestinationId { get; set; }
        public int CampaignLevelId { get; set; }
        public string DestinationUrl { get; set; } = null!;
        public bool? IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
