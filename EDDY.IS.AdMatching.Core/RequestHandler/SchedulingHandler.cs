using EDDY.IS.AdMatching.Core.Engines;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Models;
using Microsoft.Extensions.Logging;
using NewRelic.Api.Agent;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDDY.IS.AdMatching.Core.RequestHandler
{
    public class SchedulingHandler : IChainHandler<AdMatchingModel>
    {
        private readonly DebugLogger _debugLogger;
        private readonly ILogger<SchedulingHandler> _logger;
        private readonly PerformanceLogger _performanceLog;
        public IChainHandler<AdMatchingModel> Next { get; }
        public SchedulingHandler(
            IChainHandler<AdMatchingModel> next, 
            DebugLogger debugLogger, 
            ILogger<SchedulingHandler> logger, 
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
            ScheduleEvaluator scheduleEval = new ScheduleEvaluator(_debugLogger);

            model.MainDictionaryEvaluated = scheduleEval.Process(model.Filtered, model.MainDictionaryEvaluated, model.Parameters);

            _performanceLog.LogPerformanceDetail(model.SearchGuid, 1, "SchedulingHandler", startTime);

            if (Next is not null)
                await Next.Handle(model);
        }

    }
}
