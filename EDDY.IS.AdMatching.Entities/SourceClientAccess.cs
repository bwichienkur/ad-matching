using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDDY.IS.AdMatching.Entities
{
    public partial class SourceClientAccess
    {
        public int SourceClientAccessId { get; set; }

        public int SourceId { get; set; }

        public int ClientAdAccountId { get; set; }

        public bool IsEnabled { get; set; }

        public bool IsDeleted { get; set; }

        public int CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime UpdatedDate { get; set; }


    }
}
