using System;
using System.Collections.Generic;
using System.Linq;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Entities;
using EDDY.IS.AdMatching.Service;
using EDDY.IS.Common.Settings;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace EDDY.IS.AdMatching.Core.Tests.CommonEngineTests;

[TestClass]
public class CommonEngineTests
{
    [DataTestMethod]
    [DataRow(TargetingRuleTestScenario.HappyPath_NoParentCampaign)]
    [DataRow(TargetingRuleTestScenario.HappyPath_TwoCampaignsOneParentEverythingLinked)]
    [DataRow(TargetingRuleTestScenario.HappyPath_ThreeCampaignsOneParentEverythingLinked)]
    public void CommonEngine_Tests_NoParentCampaign(TargetingRuleTestScenario targetingRuleTestScenario)
    {
        //Arrange
        var commonEngine = new Core.Engines.CommonEngine(null, null, null, null, new DebugLogger(Options.Create<LoggingDebugInformation>(new LoggingDebugInformation(){EnabledTrueFalse = true})));

        var testInputs = new FilterRules_TestingGenerator()
            .AddScenario(targetingRuleTestScenario)
            .Build();

        //Act
        var targetingRules =
            commonEngine.FilterTargetingRules(
                testInputs.DictionaryContainerFiltered,
                testInputs.ContainerTargetingRules,
                testInputs.ContainerCampaignRelationshipList,
                testInputs.ContainerCampaignsList);

        //Assert
        Assert.IsTrue(targetingRules.ValidateTargetingRuleScenario(targetingRuleTestScenario, testInputs.RuleJsonText));
    }
}

public class FilterRules_TestingGenerator
{
    private TestInputs _testInputs = new TestInputs();

    public FilterRules_TestingGenerator()
    {
    }


    public TestInputs Build()
    {
        return _testInputs;
    }

    public FilterRules_TestingGenerator AddScenario(TargetingRuleTestScenario targetingRuleTestScenario)
    {
        var parentOfTheParentCampaignId = 3;
        var parentCampaignId = 2;
        var childCampaignId = 1;
        switch (targetingRuleTestScenario)
        {
            case TargetingRuleTestScenario.HappyPath_NoParentCampaign:
                var campaignId = 1;
                _testInputs.RuleJsonText = AddBasicRuleScenarioWithJsonValue(Guid.NewGuid().ToString());
                _testInputs.DictionaryContainerFiltered = new DictionaryContainer();
                _testInputs.ContainerTargetingRules = new TargetingRules_TestingGenerator()
                    .AddSingleTargetingRule(campaignId, _testInputs.RuleJsonText,false,false)
                    .Build();
                _testInputs.ContainerCampaignRelationshipList = new CampaignRelationship_TestingGenerator()
                    .Build();
                _testInputs.ContainerCampaignsList = new Campaign_TestingGenerator()
                    .AddSingleCampaign(campaignId)
                    .Build();
                _testInputs.DictionaryContainerFiltered.CampaignsList = new Campaign_TestingGenerator()
                    .AddSingleCampaign(campaignId)
                    .Build();
                break;
            case TargetingRuleTestScenario.HappyPath_TwoCampaignsOneParentEverythingLinked:
                _testInputs.RuleJsonText = AddBasicRuleScenarioWithJsonValue(Guid.NewGuid().ToString());
                _testInputs.DictionaryContainerFiltered = new DictionaryContainer();
                _testInputs.ContainerTargetingRules = new TargetingRules_TestingGenerator()
                    .AddSingleTargetingRule(childCampaignId, Guid.NewGuid().ToString(),false,false)
                    .AddSingleTargetingRule(parentCampaignId, _testInputs.RuleJsonText,false,false)
                    .Build();
                _testInputs.ContainerCampaignRelationshipList = new CampaignRelationship_TestingGenerator()
                    .AddChildParentRelationship(parentCampaignId,childCampaignId, true, true, true, true)
                    .Build();
                _testInputs.ContainerCampaignsList = new Campaign_TestingGenerator()
                    .AddSingleCampaign(parentCampaignId)
                    .AddSingleCampaign(childCampaignId)
                    .Build();
                _testInputs.DictionaryContainerFiltered.CampaignsList = new Campaign_TestingGenerator()
                    .AddSingleCampaign(parentCampaignId)
                    .AddSingleCampaign(childCampaignId)
                    .Build();
                break;
            case TargetingRuleTestScenario.HappyPath_ThreeCampaignsOneParentEverythingLinked:

                _testInputs.RuleJsonText = AddBasicRuleScenarioWithJsonValue(Guid.NewGuid().ToString());
                _testInputs.DictionaryContainerFiltered = new DictionaryContainer();
                _testInputs.ContainerTargetingRules = new TargetingRules_TestingGenerator()
                    .AddSingleTargetingRule(childCampaignId, Guid.NewGuid().ToString(),false,false)
                    .AddSingleTargetingRule(parentCampaignId, Guid.NewGuid().ToString(),false,false)
                    .AddSingleTargetingRule(parentOfTheParentCampaignId, _testInputs.RuleJsonText,false,false)
                    .Build();
                _testInputs.ContainerCampaignRelationshipList = new CampaignRelationship_TestingGenerator()
                    .AddChildParentRelationship(parentCampaignId,childCampaignId, true, true, true, true)
                    .AddChildParentRelationship(parentOfTheParentCampaignId,parentCampaignId, true, true, true, true)
                    .Build();
                _testInputs.ContainerCampaignsList = new Campaign_TestingGenerator()
                    .AddSingleCampaign(parentCampaignId)
                    .AddSingleCampaign(parentOfTheParentCampaignId)
                    .AddSingleCampaign(childCampaignId)
                    .Build();
                _testInputs.DictionaryContainerFiltered.CampaignsList = new Campaign_TestingGenerator()
                    .AddSingleCampaign(parentOfTheParentCampaignId)
                    .AddSingleCampaign(parentCampaignId)
                    .AddSingleCampaign(childCampaignId)
                    .Build();
                break;
                
        }

        return this;
    }

