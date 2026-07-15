using System.ComponentModel;

namespace EDDY.IS.Common.ConstantsAndEnums
{
    public static class Constants
    {
        public static readonly string IfThisValueIsPresentTheClickWillBeAccepted = "If this variable is unknown, the click will be accepted.";

        public enum Income
        {
            [Description("Less than $20,000")] IncomeLessthan20000 = 1,
            [Description("$20,000 - $40,000")] Income20000_40000 = 2,
            [Description("$40,000 - $60,000")] Income40000_60000 = 3,
            [Description("$60,000 - $100,000")] Income60000_100000 = 4,
            [Description("More than $100,000")] IncomeMorethan100000 = 5
        }

        public enum AreaOfStudy
        {
            [Description("Fine Arts & Design")] AreaOfStudyFineArtsAndDesign = 20,
            [Description("Business")] AreaOfStudyBusiness = 21,
            [Description("Technology")] AreaOfStudyTechnology = 22,
            [Description("Liberal Arts & Humanities")] AreaOfStudyLiberalArtsAndHumanities = 23,
            [Description("Education")] AreaOfStudyEducation = 24,
            [Description("Health & Medicine")] AreaOfStudyHealthAndMedicine = 25,
            [Description("Math, Science & Engineering")] AreaOfStudyMathScienceAndEngineering = 26,
            [Description("Public Affairs & Social Sciences")] AreaOfStudyPublicAffairsAndSocialSciences = 27,
            [Description("Vocational Training")] AreaOfStudyVocationalTraining = 28,
            [Description("Criminal Justice & Legal")] AreaOfStudyCriminalJusticeAndLegal = 29,
            [Description("Religious Studies")] AreaOfStudyReligiousStudies = 30
        }

        public enum Browser
        {
            [Description("Chrome")] BrowserChrome = 1,
            [Description("Edge")] BrowserEdge = 2,
            [Description("Facebook")] BrowserFacebook = 3,
            [Description("Firefox")] BrowserFirefox = 4,
            [Description("IE")] BrowserIE = 5,
            [Description("Safari")] BrowserSafari = 6,
            [Description("Samsung Browser")] BrowserSamsungBrowser = 7,
            [Description("Instagram")] BrowserInstagram = 8,
            [Description("Other")] BrowserOther = 9,
        }

        public enum BrowserPlatform
        {
            [Description("Android")] BrowserPlatformAndroid = 1,
            [Description("iOS")] BrowserPlatformiOS = 2,
            [Description("Mac")] BrowserPlatformMac = 3,
            [Description("Windows")] BrowserPlatformWindows = 4,
            [Description("Chrome")] BrowserPlatformChrome = 5,
            [Description("Linux")] BrowserPlatformLinux = 6,
            [Description("Other")] BrowserPlatformOther = 7
        }

        public enum CollegeCredits
        {
            [Description("1 - 29 Credits")] CollegeCredits1_29Credits = 1,
            [Description("30 - 59 Credits")] CollegeCredits30_59Credits = 2,
            [Description("60 - 89 Credits")] CollegeCredits60_89Credits = 3,
            [Description("90+ Credits")] CollegeCredits90PlusCredits = 4
        }

        public enum ConnectionType
        {
            [Description("Broadband")] ConnectionTypeBroadband = 1,
            [Description("Cellular")] ConnectionTypeCellular = 2,
            [Description("Corporate")] ConnectionTypeCorporate = 3,
            [Description("Dialup")] ConnectionTypeDialup = 4,
            [Description("Unknown")] ConnectionTypeUnknown = 5
        }

        public enum Country
        {
            [Description("United States")] CountryUnitedStates = 1,
            [Description("US Territories")] CountryUSTerritories = 2,
            [Description("Military Bases")] CountryMilitaryBases = 3,
            [Description("Other")] CountryOther = 4
        }

        public enum DegreeLevel
        {
            [Description("Associate")] DegreeLevelAssociate = 2,
            [Description("Bachelor")] DegreeLevelBachelor = 3,
            [Description("Course")] DegreeLevelCourse = 6,
            [Description("Doctorate")] DegreeLevelDoctorate = 7,
            [Description("Master")] DegreeLevelMaster = 8,
            [Description("Graduate Certificate")] DegreeLevelGraduateCertificate = 11,
            [Description("High School")] DegreeLevelHighSchool = 12,
            [Description("Non Degree Award")] DegreeLevelNonDegreeAward = 14,
            [Description("Professional Degree")] DegreeLevelProfessionalDegree = 16,
            [Description("Undergraduate Certificate")] DegreeLevelUndergraduateCertificate = 18,
            [Description("Diploma")] DegreeLevelDiploma = 22,
            [Description("Gap Year")] DegreeLevelGapYear = 23,
            [Description("Undergraduate")] DegreeLevelUndergraduate = 24,
            [Description("Graduate")] DegreeLevelGraduate = 25,
            [Description("Bootcamp")] DegreeLevelBootcamp = 26,
        }

        public enum DeviceType
        {
            [Description("Mobile")] DeviceTypeMobile = 1,
            [Description("Computer")] DeviceTypeComputer = 2,
            [Description("Tablet")] DeviceTypeTablet = 3,
            [Description("Other")] DeviceTypeOther = 4
        }

        public enum EducationLevel
        {
            [Description("Haven't completed High School")] EducationLevelHaventcompletedHighSchool = 1,
[Description("G.E.D.")] EducationLevelGed = 2,
            [Description("High School Diploma")] EducationLevelHighSchoolDiploma = 3,
            [Description("Associate")] EducationLevelAssociate = 8,
            [Description("Bachelor")] EducationLevelBachelor = 9,
            [Description("Master")] EducationLevelMaster = 10,
            [Description("Doctorate")] EducationLevelDoctorate = 11,
            [Description("Some College")] EducationLevelSomeCollege = 4
        }

        public enum Gender
        {
            [Description("Male")] GenderMale = 1,
            [Description("Female")] GenderFemale = 2,
            [Description("Non-binary")] GenderNon_binary = 3,
        }

        public enum HighSchoolGPA
        {
            [Description("Less than 2.0")] HighSchoolGPALessthan2_0 = 7,
            [Description("2.0-2.4")] HighSchoolGPA2_0To2_4 = 8,
            [Description("2.5-2.74")] HighSchoolGPA2_5To2_74 = 9,
            [Description("2.75-2.9")] HighSchoolGPA2_75To2_9 = 10,
            [Description("3.5 or Higher")] HighSchoolGPA3_5orHigher = 11,
            [Description("3.0-3.4")] HighSchoolGPA3_0To3_4 = 57,
        }

        public enum LearningPreference
        {
            [Description("Campus Only")] LearningPreferenceCampusOnly = 1,
            [Description("Campus or Online")] LearningPreferenceCampusorOnline = 2,
            [Description("Online")] LearningPreferenceOnline = 3
        }

