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
public class ParametersHandlerTests
{
    private DebugLogger _debugLogger =
        new DebugLogger(
            Options.Create<LoggingDebugInformation>(new LoggingDebugInformation() {EnabledTrueFalse = true}));

    [DataTestMethod]
    [DataRow(ParametersHandlerScenario.Negative_NoCampaignsProvided)]
    [DataRow(ParametersHandlerScenario.Negative_AdMatchingModelIsNull)]
    [DataRow(ParametersHandlerScenario.Negative_AdMatchingModelCampaignListIsNull)]
    [DataRow(ParametersHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull)]
    [DataRow(ParametersHandlerScenario.Negative_OneCampaignWithKeyButNoValue)]
    [DataRow(ParametersHandlerScenario.Negative_ClientAdAccountsIsNull)]
    public void ParametersHandler_Handle(ParametersHandlerScenario ParametersScenario)
    {
        //Arrange
        var ParametersHandler = new ParametersHandler(null, _debugLogger);
        var model = new AdMatchingModel_ParametersTestingGenerator()
            .Add(ParametersScenario)
            .Build();

        //Act
        ParametersHandler.Handle(model);

        //Assert
        Assert.IsTrue(model.ValidateParametersHandlerScenario(ParametersScenario));
    }
}

public class AdMatchingModel_ParametersTestingGenerator
{
    public AdMatchingModel_ParametersTestingGenerator()
    {
    }


    public AdMatchingModel Build()
    {
        return _adMatchingModel;
    }

    public AdMatchingModel _adMatchingModel { get; set; }

    public AdMatchingModel_ParametersTestingGenerator Add(ParametersHandlerScenario capsScenario)
    {
        var campaignId = 10;
        var secondCampaignId = 12;
        int clientAdAccountId = 8;
        int campaignStopId = 4;


        switch (capsScenario)
        {
            case ParametersHandlerScenario.Negative_AdMatchingModelIsNull:
                _adMatchingModel = null;
                break;

            case ParametersHandlerScenario.Negative_NoCampaignsProvided:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                break;

            case ParametersHandlerScenario.Negative_AdMatchingModelCampaignListIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = null;
                break;

            case ParametersHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = null;
                break;

            case ParametersHandlerScenario.Negative_OneCampaignWithKeyButNoValue:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;

            case ParametersHandlerScenario.Negative_ClientAdAccountsIsNull:
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

public static class AdMatchingModel_ParametersTestingExtensions
{
    public static bool ValidateParametersHandlerScenario(this AdMatchingModel adMatchingModel,
        ParametersHandlerScenario capsScenario)
    {
        switch (capsScenario)
        {
            case ParametersHandlerScenario.Negative_NoCampaignsProvided:
            case ParametersHandlerScenario.Negative_AdMatchingModelIsNull:
            case ParametersHandlerScenario.Negative_AdMatchingModelCampaignListIsNull:
            case ParametersHandlerScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
            case ParametersHandlerScenario.Negative_OneCampaignWithKeyButNoValue:
            case ParametersHandlerScenario.Negative_ClientAdAccountsIsNull:
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

public enum ParametersHandlerScenario
{
    Negative_NoCampaignsProvided,
    Negative_AdMatchingModelIsNull,
    Negative_AdMatchingModelCampaignListIsNull,
    Negative_AdMatchingModelMainDictionaryEvaluatedIsNull,
    Negative_OneCampaignWithKeyButNoValue,
    Negative_ClientAdAccountsIsNull,
}