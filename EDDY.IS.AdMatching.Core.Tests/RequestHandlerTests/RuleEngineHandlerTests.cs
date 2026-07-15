using System;
using System.Collections.Generic;
using EDDY.IS.AdMatching.Core.Engines;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Core.RequestHandler;
using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Domain.Models;
using EDDY.IS.AdMatching.Entities;
using EDDY.IS.AdMatching.Service;
using EDDY.IS.Common.Settings;
using EDDY.IS.RuleEngine.CustomRuleEngine;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EDDY.IS.AdMatching.Core.Tests.RequestHandlerTests;

[TestClass]
public class RuleEngineHandlerTests
{
    private DebugLogger _debugLogger =
        new DebugLogger(
            Options.Create<LoggingDebugInformation>(new LoggingDebugInformation() {EnabledTrueFalse = true}));

    [DataTestMethod]
    [DataRow(RuleEngineHandlerScenario.Negative_NoCampaignsProvided)]
    [DataRow(RuleEngineHandlerScenario.Negative_AdMatchingModelIsNull)]
    [DataRow(RuleEngineHandlerScenario.Negative_AdMatchingModelCampaignListIsNull)]
    [DataRow(RuleEngineHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull)]
    [DataRow(RuleEngineHandlerScenario.Negative_OneCampaignWithKeyButNoValue)]
    [DataRow(RuleEngineHandlerScenario.Negative_ClientAdAccountsIsNull)]
    public void RuleEngineHandler_Handle(RuleEngineHandlerScenario ruleEngineScenario)
    {
        //Arrange
        IRuleEngine ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
        var ruleEngineHandler = new RuleEngineHandler(null,ruleEngine, _debugLogger);
        var model = new AdMatchingModel_RuleEngineTestingGenerator()
            .Add(ruleEngineScenario)
            .Build();

        //Act
        ruleEngineHandler.Handle(model);

        //Assert
        Assert.IsTrue(model.ValidateRuleEngineHandlerScenario(ruleEngineScenario));
    }
}

public class AdMatchingModel_RuleEngineTestingGenerator
{
    public AdMatchingModel_RuleEngineTestingGenerator()
    {
    }


    public AdMatchingModel Build()
    {
        return _adMatchingModel;
    }

    public AdMatchingModel _adMatchingModel { get; set; }

    public AdMatchingModel_RuleEngineTestingGenerator Add(RuleEngineHandlerScenario capsScenario)
    {
        var campaignId = 10;
        var secondCampaignId = 12;
        int clientAdAccountId = 8;
        int campaignStopId = 4;


        switch (capsScenario)
        {
            case RuleEngineHandlerScenario.Negative_AdMatchingModelIsNull:
                _adMatchingModel = null;
                break;

            case RuleEngineHandlerScenario.Negative_NoCampaignsProvided:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                break;

            case RuleEngineHandlerScenario.Negative_AdMatchingModelCampaignListIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = null;
                break;

            case RuleEngineHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = null;
                break;

            case RuleEngineHandlerScenario.Negative_OneCampaignWithKeyButNoValue:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;

            case RuleEngineHandlerScenario.Negative_ClientAdAccountsIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.ClientAdAccounts = null;
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;
            default:
                break;
        }

        return this;
    }

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

public static class AdMatchingModel_RuleEngineTestingExtensions
{
    public static bool ValidateRuleEngineHandlerScenario(this AdMatchingModel adMatchingModel,
        RuleEngineHandlerScenario capsScenario)
    {
        switch (capsScenario)
        {
            case RuleEngineHandlerScenario.Negative_NoCampaignsProvided:
            case RuleEngineHandlerScenario.Negative_AdMatchingModelIsNull:
            case RuleEngineHandlerScenario.Negative_AdMatchingModelCampaignListIsNull:
            case RuleEngineHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
            case RuleEngineHandlerScenario.Negative_OneCampaignWithKeyButNoValue:
            case RuleEngineHandlerScenario.Negative_ClientAdAccountsIsNull:
            {
                //in this case we just want to check no exception is throw, the handler should do nothing and just exit
                return true;
            }
            default:
                //we should never hit this condition, the exit condition should always be covered by the switch statement non default entries
                return false;
        }
    }
}

public enum RuleEngineHandlerScenario
{
    Negative_NoCampaignsProvided,
    Negative_AdMatchingModelIsNull,
    Negative_AdMatchingModelCampaignListIsNull,
    Negative_AdMatchingModelMainDictionaryEvaluatedIsNull,
    Negative_OneCampaignWithKeyButNoValue,
    Negative_ClientAdAccountsIsNull,
}