        public enum MilitaryBranch
        {
            [Description("No Military Affiliation")] MilitaryBranchNoMilitaryAffiliation = 126,
            [Description("AF - Active Duty(AD)")] MilitaryBranchAFToActiveDutyAD = 101,
            [Description("AF - Civilian")] MilitaryBranchAFToCivilian = 105,
            [Description("AF - Selective Reserve(SR)")] MilitaryBranchAFToSelectiveReserveSR = 102,
            [Description("AF - Spouse of AD or SR")] MilitaryBranchAFToSpouseofADorSR = 103,
            [Description("AF - Veteran")] MilitaryBranchAFToVeteran = 104,
            [Description("AR - Active Duty(AD)")] MilitaryBranchARToActiveDutyAD = 106,
            [Description("AR - Civilian")] MilitaryBranchARToCivilian = 110,
            [Description("AR - Selective Reserve(SR)")] MilitaryBranchARToSelectiveReserveSR = 107,
            [Description("AR - Spouse of AD or SR")] MilitaryBranchARToSpouseofADorSR = 108,
            [Description("AR - Veteran")] MilitaryBranchARToVeteran = 109,
            [Description("CG - Active Duty(AD)")] MilitaryBranchCGToActiveDutyAD = 111,
            [Description("CG - Civilian")] MilitaryBranchCGToCivilian = 115,
            [Description("CG - Selective Reserve(SR)")] MilitaryBranchCGToSelectiveReserveSR = 112,
            [Description("CG - Spouse of AD or SR")] MilitaryBranchCGToSpouseofADorSR = 113,
            [Description("CG - Veteran")] MilitaryBranchCGToVeteran = 114,
            [Description("MC - Active Duty(AD)")] MilitaryBranchMCToActiveDutyAD = 116,
            [Description("MC - Civilian")] MilitaryBranchMCToCivilian = 120,
            [Description("MC - Selective Reserve(SR)")] MilitaryBranchMCToSelectiveReserveSR = 117,
            [Description("MC - Spouse of AD or SR")] MilitaryBranchMCToSpouseofADorSR = 118,
            [Description("MC - Veteran")] MilitaryBranchMCToVeteran = 119,
            [Description("NV - Active Duty(AD)")] MilitaryBranchNVToActiveDutyAD = 121,
            [Description("NV - Civilian")] MilitaryBranchNVToCivilian = 125,
            [Description("NV - Selective Reserve(SR)")] MilitaryBranchNVToSelectiveReserveSR = 122,
            [Description("NV - Spouse of AD or SR")] MilitaryBranchNVToSpouseofADorSR = 123,
            [Description("NV - Veteran")] MilitaryBranchNVToVeteran = 124,
        }

