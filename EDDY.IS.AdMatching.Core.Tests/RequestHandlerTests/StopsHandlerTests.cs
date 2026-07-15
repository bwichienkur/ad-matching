using System;
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
public class StopsHandlerTests
{
    private DebugLogger _debugLogger =
        new DebugLogger(
            Options.Create<LoggingDebugInformation>(new LoggingDebugInformation() {EnabledTrueFalse = false}));
    
    [DataTestMethod]
    [DataRow(StopsScenario.Negative_NoCampaignsProvided)]
    [DataRow(StopsScenario.Negative_AdMatchingModelIsNull)]
    [DataRow(StopsScenario.Negative_AdMatchingModelCampaignListIsNull)]
    [DataRow(StopsScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull)]
    [DataRow(StopsScenario.Negative_OneCampaignWithKeyButNoValue)]
    [DataRow(StopsScenario.Negative_ClientAdAccountsIsNull)]
    [DataRow(StopsScenario.Positive_CampaignOutOfClientAdAccountStopHourNotInMainDictionaryAdded)]
    [DataRow(StopsScenario.Positive_CampaignInStopHourInMainDictionaryRemoved)]
    public void StopsHandler_Handle(StopsScenario capsScenario)
    {
        //Arrange
        var capsHandler = new StopsHandler(null, _debugLogger);
        var model = new AdMatchingModel_StopsTestingGenerator()
            .Add(capsScenario)
            .Build();

        //Act
        capsHandler.Handle(model);

        //Assert
        Assert.IsTrue(model.ValidateStopsScenario(capsScenario));
    }
    
    [TestMethod]
    public void StopsHandler_Benchmarking_Handle()
    {
        //Arrange
        var stopsHandler = new StopsHandler(null, _debugLogger);

        var stopWatch = new Stopwatch();
        var iterations = 10000;
        
        //Act
        stopWatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var model = new AdMatchingModel_StopsTestingGenerator()
                .Add(StopsScenario.Benchmarking_CampaignOutOfClientAdAccountStopHourNotInMainDictionaryAdded)
                .Build();
            stopsHandler.Handle(model);
        }
        stopWatch.Stop();

        //Assert
        Trace.WriteLine($"Time per iterations: {iterations}: {stopWatch.ElapsedMilliseconds}ms, timer per iteration: {stopWatch.ElapsedMilliseconds*1.0/iterations}ms");
    }
}

public class AdMatchingModel_StopsTestingGenerator
{
    private Random _random = new Random(DateTime.Now.Millisecond);

    public AdMatchingModel_StopsTestingGenerator()
    {
    }


    public AdMatchingModel Build()
    {
        return _adMatchingModel;
    }

    public AdMatchingModel _adMatchingModel { get; set; }

