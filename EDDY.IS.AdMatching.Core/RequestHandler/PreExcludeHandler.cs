using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Models;
using NewRelic.Api.Agent;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EDDY.IS.AdMatching.Core.RequestHandler
{
    public class PreExcludeHandler : IChainHandler<AdMatchingModel>
    {
        private readonly PerformanceLogger _performanceLog;
        public IChainHandler<AdMatchingModel> Next { get; }

        public PreExcludeHandler(
            IChainHandler<AdMatchingModel> next,
            PerformanceLogger performanceLogger
            )
        {
            Next = next;
            _performanceLog = performanceLogger;
        }

        [Trace]
        public async Task Handle(AdMatchingModel model)
        {
            DateTime startTime = DateTime.Now;

            PreExcludeInstitutions(model);

            _performanceLog.LogPerformanceDetail(model.SearchGuid, 1, "PreExcludeHandler", startTime);

            if (Next is not null)
                await Next.Handle(model);
        }

        private static void PreExcludeInstitutions(AdMatchingModel model)
        {
            var preExcludedInstitutionsIds = model?.Filtered.ClientAdAccounts
                                            .Where(x => model.PreExcludeInstitutions.Select(p => p.ToLower())
                                            .Contains(x.Value.InstitutionAlias.ToLower()))
                                            .Select(x => x.Value.ClientAdAccountId);

            if (preExcludedInstitutionsIds.Any())
            {
                foreach (var institutionId in preExcludedInstitutionsIds)
                {
                    //  Removing ClientAdAccounts
                    model?.Filtered.ClientAdAccounts.Remove(institutionId);

                    //  Removing Ads
                    foreach (var adToRemove in model?.MainDictionaryEvaluated.SlimAdsDictionary
                                     .Where(a => a.Value.ClientAdAccountId == institutionId))
                    {
                        model?.MainDictionaryEvaluated.SlimAdsDictionary.Remove(adToRemove.Key);
                    }
                }
            }
        }
    }
}
