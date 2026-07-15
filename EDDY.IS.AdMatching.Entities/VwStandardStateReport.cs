using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwStandardStateReport
    {
        public int StateId { get; set; }
        public string? State { get; set; }
        public int? Clicks { get; set; }
        public double? Ctr { get; set; }
        public decimal? Spend { get; set; }
        public decimal? Cpc { get; set; }
        public int? UipixelCount { get; set; }
        public double? Cruip { get; set; }
        public decimal? Cpu { get; set; }
    }
}
