using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwPlacement
    {
        public int PlacementId { get; set; }
        public string PlacementName { get; set; } = null!;
        public string PlacementCreative { get; set; } = null!;
        public string PlacementSearches { get; set; } = null!;
        public DateTime PlacementCreatedDate { get; set; }
        public int? LineItemId { get; set; }
        public string? LineItemName { get; set; }
        public string LineItemCreative { get; set; } = null!;
        public string LineItemSearches { get; set; } = null!;
        public DateTime? LineItemCreatedDate { get; set; }
        public bool IsEnabled { get; set; }
        public bool? LineItemIsEnabled { get; set; }
    }
}
