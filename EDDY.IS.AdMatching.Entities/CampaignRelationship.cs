using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class CampaignRelationship
    {
        public int CampaignRelationshipId { get; set; }
        public int ParentCampaignId { get; set; }
        public int CampaignId { get; set; }
        public bool LinkParentSchedule { get; set; }
        public bool LinkParentSources { get; set; }
        public bool LinkParentRules { get; set; }
        public bool LinkParentOptimizations { get; set; }
        public bool LinkParentDynamicBidVariables { get; set; }
        public bool IsDeleted { get; set; }
    }
}
