using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class SourceGroupSource
    {
        public int SourceGroupSourceId { get; set; }
        public int SourceGroupId { get; set; }
        public int SourceId { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual Source Source { get; set; } = null!;
        public virtual SourceGroup SourceGroup { get; set; } = null!;
    }
}
