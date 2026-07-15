using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class ClientAdAccountBudget
    {
        public int ClientAdAccountBudgetId { get; set; }
        public int ClientAdAccountId { get; set; }
        public int ProductTypeId { get; set; }
        public string? AllocationType { get; set; }
        public decimal? NextMonthAllocation { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsCapped { get; set; }
        public int? CampaignCapReasonId { get; set; }
        public decimal? InitialMonthAllocation { get; set; }

    }
}