    public AdMatchingModel_StopsTestingGenerator Add(StopsScenario capsScenario)
    {
        var campaignId = 10;
        var secondCampaignId = 12;
        int clientAdAccountId = 8;
        int campaignStopId = 4;


        switch (capsScenario)
        {
            case StopsScenario.Negative_AdMatchingModelIsNull:
                _adMatchingModel = null;
                break;

            case StopsScenario.Negative_NoCampaignsProvided:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                break;

            case StopsScenario.Negative_AdMatchingModelCampaignListIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = null;
                break;

            case StopsScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = null;
                break;

            case StopsScenario.Negative_OneCampaignWithKeyButNoValue:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;

            case StopsScenario.Negative_ClientAdAccountsIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.ClientAdAccounts = null;
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;
            case StopsScenario.Positive_CampaignInStopHourInMainDictionaryRemoved:
                GetAdMatchingModelTwoCampaign(campaignId, secondCampaignId, clientAdAccountId, true);
                _adMatchingModel.MainDictionaryEvaluated.ClientAdAccounts = new Dictionary<int, ClientAdAccount>();
                _adMatchingModel.Filtered.ClientAccountStopList = new Dictionary<int, ClientAdAccountStop>();
                _adMatchingModel.Filtered.ClientAdAccounts = new Dictionary<int, ClientAdAccount>()
                {
                    {
                        0, new ClientAdAccount()
                        {
                            ClientAdAccountId = clientAdAccountId,
                            IsEnabled = true,
                            IsDeleted = false
                        }
                    }
                };
                _adMatchingModel.Filtered.CampaignStopList = new Dictionary<int, CampaignStop>()
                {
                    {
                        0, new CampaignStop()
                        {
                            CampaignStopId = campaignStopId,
                            IsEnabled = true,
                            IsDeleted = false,
                            BeginStop = DateTime.UtcNow.AddHours(-1),
                            EndStop = DateTime.UtcNow.AddHours(1),
                            CampaignId = campaignId
                        }
                    },
                    {
                    1, new CampaignStop()
                    {
                        CampaignStopId = campaignStopId,
                        IsEnabled = true,
                        IsDeleted = false,
                        BeginStop = DateTime.UtcNow.AddHours(-1),
                        EndStop = DateTime.UtcNow.AddHours(1),
                        CampaignId = secondCampaignId
                    }
                }
                };
                break;
            case StopsScenario.Benchmarking_CampaignOutOfClientAdAccountStopHourNotInMainDictionaryAdded:
                GetAdMatchingModelTwoCampaign(campaignId, secondCampaignId, clientAdAccountId, false);
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>();
                _adMatchingModel.MainDictionaryEvaluated.ClientAdAccounts = new Dictionary<int, ClientAdAccount>();
                for (int i = 0; i < 10; i++)
                {
                    _adMatchingModel.Filtered.ClientAdAccounts = new Dictionary<int, ClientAdAccount>()
                    {
                        {
                            i, new ClientAdAccount()
                            {
                                ClientAdAccountId = clientAdAccountId,
                                IsEnabled = true,
                                IsDeleted = false
                            }
                        }
                    };
                    for (int j = 0; j < 10; j++)
                    {
                        _adMatchingModel.Filtered.ClientAccountStopList = new Dictionary<int, ClientAdAccountStop>()
                        {
                            {
                                i*10+j, new ClientAdAccountStop()
                                {
                                    ClientAdAccountId = clientAdAccountId,
                                    IsEnabled = true,
                                    IsDeleted = false,
                                    BeginStop = DateTime.UtcNow.AddHours(-(_random.Next()%4)),
                                    EndStop = DateTime.UtcNow.AddHours((_random.Next()%4)),
                                }
                            }
                        };   
                    }
                }
                break;
            case StopsScenario.Positive_CampaignOutOfClientAdAccountStopHourNotInMainDictionaryAdded:
                GetAdMatchingModelTwoCampaign(campaignId, secondCampaignId, clientAdAccountId, false);
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>();
                _adMatchingModel.MainDictionaryEvaluated.ClientAdAccounts = new Dictionary<int, ClientAdAccount>();
                _adMatchingModel.Filtered.ClientAdAccounts = new Dictionary<int, ClientAdAccount>()
                {
                    {
                        0, new ClientAdAccount()
                        {
                            ClientAdAccountId = clientAdAccountId,
                            IsEnabled = true,
                            IsDeleted = false
                        }
                    }
                };
                _adMatchingModel.Filtered.ClientAccountStopList = new Dictionary<int, ClientAdAccountStop>()
                {
                    {
                        0, new ClientAdAccountStop()
                        {
                            ClientAdAccountId = clientAdAccountId,
                            IsEnabled = true,
                            IsDeleted = false,
                            BeginStop = DateTime.UtcNow.AddHours(+1),
                            EndStop = DateTime.UtcNow.AddHours(+11),
                        }
                    }
                };
                break;
        }

        return this;
    }

    private void GetAdMatchingModelTwoCampaign(int campaignId, int secondCampaignId, int clientAdAccountId, bool addCampaignListForMainDictionaryEvaluated)
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

    private static Dictionary<int, Campaign> GetTwoCampaigns(int campaignId, int secondCampaignId, int clientAdAccountId) =>
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

public static class AdMatchingModel_StopsTestingExtensions
{
    public static bool ValidateStopsScenario(this AdMatchingModel adMatchingModel, StopsScenario capsScenario)
    {
        switch (capsScenario)
        {
            case StopsScenario.Negative_NoCampaignsProvided:
            case StopsScenario.Negative_AdMatchingModelIsNull:
            case StopsScenario.Negative_AdMatchingModelCampaignListIsNull:
            case StopsScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
            case StopsScenario.Negative_OneCampaignWithKeyButNoValue:
            case StopsScenario.Negative_ClientAdAccountsIsNull:
            {
                //in this case we just want to check no exception is throw, the handler should do nothing and just exit
                return true;
            }
            case StopsScenario.Positive_CampaignOutOfClientAdAccountStopHourNotInMainDictionaryAdded:
            {
                //in this case we just want to check no exception is throw, the handler should do nothing and just exit
                if (adMatchingModel.MainDictionaryEvaluated.CampaignsList is {Count: 2})
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            case StopsScenario.Positive_CampaignInStopHourInMainDictionaryRemoved:
            {
                //in this case we just want to check no exception is throw, the handler should do nothing and just exit
                if (adMatchingModel.MainDictionaryEvaluated.CampaignsList is {Count: 0})
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            default:
                //we should never hit this condition, the exit condition should always be covered by the switch statement non default entries
                return false;
        }
    }
}

public enum StopsScenario
{
    Negative_NoCampaignsProvided,
    Negative_AdMatchingModelIsNull,
    Negative_AdMatchingModelCampaignListIsNull,
    Negative_AdMatchingModelMainDictionaryEvaluatedIsNull,
    Negative_OneCampaignWithKeyButNoValue,
    Negative_ClientAdAccountsIsNull,
    Positive_CampaignOutOfClientAdAccountStopHourNotInMainDictionaryAdded,
    Positive_CampaignInStopHourInMainDictionaryRemoved,
    Benchmarking_CampaignOutOfClientAdAccountStopHourNotInMainDictionaryAdded
}