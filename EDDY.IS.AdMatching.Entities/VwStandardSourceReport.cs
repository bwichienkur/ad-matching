using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwStandardSourceReport
    {
        public int SourceId { get; set; }
        public string Source { get; set; } = null!;
        public int? SubSourceId { get; set; }
        public string? SubSource { get; set; }
        public int? Uniques { get; set; }
        public int? Impressions { get; set; }
        public double? Searches { get; set; }
        public int? Ips { get; set; }
        public int? Clicks { get; set; }
        public int? Rpc { get; set; }
        public int? Revenue { get; set; }
        public int? Rps { get; set; }
        public int? Spend { get; set; }
    }
}
