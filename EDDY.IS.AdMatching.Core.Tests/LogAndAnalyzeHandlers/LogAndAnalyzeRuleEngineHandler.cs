using System.Diagnostics;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Core.RequestHandler;
using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Models;
using EDDY.IS.RuleEngine.CustomRuleEngine;

namespace EDDY.IS.AdMatching.Core.Tests.LogAndAnalyzeHandlers;

public class LogAndAnalyzeRuleEngineHandler : IChainHandler<AdMatchingModel>, IStopWatchable
{
    public IChainHandler<AdMatchingModel> DecoratedClass { get; set; }
private readonly Stopwatch ProcessStopWatch = new Stopwatch();
    public LogAndAnalyzeRuleEngineHandler(IChainHandler<AdMatchingModel> next,IRuleEngine ruleEngine, DebugLogger debugLogger)
    {
        var decoratedClass = 
            new RuleEngineHandler(
                next,
                ruleEngine, 
                debugLogger);
        DecoratedClass = decoratedClass;
    }

    public IChainHandler<AdMatchingModel> Next { get; }


    public long GetElapsedTimeInMilliseconds
    {
        get
        {
            return ProcessStopWatch.ElapsedMilliseconds -
                   ((IStopWatchable) DecoratedClass.Next).GetElapsedTimeInMilliseconds;
        }
    }

    public void Handle(AdMatchingModel model)
    {
        ProcessStopWatch.Start();
        ((IChainHandler<AdMatchingModel>) DecoratedClass).Handle(model);
        ProcessStopWatch.Stop();
    }
}