using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class ClientAdAccountDefaultParameter
    {
        public int ClientAdAccountDefaultParametersId { get; set; }
        public string Category { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Macro { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? Notes { get; set; }
        public bool IsEnabled { get; set; }
    }
}
