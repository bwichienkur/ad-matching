using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class AdMatchingServiceParameter
    {
        public int AdMatchingServiceParameterId { get; set; }
        public string ParameterName { get; set; } = null!;
        public string ParameterValue { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
