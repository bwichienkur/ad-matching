using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EDDY.IS.AdMatching.Core.Engines;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Core.RequestHandler;
using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Domain.Models;
using EDDY.IS.AdMatching.Entities;
using EDDY.IS.AdMatching.Service;
using EDDY.IS.Common.Dto.RuleEngine;
using EDDY.IS.Common.Settings;
using EDDY.IS.RuleEngine.CustomRuleEngine;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace EDDY.IS.AdMatching.Core.Tests.RequestHandlerTests;

[TestClass]
public class DynamicBidVariablesHandlerTests
{
    private DebugLogger _debugLogger =
        new DebugLogger(
            Options.Create<LoggingDebugInformation>(new LoggingDebugInformation() {EnabledTrueFalse = false}));

    [DataTestMethod]
    [DataRow(DynamicBidVariablesHandlerScenario.Negative_NoCampaignsProvided)]
    [DataRow(DynamicBidVariablesHandlerScenario.Negative_AdMatchingModelIsNull)]
    [DataRow(DynamicBidVariablesHandlerScenario.Negative_AdMatchingModelCampaignListIsNull)]
    [DataRow(DynamicBidVariablesHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull)]
    [DataRow(DynamicBidVariablesHandlerScenario.Negative_OneCampaignWithKeyButNoValue)]
    [DataRow(DynamicBidVariablesHandlerScenario.Negative_ClientAdAccountsIsNull)]
    [DataRow(DynamicBidVariablesHandlerScenario.Positive_DefaultRuleIsSetForTargetingRuleNoBidValueIsAssigned)]
    [DataRow(DynamicBidVariablesHandlerScenario.Positive_HappyPath)]
    public void DynamicBidVariablesHandler_Handle(DynamicBidVariablesHandlerScenario DynamicBidVariablesScenario)
    {
        //Arrange
        IRuleEngine ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
        var DynamicBidVariablesHandler = new DynamicBidVariablesHandler(null, ruleEngine, _debugLogger);
        var model = new AdMatchingModel_DynamicBidVariablesTestingGenerator()
            .Add(DynamicBidVariablesScenario)
            .Build();

        //Act
        DynamicBidVariablesHandler.Handle(model);

        //Assert
        model.ValidateDynamicBidVariablesHandlerScenario(DynamicBidVariablesScenario);
    }
    
    [TestMethod]
    public void DynamicBidVariablesHandler_Benchmarking_Handle()
    {
        //Arrange
        IRuleEngine ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
        var stopWatch = new Stopwatch();
        var iterations = 10000;
        
        //Act
        stopWatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var model = new AdMatchingModel_DynamicBidVariablesTestingGenerator()
                .Add(DynamicBidVariablesHandlerScenario.Positive_HappyPath)
                .Build();
            var handler = new DynamicBidVariablesHandler(null, ruleEngine, _debugLogger);
            handler.Handle(model);
        }
        stopWatch.Stop();

        //Assert
        Trace.WriteLine($"Time per iterations: {iterations}: {stopWatch.ElapsedMilliseconds}ms, timer per iteration: {stopWatch.ElapsedMilliseconds*1.0/iterations}ms");

    }
}

public class AdMatchingModel_DynamicBidVariablesTestingGenerator
{
    public AdMatchingModel_DynamicBidVariablesTestingGenerator()
    {
    }


    public AdMatchingModel Build()
    {
        return _adMatchingModel;
    }

    public AdMatchingModel _adMatchingModel { get; set; }

