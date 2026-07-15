using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class SourceGroup
    {
        public SourceGroup()
        {
            SourceGroupSources = new HashSet<SourceGroupSource>();
        }

        public int SourceGroupId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ICollection<SourceGroupSource> SourceGroupSources { get; set; }
    }
}
