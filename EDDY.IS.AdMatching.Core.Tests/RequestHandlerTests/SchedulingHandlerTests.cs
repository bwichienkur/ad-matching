using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using TimeZone = EDDY.IS.AdMatching.Entities.TimeZone;

namespace EDDY.IS.AdMatching.Core.Tests.RequestHandlerTests;

[TestClass]
public class SchedulingHandlerTests
{
    private DebugLogger _debugLogger =
        new DebugLogger(
            Options.Create<LoggingDebugInformation>(new LoggingDebugInformation() {EnabledTrueFalse = false}));

    [DataTestMethod]
    [DataRow(SchedulingScenario.Negative_NoCampaignsProvided)]
    [DataRow(SchedulingScenario.Negative_AdMatchingModelIsNull)]
    [DataRow(SchedulingScenario.Negative_AdMatchingModelCampaignListIsNull)]
    [DataRow(SchedulingScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull)]
    [DataRow(SchedulingScenario.Negative_OneCampaignWithKeyButNoValue)]
    [DataRow(SchedulingScenario.Negative_ClientAdAccountsIsNull)]
    [DataRow(SchedulingScenario.Positive_HappyPath)]
    public void SchedulingHandler_Handle(SchedulingScenario capsScenario)
    {
        //Arrange
        var handler = new SchedulingHandler(null, _debugLogger);
        var model = new AdMatchingModel_SchedulingTestingGenerator()
            .Add(capsScenario)
            .Build();

        //Act
        handler.Handle(model);

        //Assert
        model.ValidateSchedulingScenario(capsScenario);
    }

    [TestMethod]
    public void SchedulingHandler__Benchmarking_Handle()
    {
        //Arrange
        var handler = new SchedulingHandler(null, _debugLogger);

        var stopWatch = new Stopwatch();
        var iterations = 10000;

        //Act
        stopWatch.Start();
        for (int i = 0; i < iterations; i++)
        {
            var model = new AdMatchingModel_SchedulingTestingGenerator()
                .Add(SchedulingScenario.Benchmarking_HappyPath)
                .Build();
            handler.Handle(model);
        }

        stopWatch.Stop();

        //Assert
        Trace.WriteLine(
            $"Time per iterations: {iterations}: {stopWatch.ElapsedMilliseconds}ms, timer per iteration: {stopWatch.ElapsedMilliseconds * 1.0 / iterations}ms");
    }
}

public class AdMatchingModel_SchedulingTestingGenerator
{
    public AdMatchingModel_SchedulingTestingGenerator()
    {
    }


    public AdMatchingModel Build()
    {
        return _adMatchingModel;
    }

    public AdMatchingModel _adMatchingModel { get; set; }

