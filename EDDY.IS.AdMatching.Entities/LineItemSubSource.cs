using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class LineItemSubSource
    {
        public int LineItemSubSourceId { get; set; }
        public int LineItemId { get; set; }
        public int SubSourceId { get; set; }
        public bool IsEnabled { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedBy { get; set; }

        public virtual LineItem LineItem { get; set; } = null!;
        public virtual SubSource SubSource { get; set; } = null!;
    }
}
