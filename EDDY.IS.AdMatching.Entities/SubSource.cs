using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class SubSource
    {
        public SubSource()
        {
            LineItemSubSources = new HashSet<LineItemSubSource>();
            SubSourcePixelClientAdAccounts = new HashSet<SubSourcePixelClientAdAccount>();
        }

        public int SubSourceId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal? BaseCpc { get; set; }
        public int SourceId { get; set; }
        public int AdProviderId { get; set; }
        public string? ImpressionPixel { get; set; }
        public string? ClickPixel { get; set; }
        public bool? FirePixelForAllClients { get; set; }
        public bool? LogVendorServiceCall { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime UpdatedDate { get; set; }

        public virtual AdProvider AdProvider { get; set; } = null!;
        public virtual Source Source { get; set; } = null!;
        public virtual ICollection<LineItemSubSource> LineItemSubSources { get; set; }
        public virtual ICollection<SubSourcePixelClientAdAccount> SubSourcePixelClientAdAccounts { get; set; }
    }
}
