using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class ConversionPixel
    {
        public int ConversionPixelId { get; set; }
        public int ClientAdAccountId { get; set; }
        public int ConversionPixelTypeId { get; set; }
        public string PixelName { get; set; } = null!;
        public string Accronym { get; set; } = null!;
        public string Url { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual ClientAdAccount ClientAdAccount { get; set; } = null!;
        public virtual ConversionPixelType ConversionPixelType { get; set; } = null!;
    }
}
