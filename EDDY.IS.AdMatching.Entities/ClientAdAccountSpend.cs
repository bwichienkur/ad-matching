using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class ClientAdAccountSpend
    {
        public int ClientAdAccountSpendId { get; set; }
        public int ClientAdAccountId { get; set; }
        public int ProductTypeId { get; set; }
        public DateTime Date { get; set; }
        public int? ClickCount { get; set; }
        public decimal? Spend { get; set; }
        public int? MonthlyClickCount { get; set; }
        public decimal? MonthlySpend { get; set; }

        public virtual ClientAdAccount ClientAdAccount { get; set; } = null!;
        public virtual ProductType ProductType { get; set; } = null!;
    }
}
