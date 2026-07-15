using System;
using System.Collections.Generic;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class ClientAdAccount
    {
        public ClientAdAccount()
        {
        }

        public int ClientAdAccountId { get; set; }
        public string InstitutionAlias { get; set; }
        public string Status { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public bool IsDeleted { get; set; }

    }
}
