using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Dto;
using EDDY.IS.AdMatching.Domain.Models;
using EDDY.IS.AdMatching.Domain.Services.Interfaces;
using EDDY.IS.AdMatching.Entities;
using NewRelic.Api.Agent;

namespace EDDY.IS.AdMatching.Domain.Services;

public class AdMatchingService : IAdMatchingService
{
    private readonly IEngine _engine;
    private readonly IChainHandler<AdMatchingModel> _chainHandler;

    public AdMatchingService(
        IEngine engine, 
        IChainHandler<AdMatchingModel> chainHandler)
    {
        _engine = engine;
        _chainHandler = chainHandler;
    }

    [Trace]
    public async Task<AdMatchingResponse> GetAdsMatched(AdMatchingRequest request)
    {
        var response = new AdMatchingResponse();
        AdMatchingModel adMatchingModel = CreateModel(request);

        //Obtaining the filtered dictionary
        adMatchingModel.Filtered = await _engine.GetCacheDictionaryContainer(Convert.ToInt32(request.SourceId));

        if (adMatchingModel.Filtered?.SlimAdsDictionary?.Count() == 0 && adMatchingModel.StaticAds == null)
        {
            response.Message = "No ads were matched with the supplied request parameters";
            response.AdsReturned = 0;
            return response;
        }

        //Making the evaluation of each engine
        await _chainHandler.Handle(adMatchingModel);

        response.AdsMatched = adMatchingModel.FinalAdsList;
        

        if (adMatchingModel.FinalAdsList.Count() == 0)
        {
            response.Message = "No ads were matched after the filtering of accounts and campaings";
            response.AdsReturned = 0;
            return response;
        }

        response.Message = "Success!";
        response.AdsReturned = adMatchingModel.FinalAdsList.Count();
        return response;
    }

    private static AdMatchingModel CreateModel(AdMatchingRequest request)
    {
        var isStatic = Guid.TryParse(request.StaticAdGuid, out Guid staticAdGuid);
        
        return new AdMatchingModel()
        {
            SourceId = request.SourceId,
            Filtered = new FilteredContainerDictionary(),
            MainDictionaryEvaluated = new FilteredContainerDictionary(),
            FinalAdsList = new List<AdsMatched>(),
            SearchGuid = request.SearchGuid,
            Parameters = request.Parameters.ToDictionary(x => x.Key, x => x.Value),
            DynamicParameters = new Dictionary<string, string>(),
            MaxAds = request.MaxAds,
            IsStatic = isStatic,
            StaticAdGuid = staticAdGuid,
            PreExcludeInstitutions = request.PreExcludeInstitutions
        };
    }
}

