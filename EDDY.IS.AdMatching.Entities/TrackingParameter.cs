using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class TrackingParameter
    {
        public string Name { get; set; } = null!;
        public string Source { get; set; } = null!;
        public string MapTo { get; set; } = null!;
        public bool IncludeSource { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
    }
}
