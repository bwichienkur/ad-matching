using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Domain.BusinessEntities;

namespace EDDY.IS.AdMatching.Core.Engines
{
    public class AdSortingEngine
    {
        private readonly DebugLogger _debugLogger;

        private readonly Random _rng = new(DateTime.Now.Millisecond);

        public AdSortingEngine(DebugLogger debugLogger)
        {
            _debugLogger = debugLogger;
        }

        public List<AdsMatched> Process(List<AdsMatched> finalAds, FilteredDictionary mainDictionaryEvaluated,
            int maxAds, bool isStatic)
        {
            _debugLogger.Log($"AdSortingEngine.Process: FinalAds before CalculateCPCBoost:", finalAds?.Count);
            finalAds = CalculateCpcBoost(finalAds, mainDictionaryEvaluated);
            _debugLogger.Log($"AdSortingEngine.Process: FinalAds before OrderByDescending:", finalAds?.Count);
            finalAds = finalAds
                .Where(x => x.RealCPC > 0)
                .OrderByDescending(x => x.BoostedCPC).ToList();
            _debugLogger.Log($"AdSortingEngine.Process: FinalAds before FilterAdsByAccount:", finalAds?.Count);
            finalAds = FilterAdsByAccount(finalAds);
            _debugLogger.Log($"AdSortingEngine.Process: FinalAds before FinalRandomSelect:", finalAds?.Count);
            finalAds = FinalRandomSelect(finalAds, maxAds);
            _debugLogger.Log($"AdSortingEngine.Process: FinalAds after all processing:", finalAds?.Count);

            return finalAds;
        }

        public List<AdsMatched> CalculateCpcBoost(List<AdsMatched> finalAds,
            FilteredDictionary mainDictionaryEvaluated)
        {
            Parallel.ForEach(finalAds, ad =>
            {

                var dynamicBidVariable = 0.0m;
                var dynamicBidSourceVariable = ad == null ? 0 : (decimal)ad.SourceBid;

                if (ad != null)
                { 
                    foreach (var dynamicBid in ad.DynamicBoostPercent)
                    {
                        dynamicBidVariable += (ad.CPC * (dynamicBid / 100.0m));
                    }
                }

                var realCpc = ad.CPC + (dynamicBidVariable) + (ad.CPC * (dynamicBidSourceVariable / 100.0m));
                //from SD-7821: BoostedCPC = RealCPC + (RealCPC * (Ad.RankMultiplier/100)) + (RealCPC * (Ad.SchoolMultiplier/100))                
                var rankingValue =  realCpc + realCpc * (ad.RankMultiplier / 100.0m) +
                                    realCpc * (ad.SchoolMultiplier / 100.0m);
                foreach (var campaingScheduler in mainDictionaryEvaluated.CampaignSchedulerList.Where(x =>
                             x.Value.CampaignId == ad.CampaignId && x.Value.IsEnabled && !x.Value.IsDeleted))
                {
                    var campaignSchedulerBid = campaingScheduler.Value != null ? (int) campaingScheduler.Value.Bid : 0;
                    realCpc = realCpc + realCpc * (campaignSchedulerBid / 100.0m);
                    rankingValue = rankingValue + rankingValue * (campaignSchedulerBid / 100.0m);
                }

                ad.BoostedCPC = Convert.ToDouble(rankingValue);
                ad.RealCPC = Convert.ToDouble(realCpc);
            });

            return finalAds;
        }

        /// <summary>
        /// Returns only one ad per AdClientAccount
        /// </summary>
        /// <param name="finalAds"></param>
        /// <returns></returns>
        public List<AdsMatched> FilterAdsByAccount(List<AdsMatched> finalAds)
        {
            var filteredAds = new List<AdsMatched>();
            var adClientAccountIdHashSet = new HashSet<int>();

            var orderedAdsByBoostedCpc = finalAds.OrderByDescending(x => x.BoostedCPC);
            foreach (var ad in orderedAdsByBoostedCpc)
            {
                var randomizedAds = new List<AdsMatched>();
                randomizedAds.AddRange(finalAds.Where(x =>
                    x.AdGroupId == ad.AdGroupId && Math.Abs(x.BoostedCPC - ad.BoostedCPC) < 0.1));
                Shuffle(randomizedAds);
                var randomAdFromGroup = randomizedAds[0];

                if (adClientAccountIdHashSet.Contains(randomAdFromGroup.ClientAdAccountId))
                {
                    _debugLogger.Log(
                        "***AdSortingEngine.FilterAdsByAccount: Because it already exists in  ExistingAdClientAccounts:",
                        string.Join(",", adClientAccountIdHashSet));
                    _debugLogger.Log("****AdSortingEngine.FilterAdsByAccount: Excluding Ad", ad);
                    continue;
                }

                _debugLogger.Log("+++AdSortingEngine.FilterAdsByAccount: Adding Ad:", ad);
                adClientAccountIdHashSet.Add(randomAdFromGroup.ClientAdAccountId);
                filteredAds.Add(randomAdFromGroup);

            }

            _debugLogger.Log(
                "****AdSortingEngine.FilterAdsByAccount: List of AdClientAccounts at end of FilterAdsByAccount:",
                string.Join(",", adClientAccountIdHashSet));

            return filteredAds;
        }

        public List<AdsMatched> FinalRandomSelect(List<AdsMatched> finalAds, int maxAds)
        {
            var filteredAds = new List<AdsMatched>();
            var randomizedAds = new List<AdsMatched>();
            var distinctBoostedCpc = new List<double>();
            var totalAds = 0;

            distinctBoostedCpc = finalAds.Select(x => x.BoostedCPC).OrderByDescending(x => x).Distinct().ToList();

            var terminate = false;
            var aCounter = 0;
            var remainingAds = 0;

            for (var i = 0; i < distinctBoostedCpc.Count; i++)
            {
                if (terminate)
                    break;

                if (totalAds < maxAds)
                {
                    randomizedAds.AddRange(finalAds.Where(x => x.BoostedCPC == distinctBoostedCpc.ElementAt(aCounter)));
                    Shuffle(randomizedAds);

                    filteredAds.AddRange(randomizedAds);
                    randomizedAds.Clear();

                    remainingAds = finalAds.Count - filteredAds.Count;
                    totalAds = filteredAds.Count;
                    aCounter++;
                }
                else if (remainingAds == 0 || totalAds >= maxAds)
                {
                    terminate = true;
                }
            }

            if (filteredAds.Count > maxAds)
                filteredAds.RemoveRange(maxAds - 1, filteredAds.Count - maxAds);

            return filteredAds;
        }

        public void Shuffle<T>(IList<T> list)
        {
            var n = list.Count;
            while (n > 1)
            {
                n--;
                var k = _rng.Next(n + 1);
                (list[k], list[n]) = (list[n], list[k]);
            }
        }
    }
}