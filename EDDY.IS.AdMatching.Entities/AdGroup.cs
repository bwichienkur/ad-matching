namespace EDDY.IS.AdMatching.Entities
{
    public partial class AdGroup
    {
        public AdGroup()
        {
        }

        public int AdGroupId { get; set; }
        public int CampaignId { get; set; }
        public string? Name { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
      
    }
}
