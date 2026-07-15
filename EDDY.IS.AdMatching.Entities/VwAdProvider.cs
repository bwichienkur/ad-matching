using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwAdProvider
    {
        public int AdProviderId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsEnabled { get; set; }
    }
}
