using System.ComponentModel.DataAnnotations.Schema;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class SlimAd
    {
        public string Id { get; set; }
        public int ClientAdAccountId { get; set; }
        public int CampaignId { get; set; }
        public int AdGroupId { get; set; }
        public int AdId { get; set; }
        public int SourceId { get; set; }
        public decimal SourceBid { get; set; }

        [NotMapped]
        public string AdKey => $"{this.ClientAdAccountId}:{this.CampaignId}:{this.AdGroupId}:{this.AdId}";
    }
}
