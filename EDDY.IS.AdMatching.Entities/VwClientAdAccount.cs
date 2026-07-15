using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class VwClientAdAccount
    {
        public int ClientAdAccountId { get; set; }
        public string ClientAccountName { get; set; } = null!;
        public string InstitutionAlias { get; set; } = null!;
        public int ClientRelationshipId { get; set; }
        public int? ClientServicesRepId { get; set; }
        public string? ClientServicesRep { get; set; }
        public int ClicksRepId { get; set; }
        public string? ClicksRep { get; set; }
        public decimal? Allocation { get; set; }
        public decimal? Spend { get; set; }
        public decimal? Fill { get; set; }
        public string Status { get; set; } = null!;
        public bool? IsStopped { get; set; }
        public DateTime? BeginStop { get; set; }
        public DateTime? EndStop { get; set; }
    }
}
