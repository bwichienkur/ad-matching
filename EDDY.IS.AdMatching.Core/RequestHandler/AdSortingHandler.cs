using EDDY.IS.AdMatching.Core.Auxiliary;
using EDDY.IS.AdMatching.Core.Engines;
using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Models;
using System;
using EDDY.IS.AdMatching.Core.Logging;
using Microsoft.Extensions.Logging;
using NewRelic.Api.Agent;
using System.Threading.Tasks;

namespace EDDY.IS.AdMatching.Core.RequestHandler
{
    public class AdSortingHandler : IChainHandler<AdMatchingModel>
    {
        private readonly DebugLogger _debugLogger;
        private readonly ILogger<AdSortingHandler> _logger;
        private readonly PerformanceLogger _performanceLog;
        public IChainHandler<AdMatchingModel> Next { get; }
        public AdSortingHandler(IChainHandler<
            AdMatchingModel> next, 
            DebugLogger debugLogger, 
            ILogger<AdSortingHandler> logger, PerformanceLogger performanceLogger)
        {
            _logger = logger;
            _debugLogger = debugLogger;
            Next = next;
            _performanceLog = performanceLogger;
        }

        [Trace]
        public async Task Handle(AdMatchingModel model)
        {
            var startTime = DateTime.Now;
            var adSorting = new AdSortingEngine(_debugLogger);

            if (model.MainDictionaryEvaluated.CampaignsList.Count > 0)
                model.FinalAdsList = adSorting.Process(model.FinalAdsList, model.MainDictionaryEvaluated, model.MaxAds, model.IsStatic);

            _performanceLog.LogPerformanceDetail(model.SearchGuid, 1, "AdSortingHandler", startTime);

            if (Next is not null)
                await Next.Handle(model);
        }
    }
}
