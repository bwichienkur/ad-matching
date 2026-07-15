using EDDY.IS.AdMatching.Core.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Diagnostics;
using EDDY.IS.Common.ConstantsAndEnums;
using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.AdMatching.Core.Tests.Engines
{
    [TestClass]
    public class RuleEngine_MultipleInputsOperatorTests
    {

        
        [DataTestMethod]
        [DataRow("Campus Only")]
        [DataRow("Campus Only|Online")]
        [DataRow("NotValid|Online")]
        public void RuleEngine_JsonFromGlassPanelMultipleInputs_PassTrue(string value)
        {
            //arrange
            var ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
            var sourceValues = new SourceValuesGenerator()
                .AddRandomValues()
                .AddIdOrFieldValues(IdOrField.LearningPreference, value)
                .Build();

            var queryBuilderFilterRule = JsonConvert.DeserializeObject<QueryBuilderFilterRule>(@"
{
	""condition"": ""AND"",
	""rules"": [		
		{
			""condition"": ""AND"",
			""rules"": [
				{
					""id"": ""LearningPreference"",
					""field"": ""LearningPreference"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Campus Only"",
						""Campus or Online"",
						""Online""
					]
				},
				{
					""id"": ""AcceptMissingKey"",
					""field"": ""AcceptMissingKey"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
					],
					""data"": {
						""forceEnd"": ""true""
					}
				}
			]
		}
	],
	""valid"": true
}");

            //act
            var res = ruleEngine.EvaluateRulesForDictionaryAndQueryBuilderFilterRule(sourceValues, queryBuilderFilterRule);

            //assert
            Assert.IsNotNull(res);
            Assert.IsTrue(string.IsNullOrEmpty(res.ReasonDidntPass), res.ReasonDidntPass);
            Assert.IsTrue(res.Pass);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("|")]
        [DataRow("NotValid|Blanline")]
        public void RuleEngine_JsonFromGlassPanelMultipleInputsIsNot_PassTrue(string value)
        {
	        //arrange
	        var ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
	        var sourceValues = new SourceValuesGenerator()
		        .AddRandomValues()
		        .AddIdOrFieldValues(IdOrField.LearningPreference, value)
		        .Build();

	        var queryBuilderFilterRule = JsonConvert.DeserializeObject<QueryBuilderFilterRule>(@"
{
	""condition"": ""AND"",
	""rules"": [		
		{
			""condition"": ""AND"",
			""rules"": [
				{
					""id"": ""LearningPreference"",
					""field"": ""LearningPreference"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is_not"",
					""value"": [
						""Campus Only"",
						""Campus or Online"",
						""Online""
					]
				},
				{
					""id"": ""AcceptMissingKey"",
					""field"": ""AcceptMissingKey"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
					],
					""data"": {
						""forceEnd"": ""true""
					}
				}
			]
		}
	],
	""valid"": true
}");

	        //act
	        var res = ruleEngine.EvaluateRulesForDictionaryAndQueryBuilderFilterRule(sourceValues, queryBuilderFilterRule);

	        //assert
	        Assert.IsNotNull(res);
	        Assert.IsTrue(string.IsNullOrEmpty(res.ReasonDidntPass), res.ReasonDidntPass);
	        Assert.IsTrue(res.Pass);
        }
    }
}
