using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDDY.IS.AdMatching.DataAccess.Models;

namespace EDDY.IS.AdMatching.Core.Definitions
{
   public class DictionaryContainer
    {
        public Dictionary<int,CampaignSource> campaignSourceList { get; set; }
        public Dictionary<int, AdGroup> adGroupList { get; set; }
        public Dictionary<int,Campaign> campaignsList { get; set; }
        public Dictionary<int, AdGroupAd> adGroupAdList { get; set; }  
        public Dictionary<int,ClientAdAccount> clientAdAccounts { get; set; } 
        public Dictionary<int,Ad> ads { get; set; }

    }
}
