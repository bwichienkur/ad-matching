using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Models;
using EDDY.IS.AdMatching.Domain.Services.Interfaces;
using EDDY.IS.AdMatching.Entities;
using NewRelic.Api.Agent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EDDY.IS.AdMatching.Core.RequestHandler
{
    public class StaticAdHandler : IChainHandler<AdMatchingModel>
    {
        private readonly PerformanceLogger _performanceLog;
        private IDataManager _dataManager;
        public IChainHandler<AdMatchingModel> Next { get; }

        public StaticAdHandler(
            IChainHandler<AdMatchingModel> next,
            PerformanceLogger performanceLogger,
            IDataManager dataManager
        )
        {
            Next = next;
            _dataManager = dataManager;
            _performanceLog = performanceLogger;
        }

        [Trace]
        public async Task Handle(AdMatchingModel model)
        {
            DateTime startTime = DateTime.Now;

            if (model.IsStatic)
            {
                var staticAd = model.Filtered.Ads?.Values
                   .Where(a => a.AdGuid == model.StaticAdGuid).FirstOrDefault();

                if (staticAd == null)
                {
                    //if the ad is not available, coulb be paused and we need to retrive that paused ad and use the backup Url
                    var ad = _dataManager.GetStaticAd(model.StaticAdGuid);
                    if (ad != null)
                        staticAd = new VwAdsAm()
                        {
                            AdId = -1,
                            ClientAdAccountId = ad.ClientAdAccountId,
                            Headline = "StaticFallback",
                            BackupUrl = ad.BackupUrl,
                            Cpc = 0.01m
                        };
                }

                // if the ad still null, is not a valid url or the ad was deleted
                if (staticAd == null)
                    throw new Exception("the static ad does not exist or is paused");

                //get the static ad from common dictionary, and setting a empty list for clear other ads in filtered dictionary
                model.StaticAds = staticAd;
                var key = string.Concat(model.StaticAds.Id, ":", model.SourceId);
                if (model.Filtered.SlimAdsDictionary.TryGetValue(key, out var slimStaticAd))
                    model.Filtered.SlimAdsDictionary = new Dictionary<string, SlimAd>() { { key, slimStaticAd } };
                else
                    model.Filtered.SlimAdsDictionary = new Dictionary<string, SlimAd>();

            }

            //filter campaigns by sourceid
            if (model.Filtered?.SlimAdsDictionary != null)
                model.MainDictionaryEvaluated.SlimAdsDictionary = new Dictionary<string, SlimAd>(model.Filtered.SlimAdsDictionary);


            _performanceLog.LogPerformanceDetail(model.SearchGuid, 1, "PreExcludeHandler", startTime);

            if (Next is not null)
                await Next.Handle(model);
        }

    }
}
