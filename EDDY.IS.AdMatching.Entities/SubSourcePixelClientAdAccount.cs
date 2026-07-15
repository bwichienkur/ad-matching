using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class SubSourcePixelClientAdAccount
    {
        public int SubSourcePixelClientAdAccountId { get; set; }
        public int ClientAdAccountId { get; set; }
        public int SubSourceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }

        public virtual ClientAdAccount ClientAdAccount { get; set; } = null!;
        public virtual SubSource SubSource { get; set; } = null!;
    }
}
