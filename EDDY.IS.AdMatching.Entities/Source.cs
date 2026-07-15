using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class Source
    {
        public Source()
        {
            CampaignSources = new HashSet<CampaignSource>();
            SourceGroupSources = new HashSet<SourceGroupSource>();
            SourceProductTypes = new HashSet<SourceProductType>();
            SubSources = new HashSet<SubSource>();
        }

        public int SourceId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<CampaignSource> CampaignSources { get; set; }
        public virtual ICollection<SourceGroupSource> SourceGroupSources { get; set; }
        public virtual ICollection<SourceProductType> SourceProductTypes { get; set; }
        public virtual ICollection<SubSource> SubSources { get; set; }
    }
}
