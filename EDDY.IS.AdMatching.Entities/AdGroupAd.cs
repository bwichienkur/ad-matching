namespace EDDY.IS.AdMatching.Entities
{
    public partial class AdGroupAd
    {
        public int AdGroupAdId { get; set; }
        public int AdGroupId { get; set; }
        public int AdId { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }

    }
}
