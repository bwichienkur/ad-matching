using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class SourceProductType
    {
        public int SourceProductTypeId { get; set; }
        public int SourceId { get; set; }
        public int ProductTypeId { get; set; }
        public bool IsEnabled { get; set; }

    }
}
