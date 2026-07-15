using EDDY.IS.AdMatching.Core.Auxiliary;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Entities;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDDY.IS.AdMatching.Core.Engines
{
    public class ResponseBuilder
    {
        private readonly DebugLogger _debugLogger;

        public ResponseBuilder(DebugLogger debugLogger)
        {
            _debugLogger = debugLogger;
        }

        /// <summary>
        /// This method builds the final object that will contain the services response
        /// </summary>
        /// <returns></returns>
        public List<AdsMatched> GetAdsMatchedResponse(
            FilteredDictionary MainDictionaryEvaluated, 
            Dictionary<string, VwAdsAm> ads)
        {
            ConcurrentBag<AdsMatched> adsMatched = new();

            //In order to obtain the final list of ads
            var finalAds = from p in MainDictionaryEvaluated.SlimAdsDictionary
                           where MainDictionaryEvaluated.CampaignsList.Any
                           (bc => bc.Value.CampaignId == p.Value.CampaignId)
                           select p.Value;

            Parallel.ForEach(finalAds, adItem =>
            {
                if (ads.TryGetValue(adItem.AdKey, out VwAdsAm ad))
                    if (!ad.IsStaticAd) adsMatched.Add(MapAdMatched(ad, adItem)); // prevent show static ads in regular ad stack
            });
            
            return adsMatched.ToList();
        }

        public List<AdsMatched> GetStaticAdMatchResponse(FilteredDictionary MainDictionaryEvaluated, VwAdsAm staticAd, Dictionary<string, VwAdsAm> ads) {
            
            var response = new ConcurrentBag<AdsMatched>();
            var finalAds = MainDictionaryEvaluated.SlimAdsDictionary
                            .Where(a => 
                                MainDictionaryEvaluated.CampaignsList.Any(bc => bc.Value.CampaignId == a.Value.CampaignId))
                            .Select(a => a.Value)
                            .ToList();
            
            if (finalAds != null && finalAds.Count() > 0) {
                Parallel.ForEach(finalAds, adItem =>
                {
                    if (ads.TryGetValue(adItem.AdKey, out VwAdsAm ad))
                        if (ad.IsStaticAd) response.Add(MapAdMatched(ad, adItem)); // prevent show regular ads in static
                });

                return response.ToList();
            }
            
            response.Add(MapAdMatched(new VwAdsAm() { 
                AdId = -1,
                ClientAdAccountId = staticAd.ClientAdAccountId,
                ClientAccountName = staticAd.ClientAccountName,
                CampaignId = staticAd.CampaignId,
                CampaignName = staticAd.CampaignName,
                ClientToken = staticAd.ClientToken,
                Headline = "StaticFallback",
                CampaignLevelId = (int)CampaignLevelId.Ad,
                ClickUrl = staticAd.BackupUrl,
                Cpc = 0.01m
            }, new SlimAd()));

            return response.ToList();
        }

        private AdsMatched MapAdMatched(VwAdsAm ad, SlimAd slimAd) => new AdsMatched()
        {
            AdClickUrl = ad.ClickUrl,
            AdDescription = ad.Description ?? "",
            AdDisplayUrl = ad.DisplayUrl ?? "",
            AdHeader = ad.Headline ?? "",
            AdId = ad.AdId,
            AdName = ad.Name ?? "",
            CampaignId = ad.CampaignId,
            BoostedCPC = 0,
            InstitutionName = ad.ClientAccountName ?? "",
            LargeImageURL = ad.ImageUrl ?? "",
            MediumImageURL = ad.ImageUrl ?? "",
            SmallImageURL = ad.ImageUrl ?? "",
            OriginalImageURL = ad.ImageUrl ?? "",
            RealCPC = 0,
            VendorId = 1,
            RankMultiplier = ad.RankMultiplier,
            SchoolMultiplier = ad.SchoolMultiplier,
            CPC = ad.Cpc ?? 0,
            CampaignName = ad.CampaignName ?? "",
            AdGroupName = ad.AdGroupName ?? "",
            AdGroupId = ad.AdGroupId,
            ClientAdAccountId = ad.ClientAdAccountId,
            ClientRelationshipId = ad.ClientRelationshipId,
            PopularPrograms = ad.PopularPrograms ?? "",
            ClientToken = ad.ClientToken,
            ProductTypeId = ad.ProductTypeId ?? 0,
            SourceBid = slimAd.SourceBid,
        };
        
    }
}