    public AdMatchingModel_SchedulingTestingGenerator Add(SchedulingScenario capsScenario)
    {
        var campaignId = 10;
        var campaignIdSecond = 12;
        int clientAdAccountId = 8;
        int campaignStopId = 4;
        int timeZoneId = 12;
        int scheduleOptionId = 22;
        int campaignScheduleIdSecond = 23;
        int campaignScheduleId = 45;

        switch (capsScenario)
        {
            case SchedulingScenario.Negative_AdMatchingModelIsNull:
                _adMatchingModel = null;
                break;

            case SchedulingScenario.Negative_NoCampaignsProvided:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                break;

            case SchedulingScenario.Negative_AdMatchingModelCampaignListIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = null;
                break;

            case SchedulingScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = null;
                break;

            case SchedulingScenario.Negative_OneCampaignWithKeyButNoValue:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;
            case SchedulingScenario.Negative_ClientAdAccountsIsNull:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.ClientAdAccounts = null;
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList = new Dictionary<int, Campaign>()
                {
                    {0, null}
                };
                break;
            case SchedulingScenario.Benchmarking_HappyPath:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.ClientAdAccounts = null;
                for (int i = 0; i < 100; i++)
                {
                    var firstCampaignIdFor = campaignId + i;
                    var secondCampaignIdFor = 1000 + campaignIdSecond + i;
                    _adMatchingModel.MainDictionaryEvaluated.CampaignsList =
                        GetTwoCampaigns(firstCampaignIdFor, secondCampaignIdFor, clientAdAccountId, timeZoneId);
                    _adMatchingModel.Filtered.CampaignRelationshipList = new Dictionary<int, CampaignRelationship>()
                    {
                        {
                            0, new CampaignRelationship()
                            {
                                CampaignId = firstCampaignIdFor,
                                ParentCampaignId = secondCampaignIdFor,
                                LinkParentSchedule = true,
                                IsDeleted = false,
                            }
                        }
                    };

                    _adMatchingModel.Filtered.CampaignSchedulerList = new Dictionary<int, CampaignSchedule>()
                    {
                        {
                            i, new CampaignSchedule()
                            {
                                CampaignScheduleId = campaignScheduleId,
                                CampaignId = firstCampaignIdFor,
                                ScheduleOptionId = scheduleOptionId,
                                IsDeleted = false,
                                IsEnabled = true,
                                StartTime = TimeSpan.MinValue,
                                EndTime = TimeSpan.MaxValue,
                            }
                        },
                        {
                            1000+i, new CampaignSchedule()
                            {
                                CampaignScheduleId = campaignScheduleIdSecond,
                                CampaignId = secondCampaignIdFor,
                                ScheduleOptionId = scheduleOptionId,
                                IsDeleted = false,
                                IsEnabled = true,
                                StartTime = TimeSpan.MinValue,
                                EndTime = TimeSpan.MaxValue,
                            }
                        },
                    };
                    _adMatchingModel.MainDictionaryEvaluated.CampaignSchedulerList =
                        new Dictionary<int, CampaignSchedule>();
                    _adMatchingModel.Filtered.TimeZoneList = new Dictionary<int, TimeZone>()
                    {
                        {
                            i, new TimeZone()
                            {
                                Code = "Eastern Standard Time",
                                Name = "Eastern Standard Time",
                                IsDeleted = false,
                                IsEnabled = true,
                                TimeZoneId = timeZoneId,
                                Campaigns = new List<Campaign>()
                                {
                                    _adMatchingModel.MainDictionaryEvaluated.CampaignsList.ToArray()[0].Value,
                                    _adMatchingModel.MainDictionaryEvaluated.CampaignsList.ToArray()[1].Value
                                }
                            }
                        }
                    };
                    _adMatchingModel.Filtered.ScheduleOptionList = new Dictionary<int, ScheduleOption>()
                    {
                        {
                            i, new ScheduleOption()
                            {
                                ScheduleOptionId = scheduleOptionId,
                                DayOfWeek = "1,2,3,4,5,6,7",
                            }
                        }
                    };
                }

                break;
            case SchedulingScenario.Positive_HappyPath:
                _adMatchingModel = new AdMatchingModel();
                _adMatchingModel.Filtered = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
                _adMatchingModel.MainDictionaryEvaluated.ClientAdAccounts = null;
                _adMatchingModel.MainDictionaryEvaluated.CampaignsList =
                    GetTwoCampaigns(campaignId, campaignIdSecond, clientAdAccountId, timeZoneId);
                _adMatchingModel.Filtered.CampaignRelationshipList = new Dictionary<int, CampaignRelationship>()
                {
                    {
                        0, new CampaignRelationship()
                        {
                            CampaignId = campaignId,
                            ParentCampaignId = campaignIdSecond,
                            LinkParentSchedule = true,
                            IsDeleted = false,
                        }
                    }
                };

                _adMatchingModel.Filtered.CampaignSchedulerList = new Dictionary<int, CampaignSchedule>()
                {
                    {
                        0, new CampaignSchedule()
                        {
                            CampaignScheduleId = campaignScheduleId,
                            CampaignId = campaignId,
                            ScheduleOptionId = scheduleOptionId,
                            IsDeleted = false,
                            IsEnabled = true,
                            StartTime = TimeSpan.MinValue,
                            EndTime = TimeSpan.MaxValue,
                        }
                    },
                    {
                        1, new CampaignSchedule()
                        {
                            CampaignScheduleId = campaignScheduleIdSecond,
                            CampaignId = campaignIdSecond,
                            ScheduleOptionId = scheduleOptionId,
                            IsDeleted = false,
                            IsEnabled = true,
                            StartTime = TimeSpan.MinValue,
                            EndTime = TimeSpan.MaxValue,
                        }
                    },
                };
                _adMatchingModel.MainDictionaryEvaluated.CampaignSchedulerList =
                    new Dictionary<int, CampaignSchedule>();
                _adMatchingModel.Filtered.TimeZoneList = new Dictionary<int, TimeZone>()
                {
                    {
                        0, new TimeZone()
                        {
                            Code = "Eastern Standard Time",
                            Name = "Eastern Standard Time",
                            IsDeleted = false,
                            IsEnabled = true,
                            TimeZoneId = timeZoneId,
                            Campaigns = new List<Campaign>()
                            {
                                _adMatchingModel.MainDictionaryEvaluated.CampaignsList.ToArray()[0].Value,
                                _adMatchingModel.MainDictionaryEvaluated.CampaignsList.ToArray()[1].Value
                            }
                        }
                    }
                };
                _adMatchingModel.Filtered.ScheduleOptionList = new Dictionary<int, ScheduleOption>()
                {
                    {
                        0, new ScheduleOption()
                        {
                            ScheduleOptionId = scheduleOptionId,
                            DayOfWeek = "1,2,3,4,5,6,7",
                        }
                    }
                };
                break;
        }

        return this;
    }

