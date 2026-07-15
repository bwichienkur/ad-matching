using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class Placement
    {
        public Placement()
        {
            LineItems = new HashSet<LineItem>();
        }

        public int PlacementId { get; set; }
        public string Name { get; set; } = null!;
        public Guid Token { get; set; }
        public bool? Dedupe { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
