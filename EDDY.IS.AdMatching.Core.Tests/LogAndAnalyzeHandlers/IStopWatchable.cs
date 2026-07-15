using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Models;

namespace EDDY.IS.AdMatching.Core.Tests.LogAndAnalyzeHandlers;

public interface IStopWatchable
{
    IChainHandler<AdMatchingModel> DecoratedClass { get; set; }

    long GetElapsedTimeInMilliseconds { get;  }
}