        public enum MarketingUnit
        {
            [Description("Inactive_50onRed (Traditional Display)")] MarketingUnitInactive_50onRedTraditionalDisplay = 1,
            [Description("Inactive_AOL (Traditional Display)")] MarketingUnitInactive_AOLTraditionalDisplay = 2,
            [Description("Clicks.Net (Traditional Display)")] MarketingUnitClicksNetTraditionalDisplay = 3,
            [Description("Future Ads PPC (Traditional Display)")] MarketingUnitFutureAdsPPCTraditionalDisplay = 4,
            [Description("Future Ads PPV (Traditional Display)")] MarketingUnitFutureAdsPPVTraditionalDisplay = 5,
            [Description("Inactive_Other Vendors (Traditional Display)")] MarketingUnitInactive_OtherVendorsTraditionalDisplay = 6,
            [Description("Degree PPC (GDN)")] MarketingUnitDegreePPCGDN = 7,
            [Description("Financial Aid (GDN)")] MarketingUnitFinancialAidGDN = 8,
            [Description("Inactive_Other (GDN)")] MarketingUnitInactive_OtherGDN = 9,
            [Description("Religion (GDN)")] MarketingUnitReligionGDN = 10,
            [Description("Remarketing (GDN)")] MarketingUnitRemarketingGDN = 11,
            [Description("General (Call Center Partners)")] MarketingUnitGeneralCallCenterPartners = 13,
            [Description("Job Board - All (Call Center Partners)")] MarketingUnitJobBoardToAllCallCenterPartners = 14,
            [Description("Job Board - Campus (Call Center Partners)")] MarketingUnitJobBoardToCampusCallCenterPartners = 15,
            [Description("Non-Job Board - All (Call Center Partners)")] MarketingUnitNonToJobBoardToAllCallCenterPartners = 16,
            [Description("Non-Job Board - Campus (Call Center Partners)")] MarketingUnitNonToJobBoardToCampusCallCenterPartners = 17,
            [Description("General (Online Partners)")] MarketingUnitGeneralOnlinePartners = 18,
            [Description("Host And Post (Online Partners)")] MarketingUnitHostAndPostOnlinePartners = 19,
            [Description("Job Board (Online Partners)")] MarketingUnitJobBoardOnlinePartners = 20,
            [Description("SEO (Online Partners)")] MarketingUnitSEOOnlinePartners = 21,
            [Description("Branded (Paid Search)")] MarketingUnitBrandedPaidSearch = 22,
            [Description("Branded (mobile) (Paid Search)")] MarketingUnitBrandedmobilePaidSearch = 23,
            [Description("Degree (Paid Search)")] MarketingUnitDegreePaidSearch = 24,
            [Description("Degree (mobile) (Paid Search)")] MarketingUnitDegreemobilePaidSearch = 25,
            [Description("Financial Aid (Paid Search)")] MarketingUnitFinancialAidPaidSearch = 26,
            [Description("Financial Aid (mobile) (Paid Search)")] MarketingUnitFinancialAidmobilePaidSearch = 27,
            [Description("Head Terms (Paid Search)")] MarketingUnitHeadTermsPaidSearch = 28,
            [Description("Head Terms (mobile) (Paid Search)")] MarketingUnitHeadTermsmobilePaidSearch = 29,
            [Description("School (Paid Search)")] MarketingUnitSchoolPaidSearch = 31,
            [Description("School (mobile) (Paid Search)")] MarketingUnitSchoolmobilePaidSearch = 32,
            [Description("SEO (Paid Search)")] MarketingUnitSEOPaidSearch = 33,
            [Description("Internal Data Purchase Agreements (Internal Data Purchase Agreements)")] MarketingUnitInternalDataPurchaseAgreementsInternalDataPurchaseAgreements = 34,
            [Description("External Data Purchase Agreements (External Data Purchase Agreements)")] MarketingUnitExternalDataPurchaseAgreementsExternalDataPurchaseAgreements = 35,
            [Description("Internal Transfer Agreements (Internal Transfer Agreements)")] MarketingUnitInternalTransferAgreementsInternalTransferAgreements = 36,
            [Description("External Transfer Agreements (External Transfer Agreements)")] MarketingUnitExternalTransferAgreementsExternalTransferAgreements = 37,
            [Description("Adware (Adware)")] MarketingUnitAdwareAdware = 38,
            [Description("DRTV & Video Ads (DRTV & Video Ads)")] MarketingUnitDRTVAndVideoAdsDRTVAndVideoAds = 39,
            [Description("External Email (External Email)")] MarketingUnitExternalEmailExternalEmail = 40,
            [Description("Internal Email (Internal Email)")] MarketingUnitInternalEmailInternalEmail = 41,
            [Description("Kiosks (Kiosks)")] MarketingUnitKiosksKiosks = 42,
            [Description("Events (Events)")] MarketingUnitEventsEvents = 43,
            [Description("FinancialAidV2 (Paid Search)")] MarketingUnitFinancialAidV2PaidSearch = 44,
            [Description("GradSchools (Paid Search)")] MarketingUnitGradSchoolsPaidSearch = 45,
            [Description("SEO (SEO)")] MarketingUnitSEOSEO = 46,
            [Description("Religion (Paid Search)")] MarketingUnitReligionPaidSearch = 47,
            [Description("Inactive_Paid Media Prospecting (Traditional Display)")] MarketingUnitInactive_PaidMediaProspectingTraditionalDisplay = 48,
            [Description("Inactive_Paid Media Prospecting (GDN)")] MarketingUnitInactive_PaidMediaProspectingGDN = 49,
            [Description("Paid Media Prospecting (Paid Search)")] MarketingUnitPaidMediaProspectingPaidSearch = 50,
            [Description("Generic (Paid Search)")] MarketingUnitGenericPaidSearch = 52,
            [Description("General New (Paid Search)")] MarketingUnitGeneralNewPaidSearch = 53,
            [Description("Inactive_NRE (Traditional Display)")] MarketingUnitInactive_NRETraditionalDisplay = 54,
            [Description("Inactive_NRE (GDN)")] MarketingUnitInactive_NREGDN = 55,
            [Description("NRE (Paid Search)")] MarketingUnitNREPaidSearch = 56,
            [Description("Inactive_Vantage Media (Traditional Display)")] MarketingUnitInactive_VantageMediaTraditionalDisplay = 57,
            [Description("Inactive_DGS (Traditional Display)")] MarketingUnitInactive_DGSTraditionalDisplay = 58,
            [Description("Bing Partner (Paid Search)")] MarketingUnitBingPartnerPaidSearch = 59,
            [Description("TestDoNoUse (TestDoNoUse)")] MarketingUnitTestDoNoUseTestDoNoUse = 61,
            [Description("Targeted Campus (Traditional Display)")] MarketingUnitTargetedCampusTraditionalDisplay = 62,
            [Description("Targeted Campus (GDN)")] MarketingUnitTargetedCampusGDN = 63,
            [Description("Targeted Campus (Paid Search)")] MarketingUnitTargetedCampusPaidSearch = 64,
            [Description("Exclusive Lead (Traditional Display)")] MarketingUnitExclusiveLeadTraditionalDisplay = 65,
            [Description("Exclusive Lead (Paid Search)")] MarketingUnitExclusiveLeadPaidSearch = 67,
            [Description("Generic (Traditional Display)")] MarketingUnitGenericTraditionalDisplay = 68,
            [Description("Generic (GDN)")] MarketingUnitGenericGDN = 69,
            [Description("GradSchools Subject (Paid Search)")] MarketingUnitGradSchoolsSubjectPaidSearch = 72,
            [Description("Home Security (Paid Search)")] MarketingUnitHomeSecurityPaidSearch = 73,
            [Description("Campus Explorer (Traditional Display)")] MarketingUnitCampusExplorerTraditionalDisplay = 74,
            [Description("Inactive_Vantage Campus Explorer (Traditional Display)")] MarketingUnitInactive_VantageCampusExplorerTraditionalDisplay = 75,
            [Description("Future Ads Premier (Traditional Display)")] MarketingUnitFutureAdsPremierTraditionalDisplay = 78,
            [Description("Inactive_AOL Remarketing (Traditional Display)")] MarketingUnitInactive_AOLRemarketingTraditionalDisplay = 80,
            [Description("GradLevel (Paid Search)")] MarketingUnitGradLevelPaidSearch = 82,
            [Description("Bing Partner Lead Score (Paid Search)")] MarketingUnitBingPartnerLeadScorePaidSearch = 84,
            [Description("Clicks.net Premier (Traditional Display)")] MarketingUnitClicksnetPremierTraditionalDisplay = 85,
            [Description("Look-alike (Social Media)")] MarketingUnitLookToalikeSocialMedia = 87,
            [Description("Retargeting (Social Media)")] MarketingUnitRetargetingSocialMedia = 88,
            [Description("Custom Campaign (Custom Campaign)")] MarketingUnitCustomCampaignCustomCampaign = 92,
            [Description("LowCostvendor (Call Center Partners)")] MarketingUnitLowCostvendorCallCenterPartners = 93,
            [Description("LowCostvendor (Online Partners)")] MarketingUnitLowCostvendorOnlinePartners = 94,
            [Description("Future Ads PPV Lead Score (Traditional Display)")] MarketingUnitFutureAdsPPVLeadScoreTraditionalDisplay = 97,
            [Description("Future Ads Premier Lead Score (Traditional Display)")] MarketingUnitFutureAdsPremierLeadScoreTraditionalDisplay = 98,
            [Description("Inactive_Vantage Media Campus Explorer Lead Score (Traditional Display)")] MarketingUnitInactive_VantageMediaCampusExplorerLeadScoreTraditionalDisplay = 99,
            [Description("Inactive_Vantage Media Lead Score (Traditional Display)")] MarketingUnitInactive_VantageMediaLeadScoreTraditionalDisplay = 100,
            [Description("Degree Lead Score (GDN)")] MarketingUnitDegreeLeadScoreGDN = 101,
            [Description("Financial Aid Lead Score (Paid Search)")] MarketingUnitFinancialAidLeadScorePaidSearch = 106,
            [Description("Campus Explorer Lead Score (Traditional Display)")] MarketingUnitCampusExplorerLeadScoreTraditionalDisplay = 107,
            [Description("Social Media Exclusive Leads (Social Media)")] MarketingUnitSocialMediaExclusiveLeadsSocialMedia = 109,
            [Description("Best Online Universities (Online Partners)")] MarketingUnitBestOnlineUniversitiesOnlinePartners = 110,
            [Description("SEO Partners (Online Partners)")] MarketingUnitSEOPartnersOnlinePartners = 111,
            [Description("Division-D Popunder (Traditional Display)")] MarketingUnitDivisionToDPopunderTraditionalDisplay = 113,
            [Description("Inactive_Division D Standard (Traditional Display)")] MarketingUnitInactive_DivisionDStandardTraditionalDisplay = 114,
            [Description("Inactive_Orion CKB (Social Media)")] MarketingUnitInactive_OrionCKBSocialMedia = 115,
            [Description("Inactive_Adroll (Traditional Display)")] MarketingUnitInactive_AdrollTraditionalDisplay = 116,
            [Description("Inactive_Adroll Facebook (Traditional Display)")] MarketingUnitInactive_AdrollFacebookTraditionalDisplay = 117,
            [Description("Job Board – All (Internal Transfer Agreements)")] MarketingUnitJobBoardAllInternalTransferAgreements = 118,
            [Description("Non-Job Board - All (Internal Transfer Agreements)")] MarketingUnitNonToJobBoardToAllInternalTransferAgreements = 119,
            [Description("ChristianEdu (Social Media)")] MarketingUnitChristianEduSocialMedia = 122,
            [Description("ChristianEdu Exclusive (Social Media)")] MarketingUnitChristianEduExclusiveSocialMedia = 123,
            [Description("Jobcase (Internal Transfer Agreements)")] MarketingUnitJobcaseInternalTransferAgreements = 124,
            [Description("Jobcase H&P (Online Partners)")] MarketingUnitJobcaseHAndPOnlinePartners = 126,
            [Description("Yahoo-Gemini (Paid Search)")] MarketingUnitYahooToGeminiPaidSearch = 127,
            [Description("Clicks.net Non-premier (Traditional Display)")] MarketingUnitClicksnetNonTopremierTraditionalDisplay = 128,
            [Description("Degree DSK (GDN)")] MarketingUnitDegreeDSKGDN = 129,
            [Description("Internal Ads (Internal Data Purchase Agreements)")] MarketingUnitInternalAdsInternalDataPurchaseAgreements = 130,
            [Description("Internal Ads (External Data Purchase Agreements)")] MarketingUnitInternalAdsExternalDataPurchaseAgreements = 131,
            [Description("Internal Ads (Internal Transfer Agreements)")] MarketingUnitInternalAdsInternalTransferAgreements = 132,
            [Description("Internal Ads (External Transfer Agreements)")] MarketingUnitInternalAdsExternalTransferAgreements = 133,
            [Description("Internal Ads (Adware)")] MarketingUnitInternalAdsAdware = 135,
            [Description("Internal Ads (Call Center Partners)")] MarketingUnitInternalAdsCallCenterPartners = 137,
            [Description("Internal Ads (DRTV & Video Ads)")] MarketingUnitInternalAdsDRTVAndVideoAds = 138,
            [Description("Internal Ads (External Email)")] MarketingUnitInternalAdsExternalEmail = 139,
            [Description("Internal Ads (Online Partners)")] MarketingUnitInternalAdsOnlinePartners = 141,
            [Description("Internal Ads (Kiosks)")] MarketingUnitInternalAdsKiosks = 142,
            [Description("Internal Ads (Events)")] MarketingUnitInternalAdsEvents = 143,
            [Description("Internal Ads (Paid Search)")] MarketingUnitInternalAdsPaidSearch = 144,
            [Description("Internal Ads (SEO)")] MarketingUnitInternalAdsSEO = 145,
            [Description("Internal Ads (TestDoNoUse)")] MarketingUnitInternalAdsTestDoNoUse = 146,
            [Description("Internal Ads (Social Media)")] MarketingUnitInternalAdsSocialMedia = 147,
            [Description("Internal Ads (Custom Campaign)")] MarketingUnitInternalAdsCustomCampaign = 148,
            [Description("Find Top Colleges (Adware)")] MarketingUnitFindTopCollegesAdware = 150,
            [Description("Inactive_Find Top Colleges (GDN)")] MarketingUnitInactive_FindTopCollegesGDN = 151,
            [Description("Find Top Colleges (External Email)")] MarketingUnitFindTopCollegesExternalEmail = 152,
            [Description("Find Top Colleges (Online Partners)")] MarketingUnitFindTopCollegesOnlinePartners = 154,
            [Description("Find Top Colleges (Paid Search)")] MarketingUnitFindTopCollegesPaidSearch = 155,
            [Description("Inactive_Gravity (Traditional Display)")] MarketingUnitInactive_GravityTraditionalDisplay = 157,
            [Description("GS.com SEO (SEO)")] MarketingUnitGScomSEOSEO = 159,
            [Description("Remarketing Headterm (GDN)")] MarketingUnitRemarketingHeadtermGDN = 160,
            [Description("Paid Search – Remarketing (Paid Search)")] MarketingUnitPaidSearchRemarketingPaidSearch = 161,
            [Description("Clicks.net Kaplan Allow (Traditional Display)")] MarketingUnitClicksnetKaplanAllowTraditionalDisplay = 162,
            [Description("Inactive_Vantage Media Kaplan Premier (Traditional Display)")] MarketingUnitInactive_VantageMediaKaplanPremierTraditionalDisplay = 163,
            [Description("Degree Premier (Paid Search)")] MarketingUnitDegreePremierPaidSearch = 165,
            [Description("FinancialAid Premier (Paid Search)")] MarketingUnitFinancialAidPremierPaidSearch = 166,
            [Description("Clicks (Clicks)")] MarketingUnitClicksClicks = 168,
            [Description("General New Kaplan Allow (Paid Search)")] MarketingUnitGeneralNewKaplanAllowPaidSearch = 169,
            [Description("Generic Kaplan Allow (Paid Search)")] MarketingUnitGenericKaplanAllowPaidSearch = 170,
            [Description("Religion Remarketing GSP (GDN)")] MarketingUnitReligionRemarketingGSPGDN = 171,
            [Description("Gradschools SM=0 (Paid Search)")] MarketingUnitGradschoolsSM0PaidSearch = 173,
            [Description("Head Terms Partner (Paid Search)")] MarketingUnitHeadTermsPartnerPaidSearch = 174,
            [Description("Degree Premier Partner (Paid Search)")] MarketingUnitDegreePremierPartnerPaidSearch = 175,
            [Description("Financial Aid Premier Partner (Paid Search)")] MarketingUnitFinancialAidPremierPartnerPaidSearch = 176,
            [Description("GS SEO SM=0 (SEO)")] MarketingUnitGSSEOSM0SEO = 188,
            [Description("Gradschools Partner (Paid Search)")] MarketingUnitGradschoolsPartnerPaidSearch = 189,
            [Description("UAB (SEO)")] MarketingUnitUABSEO = 190,
            [Description("SAB (SEO)")] MarketingUnitSABSEO = 191,
            [Description("Taboola (Traditional Display)")] MarketingUnitTaboolaTraditionalDisplay = 192,
            [Description("EDU Ventures (Call Center Partners)")] MarketingUnitEDUVenturesCallCenterPartners = 193,
            [Description("Job Ventures (Call Center Partners)")] MarketingUnitJobVenturesCallCenterPartners = 194,
            [Description("EDU Ventures (Online Partners)")] MarketingUnitEDUVenturesOnlinePartners = 195,
            [Description("Host & Post SEO Partner (Online Partners)")] MarketingUnitHostAndPostSEOPartnerOnlinePartners = 196,
            [Description("Job Ventures (Online Partners)")] MarketingUnitJobVenturesOnlinePartners = 197,
            [Description("SEO PARTNER APP (Online Partners)")] MarketingUnitSEOPARTNERAPPOnlinePartners = 198,
            [Description("SEO Partners Ventures (Online Partners)")] MarketingUnitSEOPartnersVenturesOnlinePartners = 199,
            [Description("Overhead Costs (Overhead Costs)")] MarketingUnitOverheadCostsOverheadCosts = 200,
            [Description("GDN CPL (GDN)")] MarketingUnitGDNCPLGDN = 201,
            [Description("Military College (Paid Search)")] MarketingUnitMilitaryCollegePaidSearch = 202,
            [Description("Exclusive (Internal Email)")] MarketingUnitExclusiveInternalEmail = 203,
            [Description("SAB Social (Social Media)")] MarketingUnitSABSocialSocialMedia = 204,
            [Description("Military (GDN)")] MarketingUnitMilitaryGDN = 205,
            [Description("QuinSt Tier 1 (Traditional Display)")] MarketingUnitQuinStTier1TraditionalDisplay = 206,
            [Description("QuinSt Tier 2 (Traditional Display)")] MarketingUnitQuinStTier2TraditionalDisplay = 207,
            [Description("QuinSt Tier 3 (Traditional Display)")] MarketingUnitQuinStTier3TraditionalDisplay = 208,
            [Description("QuinSt Campus (Traditional Display)")] MarketingUnitQuinStCampusTraditionalDisplay = 209,
            [Description("QuinSt Non-premier (Traditional Display)")] MarketingUnitQuinStNonTopremierTraditionalDisplay = 210,
            [Description("Clicks Partners (Clicks)")] MarketingUnitClicksPartnersClicks = 211,
            [Description("Military (Traditional Display)")] MarketingUnitMilitaryTraditionalDisplay = 212,
            [Description("International (Online Partners)")] MarketingUnitInternationalOnlinePartners = 213,
            [Description("XYZ Partner (Online Partners)")] MarketingUnitXYZPartnerOnlinePartners = 214,
            [Description("EdDy AdServer (SEO)")] MarketingUnitEdDyAdServerSEO = 215,
            [Description("GS Podcast (SEO)")] MarketingUnitGSPodcastSEO = 216,
            [Description("Exclusive Prime (Paid Search)")] MarketingUnitExclusivePrimePaidSearch = 217,
            [Description("SAB Autoresponders (Internal Email)")] MarketingUnitSABAutorespondersInternalEmail = 218,
            [Description("Exclusive Prime (Clicks)")] MarketingUnitExclusivePrimeClicks = 219,
            [Description("EL SEO (SEO)")] MarketingUnitELSEOSEO = 220,
            [Description("SAB General Emails (Internal Email)")] MarketingUnitSABGeneralEmailsInternalEmail = 221,
            [Description("MediaAlpha (Traditional Display)")] MarketingUnitMediaAlphaTraditionalDisplay = 222,
            [Description("Religion TD (Traditional Display)")] MarketingUnitReligionTDTraditionalDisplay = 223,
            [Description("Registered Nurse (Traditional Display)")] MarketingUnitRegisteredNurseTraditionalDisplay = 224,
            [Description("Quinst Premier (Traditional Display)")] MarketingUnitQuinstPremierTraditionalDisplay = 225,
            [Description("Graduates (Traditional Display)")] MarketingUnitGraduatesTraditionalDisplay = 226,
            [Description("Quora (Traditional Display)")] MarketingUnitQuoraTraditionalDisplay = 227,
            [Description("GSX (SEO)")] MarketingUnitGSXSEO = 228,
            [Description("Click-to-lead Direct (Clicks)")] MarketingUnitClickTotoToleadDirectClicks = 229,
            [Description("Click-to-lead Aggregation (Clicks)")] MarketingUnitClickTotoToleadAggregationClicks = 230,
            [Description("Clicks Product Lead Delivery (Clicks)")] MarketingUnitClicksProductLeadDeliveryClicks = 231,
            [Description("Contact Center Services - EMS Only (Contact Center Services - EMS Only)")] MarketingUnitContactCenterServicesToEMSOnlyContactCenterServicesToEMSOnly = 232,
            [Description("Display - EMS Only (Display - EMS Only)")] MarketingUnitDisplayToEMSOnlyDisplayToEMSOnly = 233,
            [Description("Email - EMS Only (Email - EMS Only)")] MarketingUnitEmailToEMSOnlyEmailToEMSOnly = 234,
            [Description("Online Partners - EMS Only (Online Partners - EMS Only)")] MarketingUnitOnlinePartnersToEMSOnlyOnlinePartnersToEMSOnly = 235,
            [Description("Paid Search - EMS Only (Paid Search - EMS Only)")] MarketingUnitPaidSearchToEMSOnlyPaidSearchToEMSOnly = 236,
            [Description("Social Media - EMS Only (Social Media - EMS Only)")] MarketingUnitSocialMediaToEMSOnlySocialMediaToEMSOnly = 237,
            [Description("Social Media Leads (Social Media)")] MarketingUnitSocialMediaLeadsSocialMedia = 238,
            [Description("Linkedin (Traditional Display)")] MarketingUnitLinkedinTraditionalDisplay = 239,
            [Description("Quora Finaid (Traditional Display)")] MarketingUnitQuoraFinaidTraditionalDisplay = 240,
            [Description("UnigoSEO (SEO)")] MarketingUnitUnigoSEOSEO = 241,
            [Description("Partner PPC (Traditional Display)")] MarketingUnitPartnerPPCTraditionalDisplay = 242,
            [Description("Education (Inbound)")] MarketingUnitEducationInbound = 243,
            [Description("Job Board (Inbound)")] MarketingUnitJobBoardInbound = 244,
            [Description("Education (Outbound)")] MarketingUnitEducationOutbound = 245,
            [Description("Job Board (Outbound)")] MarketingUnitJobBoardOutbound = 246,
            [Description("Display EP (Traditional Display)")] MarketingUnitDisplayEPTraditionalDisplay = 247,
            [Description("Education Select (Outbound)")] MarketingUnitEducationSelectOutbound = 248,
            [Description("Call Center Partners (Call Center Partners)")] MarketingUnitCallCenterPartnersCallCenterPartners = 249,
            [Description("TOP SEO Partners (Online Partners)")] MarketingUnitTOPSEOPartnersOnlinePartners = 250,
            [Description("EDU Traffic Partners (Online Partners)")] MarketingUnitEDUTrafficPartnersOnlinePartners = 251,
            [Description("Youtube (Paid Search)")] MarketingUnitYoutubePaidSearch = 252,
            [Description("Social Media Broad (Social Media)")] MarketingUnitSocialMediaBroadSocialMedia = 253,
            [Description("Perform (Traditional Display)")] MarketingUnitPerformTraditionalDisplay = 254,
            [Description("Perform Finaid (Traditional Display)")] MarketingUnitPerformFinaidTraditionalDisplay = 255,
            [Description("MediaAlpha Core (Traditional Display)")] MarketingUnitMediaAlphaCoreTraditionalDisplay = 256,
            [Description("Premium Clicks (Paid Search)")] MarketingUnitPremiumClicksPaidSearch = 257,
            [Description("Display Job (Traditional Display)")] MarketingUnitDisplayJobTraditionalDisplay = 258,
            [Description("Criteo Retargeting (Traditional Display)")] MarketingUnitCriteoRetargetingTraditionalDisplay = 259,
            [Description("Criteo PPC (Traditional Display)")] MarketingUnitCriteoPPCTraditionalDisplay = 260,
            [Description("Adsupply PPC (Traditional Display)")] MarketingUnitAdsupplyPPCTraditionalDisplay = 261,
            [Description("Social Media Financial Aid (Social Media)")] MarketingUnitSocialMediaFinancialAidSocialMedia = 262
        }