    private void GetAdMatchingModelTwoCampaign(int campaignId, int secondCampaignId, int clientAdAccountId,
        bool addCampaignListForMainDictionaryEvaluated, int timeZoneId)
    {
        _adMatchingModel = new AdMatchingModel();
        _adMatchingModel.Filtered = new DictionaryContainer();
        _adMatchingModel.MainDictionaryEvaluated = new DictionaryContainer();
        if (addCampaignListForMainDictionaryEvaluated)
        {
            _adMatchingModel.MainDictionaryEvaluated.CampaignsList =
                GetTwoCampaigns(campaignId, secondCampaignId, clientAdAccountId, timeZoneId);
        }

        _adMatchingModel.Filtered.CampaignsList =
            GetTwoCampaigns(campaignId, secondCampaignId, clientAdAccountId, timeZoneId);
    }

    private static Dictionary<int, Campaign> GetTwoCampaigns(int campaignId, int secondCampaignId,
        int clientAdAccountId, int timeZoneId) =>
        new Dictionary<int, Campaign>()
        {
            {
                campaignId, new Campaign()
                {
                    CampaignId = campaignId,
                    ClientAdAccountId = clientAdAccountId,
                    TimeZoneId = timeZoneId,
                }
            },
            {
                secondCampaignId, new Campaign()
                {
                    CampaignId = secondCampaignId,
                    ClientAdAccountId = clientAdAccountId,
                    TimeZoneId = timeZoneId,
                }
            }
        };
}

public static class AdMatchingModel_SchedulingTestingExtensions
{
    public static bool ValidateSchedulingScenario(this AdMatchingModel adMatchingModel, SchedulingScenario capsScenario)
    {
        switch (capsScenario)
        {
            case SchedulingScenario.Negative_NoCampaignsProvided:
            case SchedulingScenario.Negative_AdMatchingModelIsNull:
            case SchedulingScenario.Negative_AdMatchingModelCampaignListIsNull:
            case SchedulingScenario.Negative_AdMatchingModelMainDictionaryEvaluatedIsNull:
            case SchedulingScenario.Negative_OneCampaignWithKeyButNoValue:
            case SchedulingScenario.Negative_ClientAdAccountsIsNull:
            {
                //in this case we just want to check no exception is throw, the handler should do nothing and just exit
                return true;
            }
            case SchedulingScenario.Positive_HappyPath:
            {
                Assert.IsTrue(adMatchingModel.MainDictionaryEvaluated.CampaignSchedulerList is {Count: 2});
                return true;
            }
            default:
                //we should never hit this condition, the exit condition should always be covered by the switch statement non default entries
                Assert.Fail("Should never happen");
                return false;
        }
    }
}

public enum SchedulingScenario
{
    Negative_NoCampaignsProvided,
    Negative_AdMatchingModelIsNull,
    Negative_AdMatchingModelCampaignListIsNull,
    Negative_AdMatchingModelMainDictionaryEvaluatedIsNull,
    Negative_OneCampaignWithKeyButNoValue,
    Negative_ClientAdAccountsIsNull,
    Positive_HappyPath,
    Benchmarking_HappyPath
}