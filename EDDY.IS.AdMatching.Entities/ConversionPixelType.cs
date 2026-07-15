using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class ConversionPixelType
    {
        public ConversionPixelType()
        {
            ConversionPixels = new HashSet<ConversionPixel>();
        }

        public int ConversionPixelTypeId { get; set; }
        public string? GroupName { get; set; }
        public string TypeName { get; set; } = null!;
        public string CostPerUnit { get; set; } = null!;
        public string PixelTemplate { get; set; } = null!;

        public virtual ICollection<ConversionPixel> ConversionPixels { get; set; }
    }
}