        public enum PlannedStart
        {
            [Description("Immediately")] PlannedStartImmediately = 1,
            [Description("1 - 3 Months")] PlannedStart1To3Months = 2,
            [Description("4 - 6 Months")] PlannedStart4To6Months = 3,
            [Description("7 - 12 Months")] PlannedStart7To12Months = 4,
            [Description("More than 1 year")] PlannedStartMorethan1year = 5,
            [Description("Not Sure")] PlannedStartNotSure = 6
        }

        public enum SubChannel
        {
            [Description("Internal Data Purchase Agreements")] SubChannelInternalDataPurchaseAgreements = 1,
            [Description("External Data Purchase Agreements")] SubChannelExternalDataPurchaseAgreements = 2,
            [Description("Internal Transfer Agreements")] SubChannelInternalTransferAgreements = 3,
            [Description("External Transfer Agreements")] SubChannelExternalTransferAgreements = 4,
            [Description("Traditional Display")] SubChannelTraditionalDisplay = 5,
            [Description("Adware")] SubChannelAdware = 6,
            [Description("GDN")] SubChannelGDN = 7,
            [Description("Call Center Partners")] SubChannelCallCenterPartners = 8,
            [Description("DRTV & Video Ads")] SubChannelDRTVAndVideoAds = 9,
            [Description("External Email")] SubChannelExternalEmail = 10,
            [Description("Internal Email")] SubChannelInternalEmail = 11,
            [Description("Online Partners")] SubChannelOnlinePartners = 12,
            [Description("Kiosks")] SubChannelKiosks = 13,
            [Description("Events")] SubChannelEvents = 14,
            [Description("Paid Search")] SubChannelPaidSearch = 15,
            [Description("SEO")] SubChannelSEO = 16,
            [Description("TestDoNoUse")] SubChannelTestDoNoUse = 17,
            [Description("Social Media")] SubChannelSocialMedia = 18,
            [Description("Custom Campaign")] SubChannelCustomCampaign = 19,
            [Description("Clicks")] SubChannelClicks = 20,
            [Description("Overhead Costs")] SubChannelOverheadCosts = 21,
            [Description("API Check")] SubChannelAPICheck = 22,
            [Description("Contact Center Services - EMS Only")] SubChannelContactCenterServicesToEMSOnly = 23,
            [Description("Display - EMS Only")] SubChannelDisplayToEMSOnly = 24,
            [Description("Email - EMS Only")] SubChannelEmailToEMSOnly = 25,
            [Description("Online Partners - EMS Only")] SubChannelOnlinePartnersToEMSOnly = 26,
            [Description("Paid Search - EMS Only")] SubChannelPaidSearchToEMSOnly = 27,
            [Description("Social Media - EMS Only")] SubChannelSocialMediaToEMSOnly = 28,
            [Description("Inbound")] SubChannelInbound = 29,
            [Description("Outbound")] SubChannelOutbound = 30,
            [Description("SEO")] SubChannelSEO2 = 31,
            [Description("Exclusive")] SubChannelExclusive = 32,
            [Description("GSX")] SubChannelGSX = 33,
        }

