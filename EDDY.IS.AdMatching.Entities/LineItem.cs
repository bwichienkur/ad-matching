using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class LineItem
    {
        public LineItem()
        {
            LineItemSubSources = new HashSet<LineItemSubSource>();
        }

        public int LineItemId { get; set; }
        public string Name { get; set; } = null!;
        public int MaxResults { get; set; }
        public int PlacementId { get; set; }
        public int Priority { get; set; }
        public int MaxAdditionalAds { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Placement Placement { get; set; } = null!;
        public virtual ICollection<LineItemSubSource> LineItemSubSources { get; set; }
    }
}
