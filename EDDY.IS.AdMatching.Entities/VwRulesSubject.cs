using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwRulesSubject
    {
        public string? DummyId { get; set; }
        public string? Value { get; set; }
        public string Text { get; set; } = null!;
        public string? Key { get; set; }
        public string? GroupKey { get; set; }
        public string GroupName { get; set; } = null!;
    }
}
