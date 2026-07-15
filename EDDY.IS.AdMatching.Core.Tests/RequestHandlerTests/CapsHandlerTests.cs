using System.Collections.Generic;
using System.Diagnostics;
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
public class CapsHandlerTests
{
    private DebugLogger _debugLogger =
        new DebugLogger(
            Options.Create<LoggingDebugInformation>(new LoggingDebugInformation() {EnabledTrueFalse = false}));

    [DataTestMethod]
    [DataRow(CapsScenario.Negative_NoCampaignsProvided)]
    [DataRow(CapsScenario.Negative_AdMatchingModelIsNull)]
    [DataRow(CapsScenario.Negative_AdMatchingModelCampaignListIsNull)]
    [DataRow(CapsScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull)]
    [DataRow(CapsScenario.Negative_OneCampaignWithKeyButNoValue)]
    [DataRow(CapsScenario.Negative_CampaignRelationshipListIsNull)]
    [DataRow(CapsScenario.Positive_CampaignHasParentIsCapped)]
    [DataRow(CapsScenario.Positive_TwoCampaignNoParentNoCapped)]
    public void CapsHandler_Handle(CapsScenario capsScenario)
    {
        //Arrange
        var capsHandler = new CapsHandler(null, _debugLogger);
        var model = new AdMatchingModel_CapsTestingGenerator()
            .Add(capsScenario)
            .Build();

        //Act
        capsHandler.Handle(model);

        //Assert
        model.ValidateCapsScenario(capsScenario);
    }
    
    [TestMethod]
    public void CapsHandler_Benchmarking_Handle()
    {
        //Arrange
        var capsHandler = new CapsHandler(null, _debugLogger);

        var stopWatch = new Stopwatch();
        var iterations = 10000;
        
        //Act
        stopWatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var model = new AdMatchingModel_CapsTestingGenerator()
                .Add(CapsScenario.Benchmarking_CampaignHasParentIsCapped)
                .Build();
            capsHandler.Handle(model);
        }
        stopWatch.Stop();

        //Assert
        Trace.WriteLine($"Time per iterations: {iterations}: {stopWatch.ElapsedMilliseconds}ms, timer per iteration: {stopWatch.ElapsedMilliseconds*1.0/iterations}ms");
    }
}

public class AdMatchingModel_CapsTestingGenerator
{
    public AdMatchingModel_CapsTestingGenerator()
    {
    }


    public AdMatchingModel Build()
    {
        return _adMatchingModel;
    }

    public AdMatchingModel _adMatchingModel { get; set; }

    public AdMatchingModel_CapsTestingGenerator Add(CapsScenario capsScenario)
    {
        var campaignId = 10;
        var secondCampaignId = 12;

        switch (capsScenario)
        {
            case CapsScenario.Negative_AdMatchingModelIsNull:
                _adMatchingModel = null;
                break;

            case CapsScenario.Negative_NoCampaignsProvided:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                break;

            case CapsScenario.Negative_AdMatchingModelCampaignListIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = null;
                break;

            case CapsScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = null;
                break;

            case CapsScenario.Negative_OneCampaignWithKeyButNoValue:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;

            case CapsScenario.Negative_CampaignRelationshipListIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignRelationshipList = null;
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;

            case CapsScenario.Positive_CampaignHasParentIsCapped:
                GetAdMatchingModelTwoCampaign(campaignId, secondCampaignId, true);
                AddCampaignRelationShipListWithParent(campaignId, secondCampaignId);
                break;
            case CapsScenario.Benchmarking_CampaignHasParentIsCapped:
                for (int i = 0; i < 100; i++)
                {
                    var firstCampaignIdFor = campaignId+i;
                    var secondCampaignIdFor = 1000+campaignId+i;
                    GetAdMatchingModelTwoCampaign(firstCampaignIdFor, secondCampaignIdFor, true);
                    AddCampaignRelationShipListWithParent(firstCampaignIdFor, secondCampaignIdFor);
                }

                break;
            case CapsScenario.Positive_TwoCampaignNoParentNoCapped:
                GetAdMatchingModelTwoCampaign(campaignId, secondCampaignId, false);
                break;
        }

        return this;
    }

    private void GetAdMatchingModelTwoCampaign(int campaignId, int secondCampaignId, bool isCapped)
    {
        _adMatchingModel = new AdMatchingModel();
        _adMatchingModel.Filtered = new DictionaryContainer();
        _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
        _adMatchingModel.MainDictionaryEvaluated.CampaignRelationshipList = null;
        _adMatchingModel.MainDictionaryEvaluated.CampaignsList =
            GetTwoCampaigns(campaignId, secondCampaignId, isCapped);
        _adMatchingModel.Filtered.CampaignsList = GetTwoCampaigns(campaignId, secondCampaignId, isCapped);
    }

    private void AddCampaignRelationShipListWithParent(int campaignId, int secondCampaignId) =>
        _adMatchingModel.Filtered.CampaignRelationshipList = new Dictionary<int, CampaignRelationship>()
        {
            {
                0, new CampaignRelationship()
                {
                    CampaignId = campaignId,
                    LinkParentSchedule = true,
                    IsDeleted = false,
                    ParentCampaignId = secondCampaignId
                }
            }
        };

    private static Dictionary<int, Campaign> GetTwoCampaigns(int campaignId, int secondCampaignId, bool isCapped) =>
        new Dictionary<int, Campaign>()
        {
            {
                campaignId, new Campaign()
                {
                    CampaignId = campaignId,
                    IsCapped = isCapped
                }
            },
            {
                secondCampaignId, new Campaign()
                {
                    CampaignId = secondCampaignId,
                    IsCapped = isCapped
                }
            }
        };
}

public static class AdMatchingModel_CapsTestingExtensions
{
    public static void ValidateCapsScenario(this AdMatchingModel adMatchingModel, CapsScenario capsScenario)
    {
        switch (capsScenario)
        {
            case CapsScenario.Negative_NoCampaignsProvided:
            case CapsScenario.Negative_AdMatchingModelIsNull:
            case CapsScenario.Negative_AdMatchingModelCampaignListIsNull:
            case CapsScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
            case CapsScenario.Negative_OneCampaignWithKeyButNoValue:
            case CapsScenario.Negative_CampaignRelationshipListIsNull:
            {
                //in this case we just want to check no exception is throw, the handler should do nothing and just exit

                break;
            }
            case CapsScenario.Positive_TwoCampaignNoParentNoCapped:
            {
                Assert.IsTrue(adMatchingModel.MainDictionaryEvaluated.CampaignsList is {Count: 2});
                break;
            }
            case CapsScenario.Positive_CampaignHasParentIsCapped:
            {
                Assert.IsTrue(adMatchingModel.MainDictionaryEvaluated.CampaignsList is {Count: 0});
                break;
            }
            default:
                //we should never hit this condition, the exit condition should always be covered by the switch statement non default entries
                Assert.Fail("Should never happen");
                break;
        }
    }
}

public enum CapsScenario
{
    Negative_NoCampaignsProvided,
    Negative_AdMatchingModelIsNull,
    Negative_AdMatchingModelCampaignListIsNull,
    Negative_AdMatchingModelMainDictionaryEvaluatedIsNull,
    Negative_OneCampaignWithKeyButNoValue,
    Negative_CampaignRelationshipListIsNull,
    Positive_CampaignHasParentIsCapped,
    Positive_TwoCampaignNoParentNoCapped,
    Benchmarking_CampaignHasParentIsCapped
}