        public enum Subject
        {
            [Description("Fashion, Retail & Merchandising")] SubjectFashionRetailAndMerchandising = 601,
            [Description("Fine Art")] SubjectFineArt = 602,
            [Description("Graphic Design & Multimedia")] SubjectGraphicDesignAndMultimedia = 603,
            [Description("Interior Design")] SubjectInteriorDesign = 604,
            [Description("Performing Arts")] SubjectPerformingArts = 605,
            [Description("Photography")] SubjectPhotography = 606,
            [Description("Music & Audio Production")] SubjectMusicAndAudioProduction = 607,
            [Description("Video Game Design")] SubjectVideoGameDesign = 608,
            [Description("Web Development & Design")] SubjectWebDevelopmentAndDesign = 609,
            [Description("Industrial Design")] SubjectIndustrialDesign = 757,
            [Description("Media Arts")] SubjectMediaArts = 759,
            [Description("Museum Studies")] SubjectMuseumStudies = 760,
            [Description("Accounting")] SubjectAccounting = 610,
            [Description("Business Administration & Management")] SubjectBusinessAdministrationAndManagement = 611,
            [Description("Business Information Systems")] SubjectBusinessInformationSystems = 612,
            [Description("eCommerce & Social Media")] SubjecteCommerceAndSocialMedia = 613,
            [Description("Economics")] SubjectEconomics = 614,
            [Description("Finance")] SubjectFinance = 615,
            [Description("Hospitality Management")] SubjectHospitalityManagement = 617,
            [Description("Human Resources Management")] SubjectHumanResourcesManagement = 618,
            [Description("International Business")] SubjectInternationalBusiness = 619,
            [Description("Marketing & Advertising")] SubjectMarketingAndAdvertising = 620,
            [Description("Office Management")] SubjectOfficeManagement = 621,
            [Description("Organizational Leadership")] SubjectOrganizationalLeadership = 622,
            [Description("Project Management")] SubjectProjectManagement = 624,
            [Description("Public Administration & Policy")] SubjectPublicAdministrationAndPolicy = 625,
            [Description("Real Estate & Property Management")] SubjectRealEstateAndPropertyManagement = 626,
            [Description("Entrepreneurship")] SubjectEntrepreneurship = 627,
            [Description("Sports Management")] SubjectSportsManagement = 628,
            [Description("Technology Management")] SubjectTechnologyManagement = 637,
            [Description("Healthcare Administration & Management")] SubjectHealthcareAdministrationAndManagement = 652,
            [Description("Environment & Agriculture")] SubjectEnvironmentAndAgriculture = 680,
            [Description("Non-Profit Administration")] SubjectNonToProfitAdministration = 761,
            [Description("Operations Management")] SubjectOperationsManagement = 770,
            [Description("Video Game Design")] SubjectVideoGameDesign2 = 608,
            [Description("Web Development & Design")] SubjectWebDevelopmentAndDesign2 = 609,
            [Description("Business Information Systems")] SubjectBusinessInformationSystems2 = 612,
            [Description("Computer Science")] SubjectComputerScience = 629,
            [Description("Database Management")] SubjectDatabaseManagement = 630,
            [Description("Information Assurance & Cybersecurity")] SubjectInformationAssuranceAndCybersecurity = 633,
            [Description("Information Technology")] SubjectInformationTechnology = 635,
            [Description("Software & Application Development")] SubjectSoftwareAndApplicationDevelopment = 636,
            [Description("Technology Management")] SubjectTechnologyManagement2 = 637,
            [Description("Telecom & Networking")] SubjectTelecomAndNetworking = 638,
            [Description("Computer Training & Support")] SubjectComputerTrainingAndSupport = 715,
            [Description("Geographic Information Systems")] SubjectGeographicInformationSystems = 755,
            [Description("Information Sciences")] SubjectInformationSciences = 758,
            [Description("Computational Science")] SubjectComputationalScience = 771,
            [Description("Economics")] SubjectEconomics2= 614,
            [Description("Communications & Public Relations")] SubjectCommunicationsAndPublicRelations = 691,
            [Description("English")] SubjectEnglish = 693,
            [Description("History")] SubjectHistory = 694,
            [Description("Liberal Arts & Sciences")] SubjectLiberalArtsAndSciences = 695,
            [Description("Languages")] SubjectLanguages = 696,
            [Description("Philosophy & Ethics")] SubjectPhilosophyAndEthics = 697,
            [Description("Women's Studies")] SubjectWomensStudies = 699 ,
            [Description("Literature & Writing")] SubjectLiteratureAndWriting = 700,
            [Description("Humanities")] SubjectHumanities = 717,
            [Description("Area, Ethnic & Cultural Studies")] SubjectAreaEthnicAndCulturalStudies = 747,
            [Description("Education, Technology & Online Learning")] SubjectEducationTechnologyAndOnlineLearning = 631,
            [Description("Adult Education")] SubjectAdultEducation = 640,
            [Description("Curriculum & Instruction")] SubjectCurriculumAndInstruction = 641,
            [Description("Early Childhood Education")] SubjectEarlyChildhoodEducation = 642,
            [Description("ESL/TESOL")] SubjectESLTESOL = 643,
            [Description("General Education")] SubjectGeneralEducation = 644,
            [Description("Higher Education")] SubjectHigherEducation = 645,
            [Description("International Education")] SubjectInternationalEducation = 646,
            [Description("K-12 Education")] SubjectKTo12Education = 647,
            [Description("Education Leadership & Administration")] SubjectEducationLeadershipAndAdministration = 648,
            [Description("Special & Gifted Education")] SubjectSpecialAndGiftedEducation = 649,
            [Description("Educational & School Psychology")] SubjectEducationalAndSchoolPsychology = 745,
            [Description("Environmental Education")] SubjectEnvironmentalEducation = 753,
            [Description("Teacher Education")] SubjectTeacherEducation = 763,
            [Description("Health Informatics")] SubjectHealthInformatics = 651,
            [Description("Healthcare Administration & Management")] SubjectHealthcareAdministrationAndManagement2 = 652,
            [Description("Human Services")] SubjectHumanServices = 654,
            [Description("Medical Assisting")] SubjectMedicalAssisting = 655,
            [Description("Medical Billing, Coding & Transcription")] SubjectMedicalBillingCodingAndTranscription = 656,
            [Description("Medical Specialties")] SubjectMedicalSpecialties = 657,
            [Description("Nursing")] SubjectNursing = 658,
            [Description("Nutrition & Fitness")] SubjectNutritionAndFitness = 659,
            [Description("Pharmacology")] SubjectPharmacology = 661,
            [Description("Physical & Occupational Therapy")] SubjectPhysicalAndOccupationalTherapy = 662,
            [Description("Public Health")] SubjectPublicHealth = 664,
            [Description("Radiology & Imaging Sciences")] SubjectRadiologyAndImagingSciences = 665,
            [Description("Gerontology")] SubjectGerontology = 668,
            [Description("Health Sciences")] SubjectHealthSciences = 718,
            [Description("Medical Diagnostic")] SubjectMedicalDiagnostic = 720,
            [Description("Paramedic & EMT")] SubjectParamedicAndEMT = 735,
            [Description("Clinical Laboratory Science")] SubjectClinicalLaboratoryScience = 738,
            [Description("Biomedical Science")] SubjectBiomedicalScience = 749,
            [Description("Communication Science & Disorders")] SubjectCommunicationScienceAndDisorders = 750,
            [Description("Architecture")] SubjectArchitecture = 681,
            [Description("Biology")] SubjectBiology = 683,
            [Description("Physical Sciences")] SubjectPhysicalSciences = 684,
            [Description("Engineering")] SubjectEngineering = 685,
            [Description("Environmental Science")] SubjectEnvironmentalScience = 686,
            [Description("Mathematics & Statistics")] SubjectMathematicsAndStatistics = 688,
            [Description("Aerospace Engineering")] SubjectAerospaceEngineering = 690,
            [Description("Biological & Biomedical Engineering")] SubjectBiologicalAndBiomedicalEngineering = 748,
            [Description("Biomedical Science")] SubjectBiomedicalScience2 = 749,
            [Description("Electrical Engineering")] SubjectElectricalEngineering = 752,
            [Description("Environmental Engineering")] SubjectEnvironmentalEngineering = 754,
            [Description("Industrial & Mechanical Engineering")] SubjectIndustrialAndMechanicalEngineering = 756,
            [Description("Physiology")] SubjectPhysiology = 762,
            [Description("Veterinary & Animal Sciences")] SubjectVeterinaryAndAnimalSciences = 765,
            [Description("Chemical Engineering")] SubjectChemicalEngineering = 766,
            [Description("Civil Engineering")] SubjectCivilEngineering = 767,
            [Description("Engineering Management")] SubjectEngineeringManagement = 768,
            [Description("Neuroscience")] SubjectNeuroscience = 769,
            [Description("Economics")] SubjectEconomics3 = 614,
            [Description("Public Administration & Policy")] SubjectPublicAdministrationAndPolicy2 = 625,
            [Description("Human Services")] SubjectHumanServices2 = 654,
            [Description("Public Health")] SubjectPublicHealth2 = 664,
            [Description("Anthropology")] SubjectAnthropology2 = 667,
            [Description("Gerontology")] SubjectGerontology2 = 668,
            [Description("Political Science")] SubjectPoliticalScience = 669,
            [Description("Psychology")] SubjectPsychology = 670,
            [Description("Social Work")] SubjectSocialWork = 671,
            [Description("Sociology")] SubjectSociology = 672,
            [Description("Emergency Management")] SubjectEmergencyManagement = 674,
            [Description("Environmental Management")] SubjectEnvironmentalManagement = 722,
            [Description("Counseling Psychology")] SubjectCounselingPsychology = 739,
            [Description("Educational & School Psychology")] SubjectEducationalAndSchoolPsychology2 = 745,
            [Description("Archaeology")] SubjectArchaeology = 746,
            [Description("Conflict & Peace Studies")] SubjectConflictAndPeaceStudies = 751,
            [Description("Non-Profit Administration")] SubjectNonToProfitAdministration2 = 761,
            [Description("Urban Affairs & Planning")] SubjectUrbanAffairsAndPlanning = 764,
            [Description("Neuroscience")] SubjectNeuroscience2 = 769,
            [Description("International Relations")] SubjectInternationalRelations = 772,
            [Description("Automotive & Mechanics")] SubjectAutomotiveAndMechanics = 702,
            [Description("Beauty, Cosmetology & Esthetics")] SubjectBeautyCosmetologyAndEsthetics = 703,
            [Description("Carpentry & Construction")] SubjectCarpentryAndConstruction = 704,
            [Description("Commercial Transportation")] SubjectCommercialTransportation = 706,
            [Description("Culinary & Catering")] SubjectCulinaryAndCatering = 707,
            [Description("Personal Services")] SubjectPersonalServices = 710,
            [Description("Plumbing")] SubjectPlumbing = 711,
            [Description("Skilled & Production  Trades")] SubjectSkilledAndProductionTrades = 714,
            [Description("High School Diploma – Accredited")] SubjectHighSchoolDiplomaAccredited = 729,
            [Description("Massage Therapy")] SubjectMassageTherapy = 730,
            [Description("HVAC Technologies")] SubjectHVACTechnologies = 733,
            [Description("Electrical Technology")] SubjectElectricalTechnology = 740,
            [Description("Truck, Tractor and Bus Licenses")] SubjectTruckTractorandBusLicenses = 774,
            [Description("Aviation Maintenance")] SubjectAviationMaintenance = 775,
            [Description("Criminal Justice & Criminalistics")] SubjectCriminalJusticeAndCriminalistics = 673,
            [Description("Forensic Science")] SubjectForensicScience = 675,
            [Description("Homeland Security & National Defense")] SubjectHomelandSecurityAndNationalDefense = 676,
            [Description("Law Enforcement, Policing & Investigation")] SubjectLawEnforcementPolicingAndInvestigation = 677,
            [Description("Legal Studies")] SubjectLegalStudies = 678,
            [Description("Paralegal")] SubjectParalegal = 679,
            [Description("Biblical Studies")] SubjectBiblicalStudies = 698,
            [Description("Christian Counseling")] SubjectChristianCounseling = 736,
            [Description("Religious Studies")] SubjectReligiousStudies = 737,
            [Description("Ministry")] SubjectMinistry = 741,
            [Description("Theology")] SubjectTheology = 743,
        }















