using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwCampaignRelationship
    {
        public int ClientAdAccountId { get; set; }
        public string CampaignType { get; set; } = null!;
        public int CampaignId { get; set; }
        public int ParentCampaignId { get; set; }
        public string? Name { get; set; }
        public int? ChildCount { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
