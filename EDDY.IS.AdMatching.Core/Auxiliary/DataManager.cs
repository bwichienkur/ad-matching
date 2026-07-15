using EDDY.IS.AdMatching.Core.Definitions;
using EDDY.IS.AdMatching.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDDY.IS.AdMatching.DataAccess.DataModel;

namespace EDDY.IS.AdMatching.Core.Engine
{
    internal class DataManager
    {
     
        public DataManager()
        {
          
        }

        public DictionaryContainer InitialCall()
        {
            //create the first big object 
            try
            {
                DictionaryContainer response = new DictionaryContainer();
                GlobalDataService db = new GlobalDataService();
                response.campaignSourceList = db.GetAllCampaignSources();
                response.campaignsList = db.GetAllCampaigns();
                response.ads = db.GetAllAds();
                response.adGroupList = db.GetAllAdgroups();
                response.adGroupAdList = db.GetAllAdGroupAds();
                response.clientAdAccounts = db.GetAllAccounts();
                return response;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
           
        }


    }
}
