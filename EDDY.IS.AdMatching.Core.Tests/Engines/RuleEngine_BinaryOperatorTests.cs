using EDDY.IS.AdMatching.Core.Tests.Utilities;
using EnumsNET;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using EDDY.IS.Common.ConstantsAndEnums;
using EDDY.IS.Common.Dto.RuleEngine;

namespace EDDY.IS.AdMatching.Core.Tests.Engines
{
    [TestClass]
    public class RuleEngine_BinaryOperatorTests
    {
	    #region LongJson
	    private const string LongJson = @"{
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
					""operator"": ""range"",
					""value"": [
						""10"",
						""100""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""EducationLevel"",
					""field"": ""EducationLevel"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Haven't completed High School"",
						""G.E.D."",
						""High School Diploma"",
						""Some College"",
						""Associate""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""CollegeCredits"",
					""field"": ""CollegeCredits"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""1 - 29 Credits"",
						""30 - 59 Credits"",
						""60 - 89 Credits"",
						""90+ Credits""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""Gender"",
					""field"": ""Gender"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Male"",
						""Female"",
						""Non-binary""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""HighSchoolGPA"",
					""field"": ""HighSchoolGPA"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Less than 2.0"",
						""2.0-2.4"",
						""2.5-2.74"",
						""2.75-2.9"",
						""3.0-3.4"",
						""3.5 or Higher""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""HighSchoolGradYear"",
					""field"": ""HighSchoolGradYear"",
					""type"": ""integer"",
					""input"": ""number"",
					""operator"": ""range"",
					""value"": [
						2000,
						2010
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""MilitaryAffiliation"",
					""field"": ""MilitaryAffiliation"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Yes"",
						""No""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""Income"",
					""field"": ""Income"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Less than $20,000"",
						""$20,000 - $40,000"",
						""$40,000 - $60,000"",
						""$60,000 - $100,000"",
						""More than $100,000""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""MilitaryBranch"",
					""field"": ""MilitaryBranch"",
					""type"": ""string"",
					""input"": ""select"",
					""operator"": ""is"",
					""value"": [
						""AF - Active Duty(AD)"",
						""AF - Selective Reserve(SR)"",
						""AF - Spouse of AD or SR"",
						""AF - Veteran"",
						""AF - Civilian"",
						""AR - Active Duty(AD)"",
						""AR - Selective Reserve(SR)"",
						""AR - Spouse of AD or SR"",
						""AR - Veteran"",
						""AR - Civilian"",
						""CG - Active Duty(AD)"",
						""CG - Selective Reserve(SR)"",
						""CG - Spouse of AD or SR"",
						""CG - Veteran"",
						""CG - Civilian"",
						""MC - Active Duty(AD)"",
						""MC - Selective Reserve(SR)"",
						""MC - Spouse of AD or SR"",
						""MC - Veteran"",
						""MC - Civilian"",
						""NV - Active Duty(AD)"",
						""NV - Selective Reserve(SR)"",
						""NV - Spouse of AD or SR"",
						""NV - Veteran"",
						""NV - Civilian"",
						""No Military Affiliation""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""PlannedStart"",
					""field"": ""PlannedStart"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Immediately"",
						""1 - 3 Months"",
						""4 - 6 Months"",
						""7 - 12 Months"",
						""More than 1 year"",
						""Not Sure""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""RegisteredNurse"",
					""field"": ""RegisteredNurse"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Yes"",
						""No"",
						""Currently Obtaining""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""TeachingCert"",
					""field"": ""TeachingCert"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Yes"",
						""No""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""USCitizen"",
					""field"": ""USCitizen"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Yes"",
						""No""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""MarketingUnits"",
					""field"": ""MarketingUnits"",
					""type"": ""string"",
					""input"": ""select"",
					""operator"": ""is"",
					""value"": [
						""Adsupply PPC (Traditional Display)"",
						""Adware (Adware)"",
						""Best Online Universities (Online Partners)"",
						""Bing Partner (Paid Search)"",
						""Bing Partner Lead Score (Paid Search)"",
						""Branded (mobile) (Paid Search)"",
						""Branded (Paid Search)"",
						""Call Center Partners (Call Center Partners)"",
						""Campus Explorer (Traditional Display)"",
						""Campus Explorer Lead Score (Traditional Display)"",
						""ChristianEdu (Social Media)"",
						""ChristianEdu Exclusive (Social Media)"",
						""Click-to-lead Aggregation (Clicks)"",
						""Click-to-lead Direct (Clicks)"",
						""Clicks (Clicks)"",
						""Clicks Partners (Clicks)"",
						""Clicks Product Lead Delivery (Clicks)"",
						""Clicks.Net (Traditional Display)"",
						""Clicks.net Kaplan Allow (Traditional Display)"",
						""Clicks.net Non-premier (Traditional Display)"",
						""Clicks.net Premier (Traditional Display)"",
						""Contact Center Services - EMS Only (Contact Center Services - EMS Only)"",
						""Criteo PPC (Traditional Display)"",
						""Criteo Retargeting (Traditional Display)"",
						""Custom Campaign (Custom Campaign)"",
						""Degree (mobile) (Paid Search)"",
						""Degree (Paid Search)"",
						""Degree DSK (GDN)"",
						""Degree Lead Score (GDN)"",
						""Degree PPC (GDN)"",
						""Degree Premier (Paid Search)"",
						""Degree Premier Partner (Paid Search)"",
						""Display - EMS Only (Display - EMS Only)"",
						""Display EP (Traditional Display)"",
						""Display Job (Traditional Display)"",
						""Division-D Popunder (Traditional Display)"",
						""DRTV & Video Ads (DRTV & Video Ads)"",
						""EdDy AdServer (SEO)"",
						""EDU Traffic Partners (Online Partners)"",
						""EDU Ventures (Call Center Partners)"",
						""EDU Ventures (Online Partners)"",
						""Education (Inbound)"",
						""Education (Outbound)"",
						""Education Select (Outbound)"",
						""EL SEO (SEO)"",
						""Email - EMS Only (Email - EMS Only)"",
						""Events (Events)"",
						""Exclusive (Internal Email)"",
						""Exclusive Lead (Paid Search)"",
						""Exclusive Lead (Traditional Display)"",
						""Exclusive Prime (Clicks)"",
						""Exclusive Prime (Paid Search)"",
						""External Data Purchase Agreements (External Data Purchase Agreements)"",
						""External Email (External Email)"",
						""External Transfer Agreements (External Transfer Agreements)"",
						""Financial Aid (GDN)"",
						""Financial Aid (mobile) (Paid Search)"",
						""Financial Aid (Paid Search)"",
						""Financial Aid Lead Score (Paid Search)"",
						""Financial Aid Premier Partner (Paid Search)"",
						""FinancialAid Premier (Paid Search)"",
						""FinancialAidV2 (Paid Search)"",
						""Find Top Colleges (Adware)"",
						""Find Top Colleges (External Email)"",
						""Find Top Colleges (Online Partners)"",
						""Find Top Colleges (Paid Search)"",
						""Future Ads PPC (Traditional Display)"",
						""Future Ads PPV (Traditional Display)"",
						""Future Ads PPV Lead Score (Traditional Display)"",
						""Future Ads Premier (Traditional Display)"",
						""Future Ads Premier Lead Score (Traditional Display)"",
						""GDN CPL (GDN)"",
						""General (Call Center Partners)"",
						""General (Online Partners)"",
						""General New (Paid Search)"",
						""General New Kaplan Allow (Paid Search)"",
						""Generic (GDN)"",
						""Generic (Paid Search)"",
						""Generic (Traditional Display)"",
						""Generic Kaplan Allow (Paid Search)"",
						""GradLevel (Paid Search)"",
						""GradSchools (Paid Search)"",
						""Gradschools Partner (Paid Search)"",
						""Gradschools SM=0 (Paid Search)"",
						""GradSchools Subject (Paid Search)"",
						""Graduates (Traditional Display)"",
						""GS Podcast (SEO)"",
						""GS SEO SM=0 (SEO)"",
						""GS.com SEO (SEO)"",
						""GSX (SEO)"",
						""Head Terms (mobile) (Paid Search)"",
						""Head Terms (Paid Search)"",
						""Head Terms Partner (Paid Search)"",
						""Home Security (Paid Search)"",
						""Host & Post SEO Partner (Online Partners)"",
						""Host And Post (Online Partners)"",
						""Inactive_50onRed (Traditional Display)"",
						""Inactive_Adroll (Traditional Display)"",
						""Inactive_Adroll Facebook (Traditional Display)"",
						""Inactive_AOL (Traditional Display)"",
						""Inactive_AOL Remarketing (Traditional Display)"",
						""Inactive_DGS (Traditional Display)"",
						""Inactive_Division D Standard (Traditional Display)"",
						""Inactive_Find Top Colleges (GDN)"",
						""Inactive_Gravity (Traditional Display)"",
						""Inactive_NRE (GDN)"",
						""Inactive_NRE (Traditional Display)"",
						""Inactive_Orion CKB (Social Media)"",
						""Inactive_Other (GDN)"",
						""Inactive_Other Vendors (Traditional Display)"",
						""Inactive_Paid Media Prospecting (GDN)"",
						""Inactive_Paid Media Prospecting (Traditional Display)"",
						""Inactive_Vantage Campus Explorer (Traditional Display)"",
						""Inactive_Vantage Media (Traditional Display)"",
						""Inactive_Vantage Media Campus Explorer Lead Score (Traditional Display)"",
						""Inactive_Vantage Media Kaplan Premier (Traditional Display)"",
						""Inactive_Vantage Media Lead Score (Traditional Display)"",
						""Internal Ads (Adware)"",
						""Internal Ads (Call Center Partners)"",
						""Internal Ads (Custom Campaign)"",
						""Internal Ads (DRTV & Video Ads)"",
						""Internal Ads (Events)"",
						""Internal Ads (External Data Purchase Agreements)"",
						""Internal Ads (External Email)"",
						""Internal Ads (External Transfer Agreements)"",
						""Internal Ads (Internal Data Purchase Agreements)"",
						""Internal Ads (Internal Transfer Agreements)"",
						""Internal Ads (Kiosks)"",
						""Internal Ads (Online Partners)"",
						""Internal Ads (Paid Search)"",
						""Internal Ads (SEO)"",
						""Internal Ads (Social Media)"",
						""Internal Ads (TestDoNoUse)"",
						""Internal Data Purchase Agreements (Internal Data Purchase Agreements)"",
						""Internal Email (Internal Email)"",
						""Internal Transfer Agreements (Internal Transfer Agreements)"",
						""International (Online Partners)"",
						""Job Board (Inbound)"",
						""Job Board (Online Partners)"",
						""Job Board (Outbound)"",
						""Job Board - All (Call Center Partners)"",
						""Job Board - Campus (Call Center Partners)"",
						""Job Board – All (Internal Transfer Agreements)"",
						""Job Ventures (Call Center Partners)"",
						""Job Ventures (Online Partners)"",
						""Jobcase (Internal Transfer Agreements)"",
						""Jobcase H&P (Online Partners)"",
						""Kiosks (Kiosks)"",
						""Linkedin (Traditional Display)"",
						""Look-alike (Social Media)"",
						""LowCostvendor (Call Center Partners)"",
						""LowCostvendor (Online Partners)"",
						""MediaAlpha (Traditional Display)"",
						""MediaAlpha Core (Traditional Display)"",
						""Military (GDN)"",
						""Military (Traditional Display)"",
						""Military College (Paid Search)"",
						""Non-Job Board - All (Call Center Partners)"",
						""Non-Job Board - All (Internal Transfer Agreements)"",
						""Non-Job Board - Campus (Call Center Partners)"",
						""NRE (Paid Search)"",
						""Online Partners - EMS Only (Online Partners - EMS Only)"",
						""Overhead Costs (Overhead Costs)"",
						""Paid Media Prospecting (Paid Search)"",
						""Paid Search - EMS Only (Paid Search - EMS Only)"",
						""Paid Search – Remarketing (Paid Search)"",
						""Partner PPC (Traditional Display)"",
						""Perform (Traditional Display)"",
						""Perform Finaid (Traditional Display)"",
						""Premium Clicks (Paid Search)"",
						""QuinSt Campus (Traditional Display)"",
						""QuinSt Non-premier (Traditional Display)"",
						""Quinst Premier (Traditional Display)"",
						""QuinSt Tier 1 (Traditional Display)"",
						""QuinSt Tier 2 (Traditional Display)"",
						""QuinSt Tier 3 (Traditional Display)"",
						""Quora (Traditional Display)"",
						""Quora Finaid (Traditional Display)"",
						""Registered Nurse (Traditional Display)"",
						""Religion (GDN)"",
						""Religion (Paid Search)"",
						""Religion Remarketing GSP (GDN)"",
						""Religion TD (Traditional Display)"",
						""Remarketing (GDN)"",
						""Remarketing Headterm (GDN)"",
						""Retargeting (Social Media)"",
						""SAB (SEO)"",
						""SAB Autoresponders (Internal Email)"",
						""SAB General Emails (Internal Email)"",
						""SAB Social (Social Media)"",
						""School (mobile) (Paid Search)"",
						""School (Paid Search)"",
						""SEO (Online Partners)"",
						""SEO (Paid Search)"",
						""SEO (SEO)"",
						""SEO PARTNER APP (Online Partners)"",
						""SEO Partners (Online Partners)"",
						""SEO Partners Ventures (Online Partners)"",
						""Social Media - EMS Only (Social Media - EMS Only)"",
						""Social Media Broad (Social Media)"",
						""Social Media Exclusive Leads (Social Media)"",
						""Social Media Financial Aid (Social Media)"",
						""Social Media Leads (Social Media)"",
						""Taboola (Traditional Display)"",
						""Targeted Campus (GDN)"",
						""Targeted Campus (Paid Search)"",
						""Targeted Campus (Traditional Display)"",
						""TestDoNoUse (TestDoNoUse)"",
						""TOP SEO Partners (Online Partners)"",
						""UAB (SEO)"",
						""UnigoSEO (SEO)"",
						""XYZ Partner (Online Partners)"",
						""Yahoo-Gemini (Paid Search)"",
						""Youtube (Paid Search)""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""SubChannels"",
					""field"": ""SubChannels"",
					""type"": ""string"",
					""input"": ""select"",
					""operator"": ""is"",
					""value"": [
						""Internal Data Purchase Agreements"",
						""External Data Purchase Agreements"",
						""Internal Transfer Agreements"",
						""External Transfer Agreements"",
						""Traditional Display"",
						""Adware"",
						""GDN"",
						""Call Center Partners"",
						""DRTV & Video Ads"",
						""External Email"",
						""Internal Email"",
						""Online Partners"",
						""Kiosks"",
						""Events"",
						""Paid Search"",
						""SEO"",
						""TestDoNoUse"",
						""Social Media"",
						""Custom Campaign"",
						""Clicks"",
						""Overhead Costs"",
						""API Check"",
						""Contact Center Services - EMS Only"",
						""Display - EMS Only"",
						""Email - EMS Only"",
						""Online Partners - EMS Only"",
						""Paid Search - EMS Only"",
						""Social Media - EMS Only"",
						""Inbound"",
						""Outbound"",
						""SEO"",
						""Exclusive"",
						""GSX""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""Country"",
					""field"": ""Country"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""United States"",
						""US Territories"",
						""Military Bases"",
						""Other""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""State"",
					""field"": ""State"",
					""type"": ""string"",
					""input"": ""select"",
					""operator"": ""is"",
					""value"": [
						""AL - Alabama"",
						""AK - Alaska"",
						""AB - Alberta"",
						""AS - American Samoa"",
						""AZ - Arizona"",
						""AR - Arkansas"",
						""AE - Armed Forces"",
						""AA - Armed Forces - Americas"",
						""AP - Armed Forces - Pacific"",
						""BC - British Columbia"",
						""CA - California"",
						""CO - Colorado"",
						""CT - Connecticut"",
						""DE - Delaware"",
						""DC - District of Columbia"",
						""FM - Federated States of Micronesia"",
						""FL - Florida"",
						""GA - Georgia"",
						""GU - Guam"",
						""HI - Hawaii"",
						""ID - Idaho"",
						""IL - Illinois"",
						""IN - Indiana"",
						""IA - Iowa"",
						""KS - Kansas"",
						""KY - Kentucky"",
						""LA - Louisiana"",
						""ME - Maine"",
						""MB - Manitoba"",
						""MH - Marshall Islands"",
						""MD - Maryland"",
						""MA - Massachusetts"",
						""MI - Michigan"",
						""4794 - Micronesia, Federated States Of"",
						""MN - Minnesota"",
						""MS - Mississippi"",
						""MO - Missouri"",
						""MT - Montana"",
						""NE - Nebraska"",
						""NV - Nevada"",
						""NB - New Brunswick"",
						""NH - New Hampshire"",
						""NJ - New Jersey"",
						""NM - New Mexico"",
						""NY - New York"",
						""NF - Newfoundland And Labrador"",
						""NC - North Carolina"",
						""ND - North Dakota"",
						""MP - Northern Mariana Islands"",
						""NT - Northwest Territories"",
						""NS - Nova Scotia"",
						""NU - Nunavut"",
						""OH - Ohio"",
						""OK - Oklahoma"",
						""ON - Ontario"",
						""OR - Oregon"",
						""PW - Palau"",
						""PA - Pennsylvania"",
						""PE - Prince Edward Island"",
						""PR - Puerto Rico"",
						""QC - Quebec"",
						""RI - Rhode Island"",
						""SK - Saskatchewan"",
						""SC - South Carolina"",
						""SD - South Dakota"",
						""TN - Tennessee"",
						""TX - Texas"",
						""UT - Utah"",
						""VT - Vermont"",
						""VI - Virgin Islands"",
						""4795 - Virgin Islands, U.S."",
						""VA - Virginia"",
						""WA - Washington"",
						""WV - West Virginia"",
						""WI - Wisconsin"",
						""WY - Wyoming"",
						""3842 - Yukon"",
						""YT - Yukon Territory""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""Zip"",
					""field"": ""Zip"",
					""type"": ""string"",
					""input"": ""textarea"",
					""operator"": ""is"",
					""value"": [
						""75032,75031""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""AreaOfStudy"",
					""field"": ""AreaOfStudy"",
					""type"": ""string"",
					""input"": ""select"",
					""operator"": ""is"",
					""value"": [
						""Business"",
						""Criminal Justice & Legal"",
						""Education"",
						""Fine Arts & Design"",
						""Health & Medicine"",
						""Liberal Arts & Humanities"",
						""Math, Science & Engineering"",
						""Public Affairs & Social Sciences"",
						""Religious Studies"",
						""Technology"",
						""Vocational Training""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""Subject"",
					""field"": ""Subject"",
					""type"": ""string"",
					""input"": ""select"",
					""operator"": ""is"",
					""value"": [
						""Fashion, Retail & Merchandising"",
						""Fine Art"",
						""Graphic Design & Multimedia"",
						""Interior Design"",
						""Performing Arts"",
						""Photography"",
						""Music & Audio Production"",
						""Video Game Design"",
						""Web Development & Design"",
						""Industrial Design"",
						""Media Arts"",
						""Museum Studies"",
						""Accounting"",
						""Business Administration & Management"",
						""Business Information Systems"",
						""eCommerce & Social Media"",
						""Economics"",
						""Finance"",
						""Hospitality Management"",
						""Human Resources Management"",
						""International Business"",
						""Marketing & Advertising"",
						""Office Management"",
						""Organizational Leadership"",
						""Project Management"",
						""Public Administration & Policy"",
						""Real Estate & Property Management"",
						""Entrepreneurship"",
						""Sports Management"",
						""Technology Management"",
						""Healthcare Administration & Management"",
						""Environment & Agriculture"",
						""Non-Profit Administration"",
						""Operations Management"",
						""Video Game Design"",
						""Web Development & Design"",
						""Business Information Systems"",
						""Computer Science"",
						""Database Management"",
						""Information Assurance & Cybersecurity"",
						""Information Technology"",
						""Software & Application Development"",
						""Technology Management"",
						""Telecom & Networking"",
						""Computer Training & Support"",
						""Geographic Information Systems"",
						""Information Sciences"",
						""Computational Science"",
						""Economics"",
						""Communications & Public Relations"",
						""English"",
						""History"",
						""Liberal Arts & Sciences"",
						""Languages"",
						""Philosophy & Ethics"",
						""Women's Studies"",
						""Literature & Writing"",
						""Humanities"",
						""Area, Ethnic & Cultural Studies"",
						""Education, Technology & Online Learning"",
						""Adult Education"",
						""Curriculum & Instruction"",
						""Early Childhood Education"",
						""ESL/TESOL"",
						""General Education"",
						""Higher Education"",
						""International Education"",
						""K-12 Education"",
						""Education Leadership & Administration"",
						""Special & Gifted Education"",
						""Educational & School Psychology"",
						""Environmental Education"",
						""Teacher Education"",
						""Health Informatics"",
						""Healthcare Administration & Management"",
						""Human Services"",
						""Medical Assisting"",
						""Medical Billing, Coding & Transcription"",
						""Medical Specialties"",
						""Nursing"",
						""Nutrition & Fitness"",
						""Pharmacology"",
						""Physical & Occupational Therapy"",
						""Public Health"",
						""Radiology & Imaging Sciences"",
						""Gerontology"",
						""Health Sciences"",
						""Medical Diagnostic"",
						""Paramedic & EMT"",
						""Clinical Laboratory Science"",
						""Biomedical Science"",
						""Communication Science & Disorders"",
						""Architecture"",
						""Biology"",
						""Physical Sciences"",
						""Engineering"",
						""Environmental Science"",
						""Mathematics & Statistics"",
						""Aerospace Engineering"",
						""Biological & Biomedical Engineering"",
						""Biomedical Science"",
						""Electrical Engineering"",
						""Environmental Engineering"",
						""Industrial & Mechanical Engineering"",
						""Physiology"",
						""Veterinary & Animal Sciences"",
						""Chemical Engineering"",
						""Civil Engineering"",
						""Engineering Management"",
						""Neuroscience"",
						""Economics"",
						""Public Administration & Policy"",
						""Human Services"",
						""Public Health"",
						""Anthropology"",
						""Gerontology"",
						""Political Science"",
						""Psychology"",
						""Social Work"",
						""Sociology"",
						""Emergency Management"",
						""Environmental Management"",
						""Counseling Psychology"",
						""Educational & School Psychology"",
						""Archaeology"",
						""Conflict & Peace Studies"",
						""Non-Profit Administration"",
						""Urban Affairs & Planning"",
						""Neuroscience"",
						""International Relations"",
						""Automotive & Mechanics"",
						""Beauty, Cosmetology & Esthetics"",
						""Carpentry & Construction"",
						""Commercial Transportation"",
						""Culinary & Catering"",
						""Personal Services"",
						""Plumbing"",
						""Skilled & Production  Trades"",
						""High School Diploma – Accredited"",
						""Massage Therapy"",
						""HVAC Technologies"",
						""Electrical Technology"",
						""Truck, Tractor and Bus Licenses"",
						""Aviation Maintenance"",
						""Criminal Justice & Criminalistics"",
						""Forensic Science"",
						""Homeland Security & National Defense"",
						""Law Enforcement, Policing & Investigation"",
						""Legal Studies"",
						""Paralegal"",
						""Biblical Studies"",
						""Christian Counseling"",
						""Religious Studies"",
						""Ministry"",
						""Theology""
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
		},
		{
			""condition"": ""OR"",
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""DegreeLevel"",
					""field"": ""DegreeLevel"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Associate"",
						""Bachelor"",
						""Bootcamp"",
						""Course"",
						""Diploma"",
						""Doctorate"",
						""Gap Year"",
						""Graduate"",
						""Graduate Certificate"",
						""High School"",
						""Master"",
						""Non Degree Award"",
						""Professional Degree"",
						""Undergraduate"",
						""Undergraduate Certificate""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""LeadDeliveredTo"",
					""field"": ""LeadDeliveredTo"",
					""type"": ""string"",
					""input"": ""textarea"",
					""operator"": ""is"",
					""value"": [
						""option1, option2""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""TrackingTags"",
					""field"": ""TrackingTags"",
					""type"": ""string"",
					""input"": ""textarea"",
					""operator"": ""is"",
					""value"": [
						""option1, option2""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""Browser"",
					""field"": ""Browser"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Chrome"",
						""Edge"",
						""Facebook"",
						""Firefox"",
						""IE"",
						""Safari"",
						""Samsung Browser"",
						""Instagram"",
						""Other""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""BrowserPlatform"",
					""field"": ""BrowserPlatform"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Android"",
						""iOS"",
						""Mac"",
						""Windows"",
						""Chrome"",
						""Linux"",
						""Other""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""ConnectionSource"",
					""field"": ""ConnectionSource"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""University""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""ConnectionType"",
					""field"": ""ConnectionType"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Broadband"",
						""Cellular"",
						""Corporate"",
						""Dialup"",
						""Unknown""
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
		},
		{
			""condition"": ""OR"",
			""rules"": [
				{
					""id"": ""DeviceType"",
					""field"": ""DeviceType"",
					""type"": ""string"",
					""input"": ""checkbox"",
					""operator"": ""is"",
					""value"": [
						""Mobile"",
						""Computer"",
						""Tablet"",
						""Other""
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
}";
	    #endregion
        private const string NewtonSoftJson = "NewtonSoftJson";
        private const string NewtonSoftJsonWithCustomParser = "NewtonSoftJsonWithCustomParser";
        private const string SystemTextJson = "SystemTextJson";

        [DataTestMethod]
        [DataRow(NewtonSoftJson)]
        [DataRow(NewtonSoftJsonWithCustomParser)]
        [DataRow(SystemTextJson)]
        public void RuleEngine_JsonFromGlassPanel15WithEveryPossibleOption_PassTrue(string serializationOption)
        {
            for (int i = 0; i < 10; i++)
            {
                //arrange
                var age = 15;
                var ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
                var sourceValues = new SourceValuesGenerator()
                    .AddRandomValues()
                    .AddAgeValues(age.ToString())
                    .AddIdOrFieldValues(IdOrField.EducationLevel, Constants.EducationLevel.EducationLevelGed.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.CollegeCredits, Constants.CollegeCredits.CollegeCredits1_29Credits.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.Gender, Constants.Gender.GenderMale.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.HighSchoolGPA, Constants.HighSchoolGPA.HighSchoolGPA2_75To2_9.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.HighSchoolGradYear, 2005)
                    .AddIdOrFieldValues(IdOrField.Zip, "75032")
                    .AddIdOrFieldValues(IdOrField.MilitaryAffiliation, Constants.MilitaryAffiliation.Yes.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.Income, Constants.Income.Income60000_100000.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.MilitaryBranch, Constants.MilitaryBranch.MilitaryBranchAFToSpouseofADorSR.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.PlannedStart, Constants.PlannedStart.PlannedStartImmediately.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.RegisteredNurse, Constants.RegisteredNurse.CurrentlyObtaining.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.TeachingCert, Constants.TeachingCertificate.Yes.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.USCitizen, Constants.USCitizen.Yes.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.MarketingUnits, Constants.MarketingUnit.MarketingUnitCampusExplorerLeadScoreTraditionalDisplay.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.SubChannels, Constants.SubChannel.SubChannelClicks.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.Country, Constants.Country.CountryOther.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.State, Constants.State.StateTX.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.AreaOfStudy, Constants.AreaOfStudy.AreaOfStudyLiberalArtsAndHumanities.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.Subject, Constants.Subject.SubjectArchaeology.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.DegreeLevel, Constants.DegreeLevel.DegreeLevelDoctorate.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.LeadDeliveredTo, "option1")
                    .AddIdOrFieldValues(IdOrField.TrackingTags, "option1")
                    .AddIdOrFieldValues(IdOrField.LearningPreference, Constants.LearningPreference.LearningPreferenceCampusorOnline.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.Browser, Constants.Browser.BrowserIE.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.BrowserPlatform, Constants.BrowserPlatform.BrowserPlatformMac.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.ConnectionSource, Constants.ConnectionSource.University.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.ConnectionType, Constants.ConnectionType.ConnectionTypeCellular.AsString(EnumFormat.Description))
                    .AddIdOrFieldValues(IdOrField.DeviceType, Constants.DeviceType.DeviceTypeMobile.AsString(EnumFormat.Description))
                    .Build();


                QueryBuilderFilterRule queryBuilderFilterRule = null;
                switch (serializationOption)
                {
                    case NewtonSoftJson:
                        queryBuilderFilterRule = GetQueryBuilderFilterUsingNewtonsoftJson();
                        break;
                    case NewtonSoftJsonWithCustomParser:
                        queryBuilderFilterRule = GetQueryBuilderFilterUsingNewtonsoftJsonWithCustomParser();
                        break;
                    case SystemTextJson:
                        queryBuilderFilterRule = GetQueryBuilderFilterUsingJsonSerializer();
                        break;

                }

                //act
                var res = ruleEngine.EvaluateRulesForDictionaryAndQueryBuilderFilterRule(sourceValues, queryBuilderFilterRule);

                //assert
                Assert.IsNotNull(res);
                Assert.IsTrue(string.IsNullOrEmpty(res.ReasonDidntPass), res.ReasonDidntPass);
                Assert.IsTrue(res.Pass);
            }
        }

        private static QueryBuilderFilterRule GetQueryBuilderFilterUsingNewtonsoftJson()
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<QueryBuilderFilterRule>(LongJson);
        }

        private static QueryBuilderFilterRule GetQueryBuilderFilterUsingNewtonsoftJsonWithCustomParser()
        {
            var reader = new Newtonsoft.Json.JsonTextReader(new StringReader(LongJson));
            while (reader.Read())
            {
                if (reader.Value != null)
                {
                    Trace.WriteLine($"Token: {reader.TokenType}, Value: {reader.Value}");
                }
                else
                {
                    Trace.WriteLine($"Token: {reader.TokenType}");
                }
            }
            return Newtonsoft.Json.JsonConvert.DeserializeObject<QueryBuilderFilterRule>(LongJson);

        }

        private static QueryBuilderFilterRule GetQueryBuilderFilterUsingJsonSerializer()
        {
            var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};
			return JsonSerializer.Deserialize<QueryBuilderFilterRule>(LongJson, options);
        }

        [TestMethod]
        public void RuleEngine_JsonFromGlassPanel15AgeRange_PassTrue()
        {
            //arrange
            var age = 15;
            var ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
            var sourceValues = new SourceValuesGenerator()
                .AddRandomValues()
                .AddAgeValues(age.ToString())
                .Build();

            var queryBuilderFilterRule = Newtonsoft.Json.JsonConvert.DeserializeObject<QueryBuilderFilterRule>(@"
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
                    ""operator"": ""range"",
					""value"": [
						""14"",
						""23""
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
        [DataRow(IdOrField.Age, 15, Operator.Range, 14, 16)]
        [DataRow(IdOrField.HighSchoolGradYear, 2005, Operator.Range, 2000, 2010)]
        [DataRow(IdOrField.Age, 18, Operator.Not_In_Range, 14, 16)]
        [DataRow(IdOrField.HighSchoolGradYear, 2015, Operator.Not_In_Range, 2000, 2010)]
        /*Happy path method, condition for the operator is met*/
        public void RuleEngine_Operator_PassTrue(IdOrField idOrField, object referenceValue, Operator op, int firstOperatorParameter, int secondOperatorParameter)
        {
            //arrange
            var ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
            var sourceValues = new SourceValuesGenerator()
                .AddRandomValues()
                .AddIdOrFieldValues(idOrField, referenceValue.ToString())
                .Build();

            var queryBuilderFilterRule = new QueryBuilderGenerator()
                .AddIdOrBinaryFieldOperatorThan(idOrField, op, firstOperatorParameter.ToString(), secondOperatorParameter.ToString())
                .Build();

            //act
            var res = ruleEngine.EvaluateRulesForDictionaryAndQueryBuilderFilterRule(sourceValues, queryBuilderFilterRule);

            //assert
            Assert.IsNotNull(res);
            Assert.IsTrue(string.IsNullOrEmpty(res.ReasonDidntPass), res.ReasonDidntPass);
            Assert.IsTrue(res.Pass);
        }


        [DataTestMethod]
        [DataRow(IdOrField.Age, 22, Operator.Range, 14, 16)]
        [DataRow(IdOrField.HighSchoolGradYear, 2015, Operator.Range, 2000, 2010)]
        [DataRow(IdOrField.Age, 15, Operator.Not_In_Range, 14, 16)]
        [DataRow(IdOrField.HighSchoolGradYear, 2005, Operator.Not_In_Range, 2000, 2010)]
        /*Happy path method, condition for the operator is met*/
        public void RuleEngine_Operator_Fails(IdOrField idOrField, object referenceValue, Operator op, int firstOperatorParameter, int secondOperatorParameter)
        {
            //arrange
            var ruleEngine = new RuleEngine.CustomRuleEngine.RuleEngine();
            var sourceValues = new SourceValuesGenerator()
                .AddRandomValues()
                .AddIdOrFieldValues(idOrField, referenceValue.ToString())
                .Build();

            var queryBuilderFilterRule = new QueryBuilderGenerator()
                .AddIdOrBinaryFieldOperatorThan(idOrField, op, firstOperatorParameter.ToString(), secondOperatorParameter.ToString())
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
