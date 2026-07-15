using System;
using System.Collections.Generic;
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
public class ResponseBuilderHandlerTests
{
    private DebugLogger _debugLogger =
        new DebugLogger(
            Options.Create<LoggingDebugInformation>(new LoggingDebugInformation() {EnabledTrueFalse = true}));

    [DataTestMethod]
    [DataRow(ResponseBuilderHandlerScenario.Negative_NoCampaignsProvided)]
    [DataRow(ResponseBuilderHandlerScenario.Negative_AdMatchingModelIsNull)]
    [DataRow(ResponseBuilderHandlerScenario.Negative_AdMatchingModelCampaignListIsNull)]
    [DataRow(ResponseBuilderHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull)]
    [DataRow(ResponseBuilderHandlerScenario.Negative_OneCampaignWithKeyButNoValue)]
    [DataRow(ResponseBuilderHandlerScenario.Negative_ClientAdAccountsIsNull)]
    public void ResponseBuilderHandler_Handle(ResponseBuilderHandlerScenario ResponseBuilderScenario)
    {
        //Arrange
        var ResponseBuilderHandler = new ResponseBuilderHandler(null, _debugLogger);
        var model = new AdMatchingModel_ResponseBuilderTestingGenerator()
            .Add(ResponseBuilderScenario)
            .Build();

        //Act
        ResponseBuilderHandler.Handle(model);

        //Assert
        Assert.IsTrue(model.ValidateResponseBuilderHandlerScenario(ResponseBuilderScenario));
    }
}

public class AdMatchingModel_ResponseBuilderTestingGenerator
{
    public AdMatchingModel_ResponseBuilderTestingGenerator()
    {
    }


    public AdMatchingModel Build()
    {
        return _adMatchingModel;
    }

    public AdMatchingModel _adMatchingModel { get; set; }

    public AdMatchingModel_ResponseBuilderTestingGenerator Add(ResponseBuilderHandlerScenario capsScenario)
    {
        var campaignId = 10;
        var secondCampaignId = 12;
        int clientAdAccountId = 8;
        int campaignStopId = 4;


        switch (capsScenario)
        {
            case ResponseBuilderHandlerScenario.Negative_AdMatchingModelIsNull:
                _adMatchingModel = null;
                break;

            case ResponseBuilderHandlerScenario.Negative_NoCampaignsProvided:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                break;

            case ResponseBuilderHandlerScenario.Negative_AdMatchingModelCampaignListIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = null;
                break;

            case ResponseBuilderHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = null;
                break;

            case ResponseBuilderHandlerScenario.Negative_OneCampaignWithKeyButNoValue:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;

            case ResponseBuilderHandlerScenario.Negative_ClientAdAccountsIsNull:
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

public static class AdMatchingModel_ResponseBuilderTestingExtensions
{
    public static bool ValidateResponseBuilderHandlerScenario(this AdMatchingModel adMatchingModel,
        ResponseBuilderHandlerScenario capsScenario)
    {
        switch (capsScenario)
        {
            case ResponseBuilderHandlerScenario.Negative_NoCampaignsProvided:
            case ResponseBuilderHandlerScenario.Negative_AdMatchingModelIsNull:
            case ResponseBuilderHandlerScenario.Negative_AdMatchingModelCampaignListIsNull:
            case ResponseBuilderHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
            case ResponseBuilderHandlerScenario.Negative_OneCampaignWithKeyButNoValue:
            case ResponseBuilderHandlerScenario.Negative_ClientAdAccountsIsNull:
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

public enum ResponseBuilderHandlerScenario
{
    Negative_NoCampaignsProvided,
    Negative_AdMatchingModelIsNull,
    Negative_AdMatchingModelCampaignListIsNull,
    Negative_AdMatchingModelMainDictionaryEvaluatedIsNull,
    Negative_OneCampaignWithKeyButNoValue,
    Negative_ClientAdAccountsIsNull,
}