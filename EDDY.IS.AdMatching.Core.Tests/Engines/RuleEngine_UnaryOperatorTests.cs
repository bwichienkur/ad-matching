using EDDY.IS.AdMatching.Core.Tests.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Diagnostics;
using EDDY.IS.Common.ConstantsAndEnums;
using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.AdMatching.Core.Tests.Engines
{
    [TestClass]
    public class RuleEngine_UnaryOperatorTests
    {

        
        [DataTestMethod]
        [DataRow("15")]
        [DataRow(null)]
        [DataRow("")]
        public void RuleEngine_JsonFromGlassPanel15AgeGreatherThan_PassTrue(object age)
        {
            //arrange
            var ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
            var sourceValues = new SourceValuesGenerator()
                .AddRandomValues()
                .AddAgeValues((string)age)
                .Build();

            var queryBuilderFilterRule = JsonConvert.DeserializeObject<QueryBuilderFilterRule>(@"
{
	""condition"": ""AND"",
	""rules"": [		
		{
			""condition"": ""AND"",
			""rules"": [
				{
					""id"": ""Age"",
					""field"": ""Age"",
					""type"": ""string"",
					""input"": ""number"",
					""operator"": ""is_greater_than"",
					""value"": [
						""1""
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
}");

            //act
            var res = ruleEngine.EvaluateRulesForDictionaryAndQueryBuilderFilterRule(sourceValues, queryBuilderFilterRule);

            //assert
            Assert.IsNotNull(res);
            Assert.IsTrue(string.IsNullOrEmpty(res.ReasonDidntPass), res.ReasonDidntPass);
            Assert.IsTrue(res.Pass);
        }

        [DataTestMethod]
        [DataRow(IdOrField.Age, 15, Operator.Is_Greater_Than, 14)]
        [DataRow(IdOrField.Age, 15, Operator.Is_Less_Than, 16)]
        [DataRow(IdOrField.Zip,"Zip90210", Operator.Is, "ZIP90210,Zip90211,zIp90212")]
        [DataRow(IdOrField.Zip, "ZipNot90310", Operator.Is_Not, "ZIP90210,Zip90211,zIp90212")]
        [DataRow(IdOrField.AreaOfStudy, "Fine Arts & Design", Operator.Is, new string[3] { "Fine Arts & Design", "Business", "Technology" })]
        [DataRow(IdOrField.AreaOfStudy, "Fine Arts & Design", Operator.Is_Not, new string[2] { "Business", "Technology" })]
        [DataRow(IdOrField.Browser, "IE", Operator.Is, new string[2] { "IE", "Firefox" })]
        [DataRow(IdOrField.BrowserPlatform, "Android", Operator.Is, new string[2] { "Android", "Windows" })]
        [DataRow(IdOrField.CollegeCredits, "1 - 29 Credits", Operator.Is, new string[2] { "1 - 29 Credits", "30 - 59 Credits" })]
        [DataRow(IdOrField.ConnectionSource, "University", Operator.Is, new string[2] { "University", "University" })]
        [DataRow(IdOrField.ConnectionType, "Cellular", Operator.Is, new string[2] { "Cellular", "Dialup" })]
        [DataRow(IdOrField.Country, "United States", Operator.Is, new string[2] { "United States", "Other" })]
        [DataRow(IdOrField.DegreeLevel, "Associate", Operator.Is, new string[2] { "Associate", "Course" })]
        [DataRow(IdOrField.DeviceType, "Mobile", Operator.Is, new string[2] { "Mobile", "Other" })]
        [DataRow(IdOrField.EducationLevel, "Bachelor", Operator.Is, new string[2] { "Bachelor", "Master" })]
        [DataRow(IdOrField.Gender, "Male", Operator.Is, new string[2] { "Male", "Female" })]
        [DataRow(IdOrField.HighSchoolGPA, "2.0 - 2.4", Operator.Is, new string[2] { "2.0 - 2.4", "2.5-2.74" })]
        [DataRow(IdOrField.Income, "$20,000 - $40,000", Operator.Is, new string[2] { "$20,000 - $40,000", "$40,000 - $60,000" })]
        [DataRow(IdOrField.LearningPreference, "Campus Only", Operator.Is, new string[2] { "Campus Only", "Online" })]
        [DataRow(IdOrField.MarketingUnits, "Degree PPC (GDN)", Operator.Is, new string[2] { "Degree PPC (GDN)", "Financial Aid (GDN)" })]
        [DataRow(IdOrField.MilitaryAffiliation, "Yes", Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.MilitaryBranch, "No Military Affiliation", Operator.Is, new string[2] { "No Military Affiliation", "AF - Active Duty(AD)" })]
        [DataRow(IdOrField.PlannedStart, "1 - 3 Months", Operator.Is, new string[2] { "1 - 3 Months", "4 - 6 Months" })]
        [DataRow(IdOrField.RegisteredNurse, "Yes", Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.State, "AL - Alabama", Operator.Is, new string[2] { "AL - Alabama", "TX - Texas" })]
        [DataRow(IdOrField.SubChannels, "Internal Data Purchase Agreements", Operator.Is, new string[2] { "Internal Data Purchase Agreements", "External Data Purchase Agreements" })]
        [DataRow(IdOrField.Subject, "Fine Art", Operator.Is, new string[2] { "Fine Art", "Interior Design" })]
        [DataRow(IdOrField.TeachingCert, "Yes", Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.TrackingTags, "Tag1", Operator.Is, "Tag1,Tag2,Tag3")]
        [DataRow(IdOrField.USCitizen, "Yes", Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.Browser, "Chrome", Operator.Is_Not, new string[2] { "IE", "Firefox" })]
        [DataRow(IdOrField.BrowserPlatform, "Linux", Operator.Is_Not, new string[2] { "Android", "Windows" })]
        [DataRow(IdOrField.CollegeCredits, "NoCredits", Operator.Is_Not, new string[2] { "1 - 29 Credits", "30 - 59 Credits" })]
        [DataRow(IdOrField.ConnectionSource, "NotUniversity", Operator.Is_Not, new string[2] { "University", "University" })]
        [DataRow(IdOrField.ConnectionType, "Broadband", Operator.Is_Not, new string[2] { "Cellular", "Dialup" })]
        [DataRow(IdOrField.Country, "Europe", Operator.Is_Not, new string[2] { "United States", "Other" })]
        [DataRow(IdOrField.DegreeLevel, "Bachelor", Operator.Is_Not, new string[2] { "Associate", "Course" })]
        [DataRow(IdOrField.DeviceType, "Tablet", Operator.Is_Not, new string[2] { "Mobile", "Other" })]
        [DataRow(IdOrField.EducationLevel, "Doctorate", Operator.Is_Not, new string[2] { "Bachelor", "Master" })]
        [DataRow(IdOrField.Gender, "Non Binary", Operator.Is_Not, new string[2] { "Male", "Female" })]
        [DataRow(IdOrField.HighSchoolGPA, "Over 6000", Operator.Is_Not, new string[2] { "2.0 - 2.4", "2.5-2.74" })]
        [DataRow(IdOrField.Income, "$60,000 - $80,000", Operator.Is_Not, new string[2] { "$20,000 - $40,000", "$40,000 - $60,000" })]
        [DataRow(IdOrField.LearningPreference, "Campus or Online", Operator.Is_Not, new string[2] { "Campus Only", "Online" })]
        [DataRow(IdOrField.MarketingUnits, "Not Valid", Operator.Is_Not, new string[2] { "Degree PPC (GDN)", "Financial Aid (GDN)" })]
        [DataRow(IdOrField.MilitaryAffiliation, "Other", Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.MilitaryBranch, "InvalidOption", Operator.Is_Not, new string[2] { "No Military Affiliation", "AF - Active Duty(AD)" })]
        [DataRow(IdOrField.PlannedStart, "10 - 30 Months", Operator.Is_Not, new string[2] { "1 - 3 Months", "4 - 6 Months" })]
        [DataRow(IdOrField.RegisteredNurse, "Other", Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.State, "CA - California", Operator.Is_Not, new string[2] { "AL - Alabama", "TX - Texas" })]
        [DataRow(IdOrField.SubChannels, "Transitory Data Purchase Agreements", Operator.Is_Not, new string[2] { "Internal Data Purchase Agreements", "External Data Purchase Agreements" })]
        [DataRow(IdOrField.Subject, "Sublime Art", Operator.Is_Not, new string[2] { "Fine Art", "Interior Design" })]
        [DataRow(IdOrField.TeachingCert, "Maybe", Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.TrackingTags, "Tag10", Operator.Is_Not, "Tag1,Tag2,Tag3")]
        [DataRow(IdOrField.USCitizen, "Yes", Operator.Is_Not, new string[1] {  "No" })]
        [DataRow(IdOrField.SiteUrl, "http://url1", Operator.Is, new string[1] {  "http://url1" })]
        [DataRow(IdOrField.SiteUrl, "http://url18", Operator.Is_Not, new string[1] {  "http://url1" })]
        [DataRow(IdOrField.SiteUrl, "http://url111/test/moreurl", Operator.Contains_One_Of, new string[1] {  "url111,zzz,JJJ" })]
        [DataRow(IdOrField.SiteUrl, "http://url111/test/moreurl", Operator.Not_Contains_One_Of, new string[1] {  "222,333,/testing,zzz,JJJ" })]
        /*Happy path method, condition for the operator is met*/
        public void RuleEngine_IdOrFieldOperator_PassTrue(IdOrField idOrField, object referenceValue, Operator op, object compareValue)
        {
            //arrange
            var ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
            var sourceValues = new SourceValuesGenerator()
                .AddRandomValues()
                .AddIdOrFieldValues(idOrField, referenceValue.ToString())
                .Build();

            var queryBuilderFilterRule = new QueryBuilderGenerator()
                .AddIdOrFieldOperatorThan(idOrField, op, compareValue)
                .Build();

            //act
            var res = ruleEngine.EvaluateRulesForDictionaryAndQueryBuilderFilterRule(sourceValues, queryBuilderFilterRule);

            //assert
            Assert.IsNotNull(res);
            Assert.IsTrue(string.IsNullOrEmpty(res.ReasonDidntPass), res.ReasonDidntPass);
            Assert.IsTrue(res.Pass);
        }


        [DataTestMethod]
        [DataRow(IdOrField.Age, 12, Operator.Is_Greater_Than, 14)]
        [DataRow(IdOrField.Age, 18, Operator.Is_Less_Than, 16)]
        [DataRow(IdOrField.Zip, "90210", Operator.Is, "ZIP90210,Zip90211,zIp90212")]
        [DataRow(IdOrField.Zip, "Zip90210", Operator.Is_Not, "ZIP90210,Zip90211,zIp90212")]
        [DataRow(IdOrField.AreaOfStudy, "Fine Arts & Design", Operator.Is_Not, new string[3] { "Fine Arts & Design", "Business", "Technology" })]
        [DataRow(IdOrField.AreaOfStudy, "Fine Arts & Design", Operator.Is, new string[2] { "Business", "Technology" })]
        [DataRow(IdOrField.Browser, "IE", Operator.Is_Not, new string[2] { "IE", "Firefox" })]
        [DataRow(IdOrField.BrowserPlatform, "Android", Operator.Is_Not, new string[2] { "Android", "Windows" })]
        [DataRow(IdOrField.CollegeCredits, "1 - 29 Credits", Operator.Is_Not, new string[2] { "1 - 29 Credits", "30 - 59 Credits" })]
        [DataRow(IdOrField.ConnectionSource, "University", Operator.Is_Not, new string[2] { "University", "University" })]
        [DataRow(IdOrField.ConnectionType, "Cellular", Operator.Is_Not, new string[2] { "Cellular", "Dialup" })]
        [DataRow(IdOrField.Country, "United States", Operator.Is_Not, new string[2] { "United States", "Other" })]
        [DataRow(IdOrField.DegreeLevel, "Associate", Operator.Is_Not, new string[2] { "Associate", "Course" })]
        [DataRow(IdOrField.DeviceType, "Mobile", Operator.Is_Not, new string[2] { "Mobile", "Other" })]
        [DataRow(IdOrField.EducationLevel, "Bachelor", Operator.Is_Not, new string[2] { "Bachelor", "Master" })]
        [DataRow(IdOrField.Gender, "Male", Operator.Is_Not, new string[2] { "Male", "Female" })]
        [DataRow(IdOrField.HighSchoolGPA, "2.0 - 2.4", Operator.Is_Not, new string[2] { "2.0 - 2.4", "2.5-2.74" })]
        [DataRow(IdOrField.Income, "$20,000 - $40,000", Operator.Is_Not, new string[2] { "$20,000 - $40,000", "$40,000 - $60,000" })]
        [DataRow(IdOrField.LearningPreference, "Campus Only", Operator.Is_Not, new string[2] { "Campus Only", "Online" })]
        [DataRow(IdOrField.MarketingUnits, "Degree PPC (GDN)", Operator.Is_Not, new string[2] { "Degree PPC (GDN)", "Financial Aid (GDN)" })]
        [DataRow(IdOrField.MilitaryAffiliation, "Yes", Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.MilitaryBranch, "No Military Affiliation", Operator.Is_Not, new string[2] { "No Military Affiliation", "AF - Active Duty(AD)" })]
        [DataRow(IdOrField.PlannedStart, "1 - 3 Months", Operator.Is_Not, new string[2] { "1 - 3 Months", "4 - 6 Months" })]
        [DataRow(IdOrField.RegisteredNurse, "Yes", Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.State, "AL - Alabama", Operator.Is_Not, new string[2] { "AL - Alabama", "TX - Texas" })]
        [DataRow(IdOrField.SubChannels, "Internal Data Purchase Agreements", Operator.Is_Not, new string[2] { "Internal Data Purchase Agreements", "External Data Purchase Agreements" })]
        [DataRow(IdOrField.Subject, "Fine Art", Operator.Is_Not, new string[2] { "Fine Art", "Interior Design" })]
        [DataRow(IdOrField.TeachingCert, "Yes", Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.TrackingTags, "Tag1", Operator.Is_Not, "Tag1,Tag2,Tag3")]
        [DataRow(IdOrField.USCitizen, "Yes", Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.Browser, "Chrome", Operator.Is, new string[2] { "IE", "Firefox" })]
        [DataRow(IdOrField.BrowserPlatform, "Linux", Operator.Is, new string[2] { "Android", "Windows" })]
        [DataRow(IdOrField.CollegeCredits, "NoCredits", Operator.Is, new string[2] { "1 - 29 Credits", "30 - 59 Credits" })]
        [DataRow(IdOrField.ConnectionSource, "NotUniversity", Operator.Is, new string[2] { "University", "University" })]
        [DataRow(IdOrField.ConnectionType, "Broadband", Operator.Is, new string[2] { "Cellular", "Dialup" })]
        [DataRow(IdOrField.Country, "Europe", Operator.Is, new string[2] { "United States", "Other" })]
        [DataRow(IdOrField.DegreeLevel, "Bachelor", Operator.Is, new string[2] { "Associate", "Course" })]
        [DataRow(IdOrField.DeviceType, "Tablet", Operator.Is, new string[2] { "Mobile", "Other" })]
        [DataRow(IdOrField.EducationLevel, "Doctorate", Operator.Is, new string[2] { "Bachelor", "Master" })]
        [DataRow(IdOrField.Gender, "Non Binary", Operator.Is, new string[2] { "Male", "Female" })]
        [DataRow(IdOrField.HighSchoolGPA, "Over 6000", Operator.Is, new string[2] { "2.0 - 2.4", "2.5-2.74" })]
        [DataRow(IdOrField.Income, "$60,000 - $80,000", Operator.Is, new string[2] { "$20,000 - $40,000", "$40,000 - $60,000" })]
        [DataRow(IdOrField.LearningPreference, "Campus or Online", Operator.Is, new string[2] { "Campus Only", "Online" })]
        [DataRow(IdOrField.MarketingUnits, "Not Valid", Operator.Is, new string[2] { "Degree PPC (GDN)", "Financial Aid (GDN)" })]
        [DataRow(IdOrField.MilitaryAffiliation, "Other", Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.MilitaryBranch, "InvalidOption", Operator.Is, new string[2] { "No Military Affiliation", "AF - Active Duty(AD)" })]
        [DataRow(IdOrField.PlannedStart, "10 - 30 Months", Operator.Is, new string[2] { "1 - 3 Months", "4 - 6 Months" })]
        [DataRow(IdOrField.RegisteredNurse, "Other", Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.State, "CA - California", Operator.Is, new string[2] { "AL - Alabama", "TX - Texas" })]
        [DataRow(IdOrField.SubChannels, "Transitory Data Purchase Agreements", Operator.Is, new string[2] { "Internal Data Purchase Agreements", "External Data Purchase Agreements" })]
        [DataRow(IdOrField.Subject, "Sublime Art", Operator.Is, new string[2] { "Fine Art", "Interior Design" })]
        [DataRow(IdOrField.TeachingCert, "Maybe", Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.TrackingTags, "Tag10", Operator.Is, "Tag1,Tag2,Tag3")]
        [DataRow(IdOrField.USCitizen, "Yes", Operator.Is, new string[1] { "No" })]
        [DataRow(IdOrField.SiteUrl, "http://url1", Operator.Is, new string[1] {  "http://url2" })]
        [DataRow(IdOrField.SiteUrl, "http://url18", Operator.Is_Not, new string[1] {  "http://url18" })]
        [DataRow(IdOrField.SiteUrl, "http://url111/test/moreurl", Operator.Contains_One_Of, new string[1] {  "url222,zzz,JJJ" })]
        [DataRow(IdOrField.SiteUrl, "http://url111/test/moreurl", Operator.Not_Contains_One_Of, new string[1] {  "11/test/mor,333,/testing,zzz,JJJ" })]        
        /*Negative case condition is not met*/
        public void RuleEngine_IdOrFieldOperator_PassFalse(IdOrField idOrField, object referenceValue, Operator op, object compareValue)
        {
            //arrange
            var ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
            var sourceValues = new SourceValuesGenerator()
                .AddRandomValues()
                .AddIdOrFieldValues(idOrField, referenceValue.ToString())
                .Build();

            var queryBuilderFilterRule = new QueryBuilderGenerator()
                .AddIdOrFieldOperatorThan(idOrField, op, compareValue)
                .Build();

            //act
            var res = ruleEngine.EvaluateRulesForDictionaryAndQueryBuilderFilterRule(sourceValues, queryBuilderFilterRule);

            //assert
            Assert.IsNotNull(res);
            Assert.IsFalse(string.IsNullOrEmpty(res.ReasonDidntPass), res.ReasonDidntPass);
            Trace.WriteLine(res.ReasonDidntPass);
            Assert.IsFalse(res.Pass);
        }


        [DataTestMethod]
        [DataRow(IdOrField.Age, null, Operator.Is_Greater_Than, 14)]
        [DataRow(IdOrField.Age, null, Operator.Is_Less_Than, 16)]
        [DataRow(IdOrField.Zip, null, Operator.Is, "ZIP90210,Zip90211,zIp90212")]
        [DataRow(IdOrField.Zip, null, Operator.Is_Not, "ZIP90210,Zip90211,zIp90212")]
        [DataRow(IdOrField.AreaOfStudy, null, Operator.Is, new string[3] { "Fine Arts & Design", "Business", "Technology" })]
        [DataRow(IdOrField.AreaOfStudy, null, Operator.Is_Not, new string[2] { "Business", "Technology" })]
        [DataRow(IdOrField.Browser, null, Operator.Is, new string[2] { "IE", "Firefox" })]
        [DataRow(IdOrField.BrowserPlatform, null, Operator.Is, new string[2] { "Android", "Windows" })]
        [DataRow(IdOrField.CollegeCredits, null, Operator.Is, new string[2] { "1 - 29 Credits", "30 - 59 Credits" })]
        [DataRow(IdOrField.ConnectionSource, null, Operator.Is, new string[2] { "University", "University" })]
        [DataRow(IdOrField.ConnectionType, null, Operator.Is, new string[2] { "Cellular", "Dialup" })]
        [DataRow(IdOrField.Country, null, Operator.Is, new string[2] { "United States", "Other" })]
        [DataRow(IdOrField.DegreeLevel, null, Operator.Is, new string[2] { "Associate", "Course" })]
        [DataRow(IdOrField.DeviceType, null, Operator.Is, new string[2] { "Mobile", "Other" })]
        [DataRow(IdOrField.EducationLevel, null, Operator.Is, new string[2] { "Bachelor", "Master" })]
        [DataRow(IdOrField.Gender, null, Operator.Is, new string[2] { "Male", "Female" })]
        [DataRow(IdOrField.HighSchoolGPA, null, Operator.Is, new string[2] { "2.0 - 2.4", "2.5-2.74" })]
        [DataRow(IdOrField.Income, null, Operator.Is, new string[2] { "$20,000 - $40,000", "$40,000 - $60,000" })]
        [DataRow(IdOrField.LearningPreference, null, Operator.Is, new string[2] { "Campus Only", "Online" })]
        [DataRow(IdOrField.MarketingUnits, null, Operator.Is, new string[2] { "Degree PPC (GDN)", "Financial Aid (GDN)" })]
        [DataRow(IdOrField.MilitaryAffiliation, null, Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.MilitaryBranch, null, Operator.Is, new string[2] { "No Military Affiliation", "AF - Active Duty(AD)" })]
        [DataRow(IdOrField.PlannedStart, null, Operator.Is, new string[2] { "1 - 3 Months", "4 - 6 Months" })]
        [DataRow(IdOrField.RegisteredNurse, null, Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.State, null, Operator.Is, new string[2] { "AL - Alabama", "TX - Texas" })]
        [DataRow(IdOrField.SubChannels, null, Operator.Is, new string[2] { "Internal Data Purchase Agreements", "External Data Purchase Agreements" })]
        [DataRow(IdOrField.Subject, null, Operator.Is, new string[2] { "Fine Art", "Interior Design" })]
        [DataRow(IdOrField.TeachingCert, null, Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.TrackingTags, null, Operator.Is, "Tag1,Tag2,Tag3")]
        [DataRow(IdOrField.USCitizen, null, Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.Browser, null, Operator.Is_Not, new string[2] { "IE", "Firefox" })]
        [DataRow(IdOrField.BrowserPlatform, null, Operator.Is_Not, new string[2] { "Android", "Windows" })]
        [DataRow(IdOrField.CollegeCredits, null, Operator.Is_Not, new string[2] { "1 - 29 Credits", "30 - 59 Credits" })]
        [DataRow(IdOrField.ConnectionSource, null, Operator.Is_Not, new string[2] { "University", "University" })]
        [DataRow(IdOrField.ConnectionType, null, Operator.Is_Not, new string[2] { "Cellular", "Dialup" })]
        [DataRow(IdOrField.Country, null, Operator.Is_Not, new string[2] { "United States", "Other" })]
        [DataRow(IdOrField.DegreeLevel, null, Operator.Is_Not, new string[2] { "Associate", "Course" })]
        [DataRow(IdOrField.DeviceType, null, Operator.Is_Not, new string[2] { "Mobile", "Other" })]
        [DataRow(IdOrField.EducationLevel, null, Operator.Is_Not, new string[2] { "Bachelor", "Master" })]
        [DataRow(IdOrField.Gender, null, Operator.Is_Not, new string[2] { "Male", "Female" })]
        [DataRow(IdOrField.HighSchoolGPA, null, Operator.Is_Not, new string[2] { "2.0 - 2.4", "2.5-2.74" })]
        [DataRow(IdOrField.Income, null, Operator.Is_Not, new string[2] { "$20,000 - $40,000", "$40,000 - $60,000" })]
        [DataRow(IdOrField.LearningPreference, null, Operator.Is_Not, new string[2] { "Campus Only", "Online" })]
        [DataRow(IdOrField.MarketingUnits, null, Operator.Is_Not, new string[2] { "Degree PPC (GDN)", "Financial Aid (GDN)" })]
        [DataRow(IdOrField.MilitaryAffiliation, null, Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.MilitaryBranch, null, Operator.Is_Not, new string[2] { "No Military Affiliation", "AF - Active Duty(AD)" })]
        [DataRow(IdOrField.PlannedStart, null, Operator.Is_Not, new string[2] { "1 - 3 Months", "4 - 6 Months" })]
        [DataRow(IdOrField.RegisteredNurse, null, Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.State, null, Operator.Is_Not, new string[2] { "AL - Alabama", "TX - Texas" })]
        [DataRow(IdOrField.SubChannels, null, Operator.Is_Not, new string[2] { "Internal Data Purchase Agreements", "External Data Purchase Agreements" })]
        [DataRow(IdOrField.Subject, null, Operator.Is_Not, new string[2] { "Fine Art", "Interior Design" })]
        [DataRow(IdOrField.TeachingCert, null, Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.TrackingTags, null, Operator.Is_Not, "Tag1,Tag2,Tag3")]
        [DataRow(IdOrField.USCitizen, null, Operator.Is_Not, new string[1] { "No" })]
        /*Negative case, value to compare is not provided, we evaluate the AcceptMissingParameter and it passes*/
        public void RuleEngine_IdOrFieldOperatorNotSupplied_PassTrue(IdOrField idOrField, object referenceValue, Operator op, object compareValue)
        {
            //arrange
            var ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
            var sourceValues = new SourceValuesGenerator()
                .AddRandomValues()
                .Build();

            var queryBuilderFilterRule = new QueryBuilderGenerator()
                .AddIdOrFieldOperatorThan(idOrField, op, compareValue)
                .Build();

            //act
            var res = ruleEngine.EvaluateRulesForDictionaryAndQueryBuilderFilterRule(sourceValues, queryBuilderFilterRule);

            //assert
            Assert.IsNotNull(res);
            Assert.IsTrue(string.IsNullOrEmpty(res.ReasonDidntPass), res.ReasonDidntPass);
            Assert.IsTrue(res.Pass);
        }


        [DataTestMethod]
        [DataRow(IdOrField.Age, "", Operator.Is_Greater_Than, 14)]
        [DataRow(IdOrField.Age, "", Operator.Is_Less_Than, 16)]
        [DataRow(IdOrField.Zip, "", Operator.Is, "ZIP90210,Zip90211,zIp90212")]
        [DataRow(IdOrField.Zip, "", Operator.Is_Not, "ZIP90210,Zip90211,zIp90212")]
        [DataRow(IdOrField.AreaOfStudy, "", Operator.Is, new string[3] { "Fine Arts & Design", "Business", "Technology" })]
        [DataRow(IdOrField.AreaOfStudy, "", Operator.Is_Not, new string[2] { "Business", "Technology" })]
        [DataRow(IdOrField.Browser, "", Operator.Is, new string[2] { "IE", "Firefox" })]
        [DataRow(IdOrField.BrowserPlatform, "", Operator.Is, new string[2] { "Android", "Windows" })]
        [DataRow(IdOrField.CollegeCredits, "", Operator.Is, new string[2] { "1 - 29 Credits", "30 - 59 Credits" })]
        [DataRow(IdOrField.ConnectionSource, "", Operator.Is, new string[2] { "University", "University" })]
        [DataRow(IdOrField.ConnectionType, "", Operator.Is, new string[2] { "Cellular", "Dialup" })]
        [DataRow(IdOrField.Country, "", Operator.Is, new string[2] { "United States", "Other" })]
        [DataRow(IdOrField.DegreeLevel, "", Operator.Is, new string[2] { "Associate", "Course" })]
        [DataRow(IdOrField.DeviceType, "", Operator.Is, new string[2] { "Mobile", "Other" })]
        [DataRow(IdOrField.EducationLevel, "", Operator.Is, new string[2] { "Bachelor", "Master" })]
        [DataRow(IdOrField.Gender, "", Operator.Is, new string[2] { "Male", "Female" })]
        [DataRow(IdOrField.HighSchoolGPA, "", Operator.Is, new string[2] { "2.0 - 2.4", "2.5-2.74" })]
        [DataRow(IdOrField.Income, "", Operator.Is, new string[2] { "$20,000 - $40,000", "$40,000 - $60,000" })]
        [DataRow(IdOrField.LearningPreference, "", Operator.Is, new string[2] { "Campus Only", "Online" })]
        [DataRow(IdOrField.MarketingUnits, "", Operator.Is, new string[2] { "Degree PPC (GDN)", "Financial Aid (GDN)" })]
        [DataRow(IdOrField.MilitaryAffiliation, "", Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.MilitaryBranch, "", Operator.Is, new string[2] { "No Military Affiliation", "AF - Active Duty(AD)" })]
        [DataRow(IdOrField.PlannedStart, "", Operator.Is, new string[2] { "1 - 3 Months", "4 - 6 Months" })]
        [DataRow(IdOrField.RegisteredNurse, "", Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.State, "", Operator.Is, new string[2] { "AL - Alabama", "TX - Texas" })]
        [DataRow(IdOrField.SubChannels, "", Operator.Is, new string[2] { "Internal Data Purchase Agreements", "External Data Purchase Agreements" })]
        [DataRow(IdOrField.Subject, "", Operator.Is, new string[2] { "Fine Art", "Interior Design" })]
        [DataRow(IdOrField.TeachingCert, "", Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.TrackingTags, "", Operator.Is, "Tag1,Tag2,Tag3")]
        [DataRow(IdOrField.USCitizen, "", Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.Browser, "", Operator.Is_Not, new string[2] { "IE", "Firefox" })]
        [DataRow(IdOrField.BrowserPlatform, "", Operator.Is_Not, new string[2] { "Android", "Windows" })]
        [DataRow(IdOrField.CollegeCredits, "", Operator.Is_Not, new string[2] { "1 - 29 Credits", "30 - 59 Credits" })]
        [DataRow(IdOrField.ConnectionSource, "", Operator.Is_Not, new string[2] { "University", "University" })]
        [DataRow(IdOrField.ConnectionType, "", Operator.Is_Not, new string[2] { "Cellular", "Dialup" })]
        [DataRow(IdOrField.Country, "", Operator.Is_Not, new string[2] { "United States", "Other" })]
        [DataRow(IdOrField.DegreeLevel, "", Operator.Is_Not, new string[2] { "Associate", "Course" })]
        [DataRow(IdOrField.DeviceType, "", Operator.Is_Not, new string[2] { "Mobile", "Other" })]
        [DataRow(IdOrField.EducationLevel, "", Operator.Is_Not, new string[2] { "Bachelor", "Master" })]
        [DataRow(IdOrField.Gender, "", Operator.Is_Not, new string[2] { "Male", "Female" })]
        [DataRow(IdOrField.HighSchoolGPA, "", Operator.Is_Not, new string[2] { "2.0 - 2.4", "2.5-2.74" })]
        [DataRow(IdOrField.Income, "", Operator.Is_Not, new string[2] { "$20,000 - $40,000", "$40,000 - $60,000" })]
        [DataRow(IdOrField.LearningPreference, "", Operator.Is_Not, new string[2] { "Campus Only", "Online" })]
        [DataRow(IdOrField.MarketingUnits, "", Operator.Is_Not, new string[2] { "Degree PPC (GDN)", "Financial Aid (GDN)" })]
        [DataRow(IdOrField.MilitaryAffiliation, "", Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.MilitaryBranch, "", Operator.Is_Not, new string[2] { "No Military Affiliation", "AF - Active Duty(AD)" })]
        [DataRow(IdOrField.PlannedStart, "", Operator.Is_Not, new string[2] { "1 - 3 Months", "4 - 6 Months" })]
        [DataRow(IdOrField.RegisteredNurse, "", Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.State, "", Operator.Is_Not, new string[2] { "AL - Alabama", "TX - Texas" })]
        [DataRow(IdOrField.SubChannels, "", Operator.Is_Not, new string[2] { "Internal Data Purchase Agreements", "External Data Purchase Agreements" })]
        [DataRow(IdOrField.Subject, "", Operator.Is_Not, new string[2] { "Fine Art", "Interior Design" })]
        [DataRow(IdOrField.TeachingCert, "", Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.TrackingTags, "", Operator.Is_Not, "Tag1,Tag2,Tag3")]
        [DataRow(IdOrField.USCitizen, "", Operator.Is_Not, new string[1] { "No" })]
        /*Negative case, value to compare is not provided, we evaluate the AcceptMissingParameter and it passes*/
        public void RuleEngine_IdOrFieldOperatorIsEmptyString_PassTrue(IdOrField idOrField, object referenceValue, Operator op, object compareValue)
        {
            //arrange
            var ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
            var sourceValues = new SourceValuesGenerator()
                .AddIdOrFieldValues(idOrField, referenceValue)
                .AddRandomValues()
                .Build();

            var queryBuilderFilterRule = new QueryBuilderGenerator()
                .AddIdOrFieldOperatorThan(idOrField, op, compareValue)
                .Build();

            //act
            var res = ruleEngine.EvaluateRulesForDictionaryAndQueryBuilderFilterRule(sourceValues, queryBuilderFilterRule);

            //assert
            Assert.IsNotNull(res);
            Assert.IsTrue(string.IsNullOrEmpty(res.ReasonDidntPass), res.ReasonDidntPass);
            Assert.IsTrue(res.Pass);
        }
        
        [DataTestMethod]
        [DataRow(IdOrField.Age, null, Operator.Is_Greater_Than, 14)]
        [DataRow(IdOrField.Age, null, Operator.Is_Less_Than, 16)]
        [DataRow(IdOrField.Zip, null, Operator.Is, "ZIP90210,Zip90211,zIp90212")]
        [DataRow(IdOrField.Zip, null, Operator.Is_Not, "ZIP90210,Zip90211,zIp90212")]
        [DataRow(IdOrField.AreaOfStudy, null, Operator.Is, new string[3] { "Fine Arts & Design", "Business", "Technology" })]
        [DataRow(IdOrField.AreaOfStudy, null, Operator.Is_Not, new string[2] { "Business", "Technology" })]
        [DataRow(IdOrField.Browser, null, Operator.Is, new string[2] { "IE", "Firefox" })]
        [DataRow(IdOrField.BrowserPlatform, null, Operator.Is, new string[2] { "Android", "Windows" })]
        [DataRow(IdOrField.CollegeCredits, null, Operator.Is, new string[2] { "1 - 29 Credits", "30 - 59 Credits" })]
        [DataRow(IdOrField.ConnectionSource, null, Operator.Is, new string[2] { "University", "University" })]
        [DataRow(IdOrField.ConnectionType, null, Operator.Is, new string[2] { "Cellular", "Dialup" })]
        [DataRow(IdOrField.Country, null, Operator.Is, new string[2] { "United States", "Other" })]
        [DataRow(IdOrField.DegreeLevel, null, Operator.Is, new string[2] { "Associate", "Course" })]
        [DataRow(IdOrField.DeviceType, null, Operator.Is, new string[2] { "Mobile", "Other" })]
        [DataRow(IdOrField.EducationLevel, null, Operator.Is, new string[2] { "Bachelor", "Master" })]
        [DataRow(IdOrField.Gender, null, Operator.Is, new string[2] { "Male", "Female" })]
        [DataRow(IdOrField.HighSchoolGPA, null, Operator.Is, new string[2] { "2.0 - 2.4", "2.5-2.74" })]
        [DataRow(IdOrField.Income, null, Operator.Is, new string[2] { "$20,000 - $40,000", "$40,000 - $60,000" })]
        [DataRow(IdOrField.LearningPreference, null, Operator.Is, new string[2] { "Campus Only", "Online" })]
        [DataRow(IdOrField.MarketingUnits, null, Operator.Is, new string[2] { "Degree PPC (GDN)", "Financial Aid (GDN)" })]
        [DataRow(IdOrField.MilitaryAffiliation, null, Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.MilitaryBranch, null, Operator.Is, new string[2] { "No Military Affiliation", "AF - Active Duty(AD)" })]
        [DataRow(IdOrField.PlannedStart, null, Operator.Is, new string[2] { "1 - 3 Months", "4 - 6 Months" })]
        [DataRow(IdOrField.RegisteredNurse, null, Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.State, null, Operator.Is, new string[2] { "AL - Alabama", "TX - Texas" })]
        [DataRow(IdOrField.SubChannels, null, Operator.Is, new string[2] { "Internal Data Purchase Agreements", "External Data Purchase Agreements" })]
        [DataRow(IdOrField.Subject, null, Operator.Is, new string[2] { "Fine Art", "Interior Design" })]
        [DataRow(IdOrField.TeachingCert, null, Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.TrackingTags, null, Operator.Is, "Tag1,Tag2,Tag3")]
        [DataRow(IdOrField.USCitizen, null, Operator.Is, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.Browser, null, Operator.Is_Not, new string[2] { "IE", "Firefox" })]
        [DataRow(IdOrField.BrowserPlatform, null, Operator.Is_Not, new string[2] { "Android", "Windows" })]
        [DataRow(IdOrField.CollegeCredits, null, Operator.Is_Not, new string[2] { "1 - 29 Credits", "30 - 59 Credits" })]
        [DataRow(IdOrField.ConnectionSource, null, Operator.Is_Not, new string[2] { "University", "University" })]
        [DataRow(IdOrField.ConnectionType, null, Operator.Is_Not, new string[2] { "Cellular", "Dialup" })]
        [DataRow(IdOrField.Country, null, Operator.Is_Not, new string[2] { "United States", "Other" })]
        [DataRow(IdOrField.DegreeLevel, null, Operator.Is_Not, new string[2] { "Associate", "Course" })]
        [DataRow(IdOrField.DeviceType, null, Operator.Is_Not, new string[2] { "Mobile", "Other" })]
        [DataRow(IdOrField.EducationLevel, null, Operator.Is_Not, new string[2] { "Bachelor", "Master" })]
        [DataRow(IdOrField.Gender, null, Operator.Is_Not, new string[2] { "Male", "Female" })]
        [DataRow(IdOrField.HighSchoolGPA, null, Operator.Is_Not, new string[2] { "2.0 - 2.4", "2.5-2.74" })]
        [DataRow(IdOrField.Income, null, Operator.Is_Not, new string[2] { "$20,000 - $40,000", "$40,000 - $60,000" })]
        [DataRow(IdOrField.LearningPreference, null, Operator.Is_Not, new string[2] { "Campus Only", "Online" })]
        [DataRow(IdOrField.MarketingUnits, null, Operator.Is_Not, new string[2] { "Degree PPC (GDN)", "Financial Aid (GDN)" })]
        [DataRow(IdOrField.MilitaryAffiliation, null, Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.MilitaryBranch, null, Operator.Is_Not, new string[2] { "No Military Affiliation", "AF - Active Duty(AD)" })]
        [DataRow(IdOrField.PlannedStart, null, Operator.Is_Not, new string[2] { "1 - 3 Months", "4 - 6 Months" })]
        [DataRow(IdOrField.RegisteredNurse, null, Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.State, null, Operator.Is_Not, new string[2] { "AL - Alabama", "TX - Texas" })]
        [DataRow(IdOrField.SubChannels, null, Operator.Is_Not, new string[2] { "Internal Data Purchase Agreements", "External Data Purchase Agreements" })]
        [DataRow(IdOrField.Subject, null, Operator.Is_Not, new string[2] { "Fine Art", "Interior Design" })]
        [DataRow(IdOrField.TeachingCert, null, Operator.Is_Not, new string[2] { "Yes", "No" })]
        [DataRow(IdOrField.TrackingTags, null, Operator.Is_Not, "Tag1,Tag2,Tag3")]
        [DataRow(IdOrField.USCitizen, null, Operator.Is_Not, new string[1] { "No" })]
        /*Negative case, value to compare is not provided, AcceptMissingParameter not provided and it fails*/
        public void RuleEngine_IdOrFieldOperatorNotProvidedNotAllowedNotPresent_PassFalse(IdOrField idOrField, object referenceValue, Operator op, object compareValue)
        {
            //arrange
            var ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
            var sourceValues = new SourceValuesGenerator()
                .AddRandomValues()
                .Build();

            var queryBuilderFilterRule = new QueryBuilderGenerator()
                .AddIdOrFieldOperatorThan(idOrField, op, compareValue,false)
                .Build();

            //act
            var res = ruleEngine.EvaluateRulesForDictionaryAndQueryBuilderFilterRule(sourceValues, queryBuilderFilterRule);

            //assert
            Assert.IsNotNull(res);
            Assert.IsFalse(string.IsNullOrEmpty(res.ReasonDidntPass), res.ReasonDidntPass);
            Trace.WriteLine(res.ReasonDidntPass);
            Assert.IsFalse(res.Pass);
        }


    }
}