    public AdMatchingModel_DynamicBidVariablesTestingGenerator Add(DynamicBidVariablesHandlerScenario capsScenario)
    {
        var campaignId = 10;
        var secondCampaignId = 12;
        int clientAdAccountId = 8;
        int campaignStopId = 4;


        switch (capsScenario)
        {
            case DynamicBidVariablesHandlerScenario.Negative_AdMatchingModelIsNull:
                _adMatchingModel = null;
                break;

            case DynamicBidVariablesHandlerScenario.Negative_NoCampaignsProvided:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                break;

            case DynamicBidVariablesHandlerScenario.Negative_AdMatchingModelCampaignListIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = null;
                break;

            case DynamicBidVariablesHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.Filtered.TargetingRules = new Dictionary<int, TargetingRule>();
                _adMatchingModel.MainDictionaryEvaluated = null;
                break;

            case DynamicBidVariablesHandlerScenario.Negative_OneCampaignWithKeyButNoValue:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.Filtered.TargetingRules = new Dictionary<int, TargetingRule>();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;

            case DynamicBidVariablesHandlerScenario.Negative_ClientAdAccountsIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.ClientAdAccounts = null;
                _adMatchingModel.Filtered.TargetingRules = new Dictionary<int, TargetingRule>();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;
            case DynamicBidVariablesHandlerScenario.Positive_DefaultRuleIsSetForTargetingRuleNoBidValueIsAssigned:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.FinalAdsList = new List<AdsMatched>()
                {
                    {
                        new AdsMatched()
                        {
                            CampaignId = campaignId
                        }
                    }
                };                
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.Parameters = new Dictionary<string, string>();
                _adMatchingModel.Filtered.TargetingRules = new Dictionary<int, TargetingRule>()
                {
                    {
                        0, new TargetingRule()
                        {
                            Campaign = new Campaign(),
                            RuleAsQueryBuilderFilterRule = JsonConvert.DeserializeObject<QueryBuilderFilterRule>("{\"condition\":\"AND\",\"rules\":[],\"valid\":true}"),
                            IsEnabled = true,
                            IsDeleted = false,
                            DynamicBoostPercent = 100,
                            IsDynamicBid = true,
                            CampaignId = campaignId
                        }
                    }
                };
                break;
            case DynamicBidVariablesHandlerScenario.Positive_HappyPath:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.FinalAdsList = new List<AdsMatched>()
                {
                    {
                        new AdsMatched()
                        {
                            CampaignId = campaignId
                        }
                    }
                };                
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.Parameters = new Dictionary<string, string>()
                {
                    {
                        "HighSchoolGradYear","2011"
                    }
                };

                _adMatchingModel.Filtered.TargetingRules = new Dictionary<int, TargetingRule>()
                {
                    {
                        0, new TargetingRule()
                        {
                            Campaign = new Campaign(),
                            RuleAsQueryBuilderFilterRule = ruleAsQueryBuilderFilterRule,
                            IsEnabled = true,
                            IsDeleted = false,
                            DynamicBoostPercent = 100,
                            IsDynamicBid = true,
                            CampaignId = campaignId
                        }
                    }
                };
                break;
            default:
                break;
        }

        return this;
    }

    private static QueryBuilderFilterRule ruleAsQueryBuilderFilterRule = JsonConvert.DeserializeObject<QueryBuilderFilterRule>("{\"condition\":\"AND\",\"rules\":[{\"condition\":\"AND\",\"rules\":[{\"id\":\"HighSchoolGradYear\",\"field\":\"HighSchoolGradYear\",\"type\":\"integer\",\"input\":\"number\",\"operator\":\"range\",\"value\":[2011,2022]},{\"id\":\"AcceptMissingKey\",\"field\":\"AcceptMissingKey\",\"type\":\"string\",\"input\":\"checkbox\",\"operator\":\"is\",\"value\":[],\"data\":{\"forceEnd\":\"true\"}}]}],\"valid\":true}");
    
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

public static class AdMatchingModel_DynamicBidVariablesTestingExtensions
{
    public static void ValidateDynamicBidVariablesHandlerScenario(this AdMatchingModel adMatchingModel,
        DynamicBidVariablesHandlerScenario capsScenario)
    {
        switch (capsScenario)
        {
            case DynamicBidVariablesHandlerScenario.Negative_NoCampaignsProvided:
            case DynamicBidVariablesHandlerScenario.Negative_AdMatchingModelIsNull:
            case DynamicBidVariablesHandlerScenario.Negative_AdMatchingModelCampaignListIsNull:
            case DynamicBidVariablesHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
            case DynamicBidVariablesHandlerScenario.Negative_OneCampaignWithKeyButNoValue:
            case DynamicBidVariablesHandlerScenario.Negative_ClientAdAccountsIsNull:
                //in this case we just want to check no exception is throw, the handler should do nothing and just exit
                Assert.IsTrue(true);
                break;
            case DynamicBidVariablesHandlerScenario.Positive_DefaultRuleIsSetForTargetingRuleNoBidValueIsAssigned:
                Assert.IsTrue(adMatchingModel.FinalAdsList.First().DynamicBoostPercent == 0 || adMatchingModel.FinalAdsList.First().DynamicBoostPercent == null);
                break;
            case DynamicBidVariablesHandlerScenario.Positive_HappyPath:
                Assert.IsTrue(adMatchingModel.FinalAdsList.First().DynamicBoostPercent == 100);
                break;
            default:
                //we should never hit this condition, the exit condition should always be covered by the switch statement non default entries
                Assert.Fail("We should never hit this condition");
                break;
        }
    }
}

public enum DynamicBidVariablesHandlerScenario
{
    Negative_NoCampaignsProvided,
    Negative_AdMatchingModelIsNull,
    Negative_AdMatchingModelCampaignListIsNull,
    Negative_AdMatchingModelMainDictionaryEvaluatedIsNull,
    Negative_OneCampaignWithKeyButNoValue,
    Negative_ClientAdAccountsIsNull,
    Positive_DefaultRuleIsSetForTargetingRuleNoBidValueIsAssigned,
    Positive_HappyPath
}