    private string AddBasicRuleScenarioWithJsonValue(string stringValue)
    {
        return "{\"condition\":\"AND\",\"rules\":[],\"valid\":true,\"extraValuePleaseIgnore\":\""+stringValue+"\" }";
    }
}

public static class TargetingRules_TestingExtensions
{
    public static bool ValidateTargetingRuleScenario(this Dictionary<int, TargetingRule> targetingRuleDictionary,
        TargetingRuleTestScenario targetingRuleScenario, string expectedJsonText)
    {
        switch (targetingRuleScenario)
        {
            case TargetingRuleTestScenario.HappyPath_NoParentCampaign:
                if (
                    targetingRuleDictionary.Count == 1
                    && targetingRuleDictionary.First().Value.RuleJson.Equals(expectedJsonText)
                )
                    return true;
                break;
            case TargetingRuleTestScenario.HappyPath_TwoCampaignsOneParentEverythingLinked:
                if (
                    targetingRuleDictionary.Count == 2
                    && targetingRuleDictionary.Values.Count(tr=>tr.RuleJson.Equals(expectedJsonText)) == 2
                )
                    return true;
                break;
            case TargetingRuleTestScenario.HappyPath_ThreeCampaignsOneParentEverythingLinked:
                if (
                    targetingRuleDictionary.Count == 3
                    && targetingRuleDictionary.Values.Count(tr=>tr.RuleJson.Equals(expectedJsonText)) == 3
                )
                    return true;
                break;            
        }

        return false;
    }
}

public class TestInputs
{
    public DictionaryContainer DictionaryContainerFiltered;
    public Dictionary<int,TargetingRule> ContainerTargetingRules;
    public Dictionary<int,CampaignRelationship> ContainerCampaignRelationshipList;
    public Dictionary<int,Campaign> ContainerCampaignsList;
    public string RuleJsonText { get; set; }
}

public class Campaign_TestingGenerator
{
    private Dictionary<int, Campaign> _campaigns = new Dictionary<int, Campaign>();

    public Campaign_TestingGenerator()
    {
    }


    public Dictionary<int, Campaign> Build()
    {
        return _campaigns;
    }

    public Campaign_TestingGenerator AddSingleCampaign(int campaignId)
    {
        _campaigns.Add(_campaigns.Count, new Campaign()
        {
            IsDeleted = false,
            IsEnabled = true,
            CampaignId = campaignId
        });
        return this;
    }
}

public class CampaignRelationship_TestingGenerator
{
    private Dictionary<int, CampaignRelationship> _campaignRelationships = new Dictionary<int, CampaignRelationship>();

    public CampaignRelationship_TestingGenerator()
    {
    }
    
    public Dictionary<int, CampaignRelationship> Build()
    {
        return _campaignRelationships;
    }
    
    public CampaignRelationship_TestingGenerator AddChildParentRelationship(int parentCampaignId, int childCampaignId,
        bool linkParentSources, bool linkParentOptimizations, bool linkParentDynamicBidVariables, bool linkParentRules)
    {
        _campaignRelationships.Add(_campaignRelationships.Count, new CampaignRelationship()
        {
            IsDeleted = false,
            CampaignId = childCampaignId,
            ParentCampaignId = parentCampaignId,
            LinkParentSources = linkParentSources,
            LinkParentOptimizations = linkParentOptimizations,
            LinkParentDynamicBidVariables = linkParentDynamicBidVariables,
            LinkParentRules = linkParentRules
        });
        return this;    
    }
}

public class TargetingRules_TestingGenerator
{
    private Dictionary<int, TargetingRule> _targetingRules = new Dictionary<int, TargetingRule>();

    public TargetingRules_TestingGenerator()
    {
    }


    public Dictionary<int, TargetingRule> Build()
    {
        foreach (var targetingRule in _targetingRules)
        {
            if (string.IsNullOrWhiteSpace(targetingRule.Value.RuleJson))
            {
                targetingRule.Value.RuleJson =
                    JsonConvert.SerializeObject(targetingRule.Value.RuleAsQueryBuilderFilterRule);
            }
        }
        return _targetingRules;
    }

    public TargetingRules_TestingGenerator AddSingleTargetingRule(int campaignId, string ruleJsonText,
        bool optimizationRule, bool dynamicBidRule)
    {
        _targetingRules.Add(_targetingRules.Count, new TargetingRule()
        {
            IsDeleted = false,
            IsEnabled = true,
            RuleJson = ruleJsonText,
            CampaignId = campaignId,
            IsOptimization = optimizationRule,
            IsDynamicBid = dynamicBidRule
        });
        return this;
    }
}

public enum TargetingRuleTestScenario
{
    HappyPath_NoParentCampaign,
    HappyPath_TwoCampaignsOneParentEverythingLinked,
    HappyPath_ThreeCampaignsOneParentEverythingLinked
}

