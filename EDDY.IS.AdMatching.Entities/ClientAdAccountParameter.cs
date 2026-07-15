using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class ClientAdAccountParameter
    {
        public int ClientAdAccountParametersId { get; set; }
        public int ClientAdAccountId { get; set; }
        public string ParameterName { get; set; } = null!;
        public string ParameterValue { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsRollingDates { get; set; }

    }
}
