using System.Collections.Generic;
using System.Linq;
using EDDY.IS.AdMatching.Domain.Services.Interfaces;
using EDDY.IS.AdMatching.Entities;
using EDDY.IS.Common.ConstantsAndEnums;
using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.AdMatching.Core.Tests.Utilities
{
    internal static class TestingUtilities
    {
        internal static Dictionary<string, string> GetSearchParameters()
        {
            var res = new Dictionary<string, string>();

            return res;
        }

        internal static IDataManager GetMockDataManager(int sourceId1)
        {
            var mockDataManager = new MockDataManager();

            var campaignId = 0;
            var clientAdAccountId = 0;
            int productTypeId = 21;

            
            var vwAdsAm = new VwAdsAm()
            {
                Description = "Unit test Ad",
                CampaignId = campaignId,
                CampaignName = "TestName for the campaign",
                AdGroupName = "Test AdGroup name for the campaign"
            };
            var clientAdAccount = new ClientAdAccount()
            {
                ClientAdAccountId = clientAdAccountId,
                IsEnabled = true,
                IsDeleted = false,
                Status = "Enabled",
            };
            var targetingRules = new List<TargetingRule>() {
                new TargetingRule()
                {
                    CampaignId = campaignId,
                    IsOptimization = false,
                    IsDeleted = false,
                    IsEnabled = true,
                    RuleJson = @"
{
	""condition"": ""AND"",
	""rules"": [		
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""Age"",
					""field"": ""Age"",
					""type"": ""string"",
					""input"": ""number"",
					""operator"": ""is_greater_than"",
					""value"": [
						""14""
					]
				},
				{
					""id"": ""AcceptMissingKey"",
					""field"": ""AcceptMissingKey"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""If this variable is unknown, the click will be accepted.""
					],
					""data"": {
						""forceEnd"": ""true""
					}
				}
			]
		}
	],
	""valid"": true
}"
                }
            };
            var campaign1 = new Campaign()
            {
                CampaignId = 0,
                IsCapped = false,
                TargetingRules = targetingRules,
                ProductTypeId = productTypeId,
                IsEnabled = true,
                IsDeleted = false
            };
            var campaign2 = new Campaign()
            {
                CampaignId = 1,
                IsCapped = false,
                TargetingRules = targetingRules,
                ProductTypeId = productTypeId,
                IsEnabled = true,
                IsDeleted = false
            };
            campaign1.CampaignSources.Add(new CampaignSource()
            {
                SourceId = sourceId1,
                CampaignId = 0,
                IsEnabled = true,
                IsDeleted = false
            });
            campaign1.ClientAdAccountId = clientAdAccountId;
            campaign2.CampaignSources.Add(new CampaignSource()
            {
                SourceId = sourceId1,
                CampaignId = 1,
                IsEnabled = true,
                IsDeleted = false
            });
            campaign2.ClientAdAccountId = clientAdAccountId;
            var campaignCap = new CampaignCap()
            {
                CampaignId = campaignId,
            };
            var adGroupId = 0;
            var adId = 0;
            var adGroup = new AdGroup()
            {
                CampaignId = campaignId,
                AdGroupId = adGroupId,
                IsEnabled = true,
                IsDeleted = false,
                Name = $"AdGroupName{adGroupId}"
            };
            var adGroupAd = new AdGroupAd()
            {
                AdGroupId = adGroupId,
                AdId = adId,
                IsEnabled = true,
                IsDeleted = false,
            };

            mockDataManager.DictionaryContainer.AdGroupAdList = new Dictionary<int, AdGroupAd>();
            mockDataManager.DictionaryContainer.AdGroupAdList.Add(0, adGroupAd);

            mockDataManager.DictionaryContainer.CampaignRelationshipList = new Dictionary<int, CampaignRelationship>();
            mockDataManager.DictionaryContainer.CampaignRelationshipList.Add(0,new CampaignRelationship()
            {
                ParentCampaignId = 1,
                CampaignId = 0,
                Campaign = campaign2,
                IsDeleted = false,
            });

            mockDataManager.DictionaryContainer.AdGroupList = new Dictionary<int, AdGroup>();
            mockDataManager.DictionaryContainer.AdGroupList.Add(0, adGroup);

            mockDataManager.DictionaryContainer.AdsAMSList = new Dictionary<int, VwAdsAm>();
            mockDataManager.DictionaryContainer.AdsAMSList.Add(campaignId, vwAdsAm);
            
            

            mockDataManager.DictionaryContainer.SourceProductTypeList = new Dictionary<int, SourceProductType>();
            
            mockDataManager.DictionaryContainer.CampaignCapList = new Dictionary<int, CampaignCap>();
            mockDataManager.DictionaryContainer.CampaignCapList.Add(0, campaignCap);
            mockDataManager.DictionaryContainer.CampaignCapList.Add(1, campaignCap);

            mockDataManager.DictionaryContainer.CampaignSchedulerList =
                new Dictionary<int, CampaignSchedule>();


            mockDataManager.DictionaryContainer.CampaignsList = new Dictionary<int, Campaign>();
            mockDataManager.DictionaryContainer.CampaignsList.Add(campaignId, campaign1);
            mockDataManager.DictionaryContainer.CampaignsList.Add(1, campaign2);


            mockDataManager.DictionaryContainer.CampaignSourceList = new Dictionary<int, CampaignSource>();
            mockDataManager.DictionaryContainer.CampaignSourceList.Add(0, new CampaignSource()
            {
                Campaign = campaign1,
                CampaignId = campaign1.CampaignId,
                SourceId = sourceId1,
                IsEnabled = true,
                IsDeleted = false
            });
            mockDataManager.DictionaryContainer.CampaignSourceList.Add(1, new CampaignSource()
            {
                Campaign = campaign2,
                CampaignId = campaign2.CampaignId,
                SourceId = sourceId1,
                IsEnabled = true,
                IsDeleted = false
            });

            mockDataManager.DictionaryContainer.CampaignSpendList = new Dictionary<int, CampaignSpend>();
            mockDataManager.DictionaryContainer.CampaignStopList = new Dictionary<int, CampaignStop>();
            mockDataManager.DictionaryContainer.ClientAccountStopList =
                new Dictionary<int, ClientAdAccountStop>();

            mockDataManager.DictionaryContainer.SourceProductTypeList = new Dictionary<int, SourceProductType>()
            {
                {
                    0, new SourceProductType()
                    {
                        ProductTypeId = productTypeId,
                        IsEnabled = true,
                    }
                }
            };
            
            mockDataManager.DictionaryContainer.ClientAdAccountBudgetList =
                new Dictionary<int, ClientAdAccountBudget>()
                {
                    {0, new ClientAdAccountBudget()
                    {
                        ProductTypeId = productTypeId,
                        ClientAdAccountId = clientAdAccountId,
                        IsCapped = false,
                        IsEnabled = true,
                        IsDeleted = false,
                    }}
                };
            
            mockDataManager.DictionaryContainer.ClientAdAccountDefaultParameterList =
                new Dictionary<int, ClientAdAccountDefaultParameter>();
            mockDataManager.DictionaryContainer.ClientAdAccountParameterList =
                new Dictionary<int, ClientAdAccountParameter>();


            mockDataManager.DictionaryContainer.ClientAdAccounts = new Dictionary<int, ClientAdAccount>();
            mockDataManager.DictionaryContainer.ClientAdAccounts.Add(campaignId, clientAdAccount);

            mockDataManager.DictionaryContainer.ClientAdAccountSpendList =
                new Dictionary<int, ClientAdAccountSpend>();
            mockDataManager.DictionaryContainer.ScheduleOptionList = new Dictionary<int, ScheduleOption>();

            mockDataManager.DictionaryContainer.TargetingRules = new Dictionary<int, TargetingRule>()
            {
                {0, targetingRules.First()}
            };

            return mockDataManager;
        }

        internal static string GetRandomIpAddress()
        {
            return "200.200.200.200";
        }

        internal static QueryBuilderFilterRule GetQueryBuilderFilterRuleForSearch()
        {
            return new QueryBuilderGenerator()
                .AddIntOperatorQueryBuilder(IdOrField.Age, "15", Operator.Is_Greater_Than)
                .Build();
        }
    }
}