        //Not present in DB views
        public enum MilitaryAffiliation
        {
            [Description("Yes")]
            Yes = 1,

            [Description("No")]
            No = 2,
        }

        public enum TeachingCertificate
        {
            [Description("Yes")]
            Yes = 1,

            [Description("No")]
            No = 2,
        }

        public enum RegisteredNurse
        {
            [Description("Yes")]
            Yes = 1,

            [Description("No")]
            No = 2,

            [Description("Currently Obtaining")]
            CurrentlyObtaining = 3,
        }

        public enum USCitizen
        {
            [Description("Yes")]
            Yes = 1,

            [Description("No")]
            No = 2,
        }

        public enum ConnectionSource
        {
            [Description("University")]
            University = 1,


        }

        public enum State
        {
            [Description("AL - Alabama")] StateAL = 1,
            [Description("AK - Alaska")] StateAK = 2,
            [Description("AB - Alberta")] StateAB = 3,
            [Description("AS - American Samoa")] StateAS = 4,
            [Description("AZ - Arizona")] StateAZ = 5,
            [Description("AR - Arkansas")] StateAR = 6,
            [Description("AE - Armed Forces")] StateAE = 7,
            [Description("AA - Armed Forces - Americas")] StateAA = 8,
            [Description("AP - Armed Forces - Pacific")] StateAP = 9,
            [Description("BC - British Columbia")] StateBC = 10,
            [Description("CA - California")] StateCA = 11,
            [Description("CO - Colorado")] StateCO = 12,
            [Description("CT - Connecticut")] StateCT = 13,
            [Description("DE - Delaware")] StateDE = 14,
            [Description("DC - District of Columbia")] StateDC = 15,
            [Description("FM - Federated States of Micronesia")] StateFM = 16,
            [Description("FL - Florida")] StateFL = 17,
            [Description("GA - Georgia")] StateGA = 18,
            [Description("GU - Guam")] StateGU = 19,
            [Description("HI - Hawaii")] StateHI = 20,
            [Description("ID - Idaho")] StateID = 21,
            [Description("IL - Illinois")] StateIL = 22,
            [Description("IN - Indiana")] StateIN = 23,
            [Description("IA - Iowa")] StateIA = 24,
            [Description("KS - Kansas")] StateKS = 25,
            [Description("KY - Kentucky")] StateKY = 26,
            [Description("LA - Louisiana")] StateLA = 27,
            [Description("ME - Maine")] StateME = 28,
            [Description("MB - Manitoba")] StateMB = 29,
            [Description("MH - Marshall Islands")] StateMH = 30,
            [Description("MD - Maryland")] StateMD = 31,
            [Description("MA - Massachusetts")] StateMA = 32,
            [Description("MI - Michigan")] StateMI = 33,
            [Description("4794 - Micronesia, Federated States Of")] State4794 = 34,
            [Description("MN - Minnesota")] StateMN = 35,
            [Description("MS - Mississippi")] StateMS = 36,
            [Description("MO - Missouri")] StateMO = 37,
            [Description("MT - Montana")] StateMT = 38,
            [Description("NE - Nebraska")] StateNE = 39,
            [Description("NV - Nevada")] StateNV = 40,
            [Description("NB - New Brunswick")] StateNB = 41,
            [Description("NH - New Hampshire")] StateNH = 42,
            [Description("NJ - New Jersey")] StateNJ = 43,
            [Description("NM - New Mexico")] StateNM = 44,
            [Description("NY - New York")] StateNY = 45,
            [Description("NF - Newfoundland And Labrador")] StateNF = 46,
            [Description("NC - North Carolina")] StateNC = 47,
            [Description("ND - North Dakota")] StateND = 48,
            [Description("MP - Northern Mariana Islands")] StateMP = 49,
            [Description("NT - Northwest Territories")] StateNT = 50,
            [Description("NS - Nova Scotia")] StateNS = 51,
            [Description("NU - Nunavut")] StateNU = 52,
            [Description("OH - Ohio")] StateOH = 53,
            [Description("OK - Oklahoma")] StateOK = 54,
            [Description("ON - Ontario")] StateON = 55,
            [Description("OR - Oregon")] StateOR = 56,
            [Description("PW - Palau")] StatePW = 57,
            [Description("PA - Pennsylvania")] StatePA = 58,
            [Description("PE - Prince Edward Island")] StatePE = 59,
            [Description("PR - Puerto Rico")] StatePR = 60,
            [Description("QC - Quebec")] StateQC = 61,
            [Description("RI - Rhode Island")] StateRI = 62,
            [Description("SK - Saskatchewan")] StateSK = 63,
            [Description("SC - South Carolina")] StateSC = 64,
            [Description("SD - South Dakota")] StateSD = 65,
            [Description("TN - Tennessee")] StateTN = 66,
            [Description("TX - Texas")] StateTX = 67,
            [Description("UT - Utah")] StateUT = 68,
            [Description("VT - Vermont")] StateVT = 69,
            [Description("VI - Virgin Islands")] StateVI = 70,
            [Description("4795 - Virgin Islands, U.S.")] State4795 = 71,
            [Description("VA - Virginia")] StateVA = 72,
            [Description("WA - Washington")] StateWA = 73,
            [Description("WV - West Virginia")] StateWV = 74,
            [Description("WI - Wisconsin")] StateWI = 75,
            [Description("WY - Wyoming")] StateWY = 76,
            [Description("3842 - Yukon")] State3842 = 77,
            [Description("YT - Yukon Territory")] StateYT = 78,
        }
    }
}
