using EDDY.IS.AdMatching.Core.Auxiliary;
using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Models;
using System;
using EDDY.IS.AdMatching.Core.Engines;
using EDDY.IS.AdMatching.Core.Logging;
using Microsoft.Extensions.Logging;
using NewRelic.Api.Agent;
using System.Linq;
using System.Threading.Tasks;

namespace EDDY.IS.AdMatching.Core.RequestHandler
{
    public class CapsHandler : IChainHandler<AdMatchingModel>
    {
        private readonly DebugLogger _debugLogger;
        private readonly ILogger<CapsHandler> _logger;
        private readonly PerformanceLogger _performanceLog;
        public IChainHandler<AdMatchingModel> Next { get; }

        public CapsHandler(
            IChainHandler<AdMatchingModel> next, 
            DebugLogger debugLogger, 
            ILogger<CapsHandler> logger, 
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
            CapsEvaluator capsEval = new CapsEvaluator();

            model.MainDictionaryEvaluated = capsEval.Process(model?.Filtered, model?.MainDictionaryEvaluated);

            _performanceLog.LogPerformanceDetail(model.SearchGuid, 1, "CapsHandler", startTime);

            if (Next is not null)
                await Next.Handle(model);
        }
    }
}