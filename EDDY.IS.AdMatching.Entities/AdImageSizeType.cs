using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class AdImageSizeType
    {
        public AdImageSizeType()
        {
            AdImages = new HashSet<AdImage>();
        }

        public int AdImageSizeTypeId { get; set; }
        public string Name { get; set; } = null!;
        public bool IsEnabled { get; set; }

        public virtual ICollection<AdImage> AdImages { get; set; }
    }
}
