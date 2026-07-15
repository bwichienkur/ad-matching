using EDDY.IS.AdMatching.Core.Engines;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EDDY.IS.AdMatching.Core.ChainResponsability;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Core.RequestHandler;
using EDDY.IS.AdMatching.Core.Tests.LogAndAnalyzeHandlers;
using EDDY.IS.AdMatching.Core.Tests.Mocks;
using EDDY.IS.AdMatching.Core.Tests.Utilities;
using EDDY.IS.AdMatching.Domain.ChainResponsability;
using EDDY.IS.AdMatching.Domain.Dto;
using EDDY.IS.AdMatching.Domain.Models;
using EDDY.IS.AdMatching.Domain.Services;
using EDDY.IS.AdMatching.Domain.Services.Interfaces;
using EDDY.IS.AdMatching.Service;
using EDDY.IS.Common.ConstantsAndEnums;
using EDDY.IS.Common.Dto.RuleEngine;
using EDDY.IS.Common.Settings;
using EDDY.IS.RuleEngine.CustomRuleEngine;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NLog;
using NLog.Config;
using EDDY.IS.Common.Settings;

namespace EDDY.IS.AdMatching.Core.Tests.Engines
{
    [TestClass]
    public class CommonEngineTests
    {

        [DataTestMethod]
        [DataRow("Benchmarking")]
        public void CommonEngine_PerformanceBenchmarking_HappyPath(string testsCaseScenario)
        {
            //Arrange 

            TestingUtilities.GetSearchParameters();
            TestingUtilities.GetRandomIpAddress();
            var sourceId = 0;
            var mockDataManager = TestingUtilities.GetMockDataManager(sourceId);
            var cacheSettings = new OptionsWrapper<CacheSettings>(new CacheSettings());
            var debugLogger = new DebugLogger(null);
            var commonEngine = new CommonEngine(
                mockDataManager, 
                new MemoryCacheMock(), 
                cacheSettings,
                null, 
                debugLogger);

            var services = new ServiceCollection();
            services.AddSingleton(mockDataManager);
            services.AddTransient<IEngine, CommonEngine>();
            services.AddSingleton<IRuleEngine, RuleEngineMock>();

            services.Configure<LoggingDebugInformation>(ConfigureLoggingDebugInformationOptions);

            services.AddSingleton(debugLogger);
            services.AddSingleton<IEngine>(commonEngine);
            services.AddScoped<IAdMatchingService, AdMatchingService>();
            services.Chain<IChainHandler<AdMatchingModel>>()
                .Add<LogAndAnalyzeStopsHandler>()
                .Add<LogAndAnalyzeCapsHandler>()
                .Add<LogAndAnalyzeRuleEngineHandler>()
                .Add<LogAndAnalyzeSchedulingHandler>()
                .Add<LogAndAnalyzeResponseBuilderHandler>()
                .Add<LogAndAnalyzeDynamicBidVariablesHandler>()
                .Add<LogAndAnalyzeAdSortingHandler>()
                .Add<LogAndAnalyzeParametersHandler>()
                .Configure();

            var provider = services.BuildServiceProvider();
            var adMatchingService = provider.GetRequiredService<IAdMatchingService>();
            AdMatchingRequest request = new AdMatchingRequest()
            {
                MaxAds = 10,
                Parameters = new SourceValuesGenerator()
                    .AddRandomValues()
                    .AddIdOrFieldValues(IdOrField.Age, 15)
                    .Build(),
                SearchGuid = Guid.NewGuid().ToString(),
                SourceId = sourceId
            };
            
            //Act
            AdMatchingResponse? res = null;
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            int numberOfCycles = 10000;
            for (int i = 0; i < numberOfCycles; i++)
            {
                res = adMatchingService.GetAdsMatched(request);
            }
            stopWatch.Stop();
            
            //Assert
            //Assert.IsNotNull(res);
            //Assert.IsTrue(res.AdsMatched.Any()); 
            Trace.WriteLine($"##############Time per request: {stopWatch.ElapsedMilliseconds*1.0/numberOfCycles}ms");
            Trace.WriteLine($"##############Total time: {stopWatch.ElapsedMilliseconds}ms, per Number of requests:{numberOfCycles}");
            //Trace.WriteLine(res.AdsMatched.First().AdDescription);

            var chain = provider.GetRequiredService<IChainHandler<AdMatchingModel>>();
            var nextInChain = (IStopWatchable)chain;

            while (nextInChain != null)
            {
                var elapsedTimeInMilliseconds = nextInChain.GetElapsedTimeInMilliseconds;
                Trace.WriteLine($"\n##############Time per request to {nextInChain.DecoratedClass.GetType().Name}: {elapsedTimeInMilliseconds*1.0/numberOfCycles}ms");
                Trace.WriteLine($"##############Total time: {elapsedTimeInMilliseconds}ms, per Number of requests:{numberOfCycles}");
                Trace.WriteLine($"##############% time: {elapsedTimeInMilliseconds*100.0/stopWatch.ElapsedMilliseconds}%");
                nextInChain = (IStopWatchable)nextInChain.DecoratedClass.Next;
            }
        }
        
        
        [DataTestMethod]
        [DataRow(IdOrField.Age, 15, true)]
        [DataRow(IdOrField.Age, 10, false)]
        public void CommonEngine_GetAdsMatched_HappyPath(IdOrField idOrField, int age, bool success)
        {
            //Arrange 

            TestingUtilities.GetSearchParameters();
            TestingUtilities.GetRandomIpAddress();
            var sourceId = 0;
            var mockDataManager = TestingUtilities.GetMockDataManager(sourceId);
            var cacheSettings = new OptionsWrapper<CacheSettings>(new CacheSettings());
            var commonEngine = new CommonEngine(mockDataManager, new MemoryCacheMock(), cacheSettings,null, new DebugLogger(Options.Create<LoggingDebugInformation>(new LoggingDebugInformation(){EnabledTrueFalse = true})));

            var services = new ServiceCollection();
            services.AddSingleton(mockDataManager);
            services.AddTransient<IEngine, CommonEngine>();
            services.AddSingleton<IRuleEngine, RuleEngine.CustomRuleEngine.RuleEngine>();

            services.Configure<LoggingDebugInformation>(ConfigureLoggingDebugInformationOptions);

            services.AddSingleton<DebugLogger>();
            services.AddSingleton<IEngine>(commonEngine);
            services.AddScoped<IAdMatchingService, AdMatchingService>();
            services.Chain<IChainHandler<AdMatchingModel>>()
                .Add<StopsHandler>()
                .Add<CapsHandler>()
                .Add<RuleEngineHandler>()
                .Add<SchedulingHandler>()
                .Add<ResponseBuilderHandler>()
                .Add<DynamicBidVariablesHandler>()
                .Add<AdSortingHandler>()
                .Add<ParametersHandler>()
                .Configure();

            var provider = services.BuildServiceProvider();
            var adMatchingService = provider.GetRequiredService<IAdMatchingService>();
            AdMatchingRequest request = new AdMatchingRequest()
            {
                MaxAds = 10,
                Parameters = new SourceValuesGenerator()
                    .AddRandomValues()
                    .AddIdOrFieldValues(idOrField, age)
                    .Build(),
                SearchGuid = Guid.NewGuid().ToString(),
                SourceId = sourceId
            };
            
            //Act
            var res = adMatchingService.GetAdsMatched(request);

            //Assert
            Assert.IsNotNull(res);
            Assert.AreEqual(res.AdsMatched.Any(), success);
            if (success)
            {
                Trace.WriteLine(res.AdsMatched.First().AdDescription);
            }
        }

        private void ConfigureLoggingDebugInformationOptions(LoggingDebugInformation obj)
        {
            obj.EnabledTrueFalse = true;
        }
    }
    

    public class RuleEngineMock: IRuleEngine
    {
        public RuleEngineResult EvaluateRulesForDictionaryAndQueryBuilderFilterRule(Dictionary<string, string> sourceValues,
            QueryBuilderFilterRule queryBuilderFilterRule)
        {
            return new RuleEngineResult(new QueryBuilderFilterRule())
            {
                Pass = true
            };
        }
    }
}