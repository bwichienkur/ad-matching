using EDDY.IS.AdMatching.Core.Engines;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Models;
using Microsoft.Extensions.Logging;
using NewRelic.Api.Agent;
using System;
using System.Threading.Tasks;

namespace EDDY.IS.AdMatching.Core.RequestHandler
{
    public  class ResponseBuilderHandler : IChainHandler<AdMatchingModel>
    {
        private readonly DebugLogger _debugLogger;
        private readonly ILogger<ResponseBuilderHandler> _logger;
        private readonly PerformanceLogger _performanceLog;
        public IChainHandler<AdMatchingModel> Next { get; }

        public ResponseBuilderHandler(
            IChainHandler<AdMatchingModel> next, 
            DebugLogger debugLogger, 
            ILogger<ResponseBuilderHandler> logger, 
            PerformanceLogger performanceLogger)
        {
            _logger = logger;
            _debugLogger = debugLogger;
            Next = next;
            _performanceLog = performanceLogger;
        }

        [Trace]
        public async Task Handle(AdMatchingModel model)
        {
            ResponseBuilder responseBuilder = new ResponseBuilder(_debugLogger);
            DateTime startTime = DateTime.Now;

            model.FinalAdsList = model.IsStatic 
                ? responseBuilder.GetStaticAdMatchResponse(model.MainDictionaryEvaluated, model.StaticAds, model.Filtered.Ads) 
                : responseBuilder.GetAdsMatchedResponse(model.MainDictionaryEvaluated, model.Filtered.Ads);

            _performanceLog.LogPerformanceDetail(model.SearchGuid, 1, "ResponseBuilderHandler", startTime);

            if (Next is not null)
                await Next.Handle(model);

        }

    }
}
