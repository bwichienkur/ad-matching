using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwRulesHighSchoolGpa
    {
        public string Value { get; set; } = null!;
        public string Text { get; set; } = null!;
        public string? Key { get; set; }
        public int? Order { get; set; }
    }
}
