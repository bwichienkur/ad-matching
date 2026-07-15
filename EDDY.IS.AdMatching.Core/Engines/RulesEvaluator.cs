using EDDY.IS.AdMatching.DataAccess.Models;
using EDDY.IS.AdMatching.Core.Abstract;
using EDDY.IS.AdMatching.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Core.Definitions;
using EDDY.IS.AdMatching.DataAccess.DataModel;

namespace EDDY.IS.AdMatching.Core.Engine
{
   public class RulesEvaluator:IRulesEngine
    {

   
        DictionaryContainer allData;

        public RulesEvaluator()
        {
            InitialObjects();
        }

        public void InitialObjects()
        {
            //start first object
            DataManager Ini = new DataManager();
            DictionaryContainer allDataInit = new DictionaryContainer();
            try
            {
                allDataInit = Ini.InitialCall();
                this.allData = allDataInit;
            }
            catch (Exception ex)
            {
                CoreException.LogException(EDDY.IS.Base.ISApplication.AdMatching, ex, "");
            }

        }

        public Dictionary<int,Ad> GetAdsBySourceId(int sourceId)
        {
            try
            {
                Dictionary<int, Ad> ListOfAds = new Dictionary<int, Ad>();  
                ListOfAds = FilterAdsBySourceId(sourceId).ads;
                return ListOfAds;
            }
            catch (Exception ex)
            {
                CoreException.LogException(EDDY.IS.Base.ISApplication.AdMatching, ex, "");
                return null;

            }
            
        }

        public DictionaryContainer FilterAdsBySourceId(int sourceId)
        {
            DictionaryContainer dictionaryContainerFiltered = new DictionaryContainer();
            //filter campaigns by sourceid
            dictionaryContainerFiltered.campaignsList = FilterCampaigns(allData.campaignSourceList, sourceId);

            //filter adgroups by campaings
            dictionaryContainerFiltered.adGroupList = FilterAdGroups(dictionaryContainerFiltered.campaignsList);

            dictionaryContainerFiltered.ads = FilterAds(dictionaryContainerFiltered.adGroupList);

            return dictionaryContainerFiltered;
        }

        public Dictionary<int, Campaign> FilterCampaigns(Dictionary<int, CampaignSource> campaignSources, int sourceId)
        {
            Dictionary<int, Campaign> FilteredCampaigns = new Dictionary<int, Campaign>();
            var filtered =  from p in allData.campaignsList
                               where campaignSources.Any
                               (bc => bc.Value.CampaignId == p.Value.CampaignId
                               && bc.Key == sourceId)
                               select p;

            FilteredCampaigns = filtered.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return FilteredCampaigns;
        }

        public Dictionary<int, AdGroup> FilterAdGroups(Dictionary<int, Campaign> CampaignList)
        {
            Dictionary<int, AdGroup> FilteredAdGroups = new Dictionary<int, AdGroup>();
            var filtered = from p in allData.adGroupList
                           where CampaignList.Any
                           (bc => bc.Value.CampaignId == p.Value.CampaignId)
                           select p;

            FilteredAdGroups = filtered.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            return FilteredAdGroups;
        }

        public Dictionary<int, Ad> FilterAds(Dictionary<int, AdGroup> adGroups)
        {
            Dictionary<int, Ad> FilteredAdGroups = new Dictionary<int, Ad>();
            var filtered = from p in allData.ads
                           where adGroups.Any
                           (bc => bc.Value.AdGroupId == p.Value.AdId)
                           select p;

            FilteredAdGroups = filtered.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);


            return FilteredAdGroups;
        }

    }
}
