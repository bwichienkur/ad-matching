using System;
using System.Collections.Generic;
using System.Linq;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Core.RequestHandler;
using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Domain.Models;
using EDDY.IS.AdMatching.Entities;
using EDDY.IS.AdMatching.Service;
using EDDY.IS.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDDY.IS.AdMatching.Core.Tests.RequestHandlerTests;

[TestClass]
public class AdSortingHandlerTests
{
    private DebugLogger _debugLogger =
        new DebugLogger(
            Options.Create<LoggingDebugInformation>(new LoggingDebugInformation() {EnabledTrueFalse = false}));

    [DataTestMethod]
    [DataRow(AdSortingHandlerScenario.Negative_NoCampaignsProvided)]
    [DataRow(AdSortingHandlerScenario.Negative_AdMatchingModelIsNull)]
    [DataRow(AdSortingHandlerScenario.Negative_AdMatchingModelCampaignListIsNull)]
    [DataRow(AdSortingHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull)]
    [DataRow(AdSortingHandlerScenario.Negative_OneCampaignWithKeyButNoValue)]
    [DataRow(AdSortingHandlerScenario.Negative_ClientAdAccountsIsNull)]
    [DataRow(AdSortingHandlerScenario.Positive_HappyPath)]
    public void AdSortingHandler_Handle(AdSortingHandlerScenario AdSortingScenario)
    {
        //Arrange
        var AdSortingHandler = new AdSortingHandler(null, _debugLogger);
        var model = new AdMatchingModel_AdSortingTestingGenerator()
            .Add(AdSortingScenario)
            .Build();

        //Act
        AdSortingHandler.Handle(model);

        //Assert
        model.ValidateAdSortingHandlerScenario(AdSortingScenario);
    }
}

public class AdMatchingModel_AdSortingTestingGenerator
{
    public AdMatchingModel_AdSortingTestingGenerator()
    {
    }


    public AdMatchingModel Build()
    {
        return _adMatchingModel;
    }

    public AdMatchingModel _adMatchingModel { get; set; }

    public AdMatchingModel_AdSortingTestingGenerator Add(AdSortingHandlerScenario capsScenario)
    {
        var campaignId = 10;
        var secondCampaignId = 12;
        int clientAdAccountId = 8;
        int campaignStopId = 4;


        switch (capsScenario)
        {
            case AdSortingHandlerScenario.Negative_AdMatchingModelIsNull:
                _adMatchingModel = null;
                break;

            case AdSortingHandlerScenario.Negative_NoCampaignsProvided:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                break;

            case AdSortingHandlerScenario.Negative_AdMatchingModelCampaignListIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = null;
                break;

            case AdSortingHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = null;
                break;

            case AdSortingHandlerScenario.Negative_OneCampaignWithKeyButNoValue:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;

            case AdSortingHandlerScenario.Negative_ClientAdAccountsIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.ClientAdAccounts = null;
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;
            case AdSortingHandlerScenario.Positive_HappyPath:
                //from SD-7821: BoostedCPC = RealCPC + (RealCPC * (Ad.RankMultiplier/100)) + (RealCPC * (Ad.SchoolMultiplier/100))
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList =
                    GetTwoCampaigns(campaignId, secondCampaignId, clientAdAccountId);
                _adMatchingModel.MaxAds = 10;
                _adMatchingModel.MainDictionaryEvaluated.CampaignSchedulerList =
                    new Dictionary<int, CampaignSchedule>();
                _adMatchingModel.FinalAdsList = new List<AdsMatched>()
                {
                    {
                        new AdsMatched()
                        {
                            AdId = 0,
                            CPC = (decimal) (_random.NextDouble() * 100.0),
                            RankMultiplier = (decimal) (_random.NextDouble() * 100.0),
                            SchoolMultiplier = (decimal) (_random.NextDouble() * 100.0),
                        }
                    }
                };
                break;
            default:
                break;
        }

        return this;
    }

    private Random _random = new Random(DateTime.Now.Millisecond);

    private void GetAdMatchingModelTwoCampaign(int campaignId, int secondCampaignId, int clientAdAccountId,
        bool addCampaignListForMainDictionaryEvaluated)
    {
        _adMatchingModel = new AdMatchingModel();
        _adMatchingModel.Filtered = new DictionaryContainer();
        _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
        if (addCampaignListForMainDictionaryEvaluated)
        {
            _adMatchingModel.MainDictionaryEvaluated.CampaignsList =
                GetTwoCampaigns(campaignId, secondCampaignId, clientAdAccountId);
        }

        _adMatchingModel.Filtered.CampaignsList = GetTwoCampaigns(campaignId, secondCampaignId, clientAdAccountId);
    }

    private static Dictionary<int, Campaign> GetTwoCampaigns(int campaignId, int secondCampaignId,
        int clientAdAccountId) =>
        new Dictionary<int, Campaign>()
        {
            {
                campaignId, new Campaign()
                {
                    CampaignId = campaignId,
                    ClientAdAccountId = clientAdAccountId,
                }
            },
            {
                secondCampaignId, new Campaign()
                {
                    CampaignId = secondCampaignId,
                    ClientAdAccountId = clientAdAccountId,
                }
            }
        };
}

public static class AdMatchingModel_AdSortingTestingExtensions
{
    public static void ValidateAdSortingHandlerScenario(this AdMatchingModel adMatchingModel,
        AdSortingHandlerScenario capsScenario)
    {
        switch (capsScenario)
        {
            case AdSortingHandlerScenario.Negative_NoCampaignsProvided:
            case AdSortingHandlerScenario.Negative_AdMatchingModelIsNull:
            case AdSortingHandlerScenario.Negative_AdMatchingModelCampaignListIsNull:
            case AdSortingHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
            case AdSortingHandlerScenario.Negative_OneCampaignWithKeyButNoValue:
            case AdSortingHandlerScenario.Negative_ClientAdAccountsIsNull:
                break;
            case AdSortingHandlerScenario.Positive_HappyPath:
                //from SD-7821: BoostedCPC = (RealCPC * (Ad.RankMultiplier/100)) + (RealCPC * (Ad.SchoolMultiplier/100))
                var adsMatched = adMatchingModel.FinalAdsList.First();
                var expectedCpcValue = adsMatched.RealCPC + (adsMatched.RealCPC * (double) (adsMatched.RankMultiplier / 100.0m)) +
                                       ((adsMatched.RealCPC * (double) (adsMatched.SchoolMultiplier / 100.0m)));
                Assert.IsTrue(Math.Abs(adsMatched.BoostedCPC - expectedCpcValue) < .001);
                break;
            default:
                Assert.Fail("Should never happen");
                break;
        }
    }
}

public enum AdSortingHandlerScenario
{
    Negative_NoCampaignsProvided,
    Negative_AdMatchingModelIsNull,
    Negative_AdMatchingModelCampaignListIsNull,
    Negative_AdMatchingModelMainDictionaryEvaluatedIsNull,
    Negative_OneCampaignWithKeyButNoValue,
    Negative_ClientAdAccountsIsNull,
    Positive_HappyPath
}