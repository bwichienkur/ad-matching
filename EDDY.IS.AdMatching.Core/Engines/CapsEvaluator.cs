using System;
using System.Linq;
using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Domain.Services.Interfaces;
using EDDY.IS.AdMatching.Entities;

namespace EDDY.IS.AdMatching.Core.Engines
{
    internal class CapsEvaluator
    {
        private IDataManager _dataManager;
        private DictionaryContainer _container;

        public CapsEvaluator()
        {

        }

        public FilteredDictionary Process(FilteredContainerDictionary filtered, FilteredDictionary mainDictionaryEvaluated)
        {
            foreach (var itemCampaign in mainDictionaryEvaluated.CampaignsList)
            {
                if (IsCapped(itemCampaign.Value, filtered)) // Caps Evaluator
                {
                    mainDictionaryEvaluated.CampaignsList.Remove(itemCampaign.Key);
                }
            }

            return mainDictionaryEvaluated;
        }

        private Boolean IsCapped(Campaign campaign, FilteredContainerDictionary filtered)
        {
            var isCapped = false;

            var fatherCampaign = filtered.CampaignRelationshipList.FirstOrDefault(x =>
                x.Value.CampaignId == campaign.CampaignId &&
                x.Value.IsDeleted == false);
            var fatherCampaignId = fatherCampaign.Value != null ? fatherCampaign.Value.ParentCampaignId : 0;

            if (fatherCampaignId > 0)
            {
                if (filtered.CampaignsList != null)
                {
                    var campaignsThatMatches = filtered.CampaignsList.Where(x => x.Value.CampaignId == fatherCampaignId);
                    if (campaignsThatMatches.Any())
                    {
                        var campaignThatMatches = campaignsThatMatches.First();
                        isCapped = campaignThatMatches.Value.IsCapped;
                    }
                }
            }
            else
            {
                isCapped = campaign.IsCapped;
            }

            return isCapped;
        }
    }
}