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
    public class ParametersHandler : IChainHandler<AdMatchingModel>
    {
        private readonly DebugLogger _debugLogger;
        private readonly ILogger<ParametersHandler> _logger;
        private readonly PerformanceLogger _performanceLog;
        public IChainHandler<AdMatchingModel> Next { get; }
        public ParametersHandler(
            IChainHandler<AdMatchingModel> next, 
            DebugLogger debugLogger, 
            ILogger<ParametersHandler> logger, 
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
            DateTime startTime = DateTime.Now;
            ParametersEvaluator parametersEngine = new ParametersEvaluator();

            if(model.MainDictionaryEvaluated.CampaignsList.Count > 0)
                model.FinalAdsList = parametersEngine.Process(model.FinalAdsList, model.Parameters, model.Filtered);

            _performanceLog.LogPerformanceDetail(model.SearchGuid, 1, "ParametersHandler", startTime);

            if (Next is not null)
                await Next.Handle(model);

        }

    }
}
