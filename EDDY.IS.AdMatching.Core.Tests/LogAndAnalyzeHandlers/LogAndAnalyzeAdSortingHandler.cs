using System.Diagnostics;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Core.RequestHandler;
using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Models;

namespace EDDY.IS.AdMatching.Core.Tests.LogAndAnalyzeHandlers;

public class LogAndAnalyzeAdSortingHandler : IChainHandler<AdMatchingModel>, IStopWatchable
{
    public IChainHandler<AdMatchingModel> DecoratedClass { get; set; }
private readonly Stopwatch ProcessStopWatch = new Stopwatch();
    public LogAndAnalyzeAdSortingHandler(IChainHandler<AdMatchingModel> next, DebugLogger debugLogger)
    {
        var decoratedClass = new AdSortingHandler(next, debugLogger);
        DecoratedClass = decoratedClass;
    }

    public IChainHandler<AdMatchingModel> Next { get; }


    public long GetElapsedTimeInMilliseconds
    {
        get
        {
            if (DecoratedClass.Next != null)
                return ProcessStopWatch.ElapsedMilliseconds -
                       ((IStopWatchable) DecoratedClass.Next).GetElapsedTimeInMilliseconds;

            return ProcessStopWatch.ElapsedMilliseconds;
        }
    }

    public void Handle(AdMatchingModel model)
    {
        ProcessStopWatch.Start();
        DecoratedClass.Handle(model);
        ProcessStopWatch.Stop();
    }
}