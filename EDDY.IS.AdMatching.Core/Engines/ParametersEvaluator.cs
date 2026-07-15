using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Entities;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace EDDY.IS.AdMatching.Core.Engines
{
    public class ParametersEvaluator
    {
        public ParametersEvaluator()
        {
        }

        public List<AdsMatched> Process(List<AdsMatched> FinalAds, Dictionary<string, string> Parameters,
            FilteredContainerDictionary Filtered)
        {
            Dictionary<string, string> FinalParameters = this.PrepareFinalParameters(Parameters);

            FinalAds = SetParametersForFinalList(FinalAds, FinalParameters, Filtered);


            return FinalAds;
        }

        private List<AdsMatched> SetParametersForFinalList(List<AdsMatched> FinalAds,
            Dictionary<string, string> FinalParameters, FilteredContainerDictionary Filtered)
        {
            //campaign|campaign_id|ad_group|ad_group_id
            // campign , 3

            for (int i = 0; i < FinalAds.Count; i++)
            {
                var ad = FinalAds[i];
                Dictionary<string, string> DefaultParameters =
                    createDefaultParameters(ad, Filtered, FinalParameters);
                DefaultParameters = createDynamicsParameter(ad, Filtered, DefaultParameters);

                //Click URL
                var defaultParametersPattern = string.Join("|",
                    DefaultParameters.Keys
                        .Select(k => k.Replace("|", "#").ToString()).ToArray());
                var finalParametersPattern = string.Join("|", FinalParameters.Keys
                    .Select(k => k.Replace("|", "#").ToString()).ToArray());
                if (!string.IsNullOrEmpty(ad.AdClickUrl))
                {
                    ad.AdClickUrl = Regex.Replace(ad.AdClickUrl, defaultParametersPattern, m => HttpUtility.UrlEncode(DefaultParameters[m.Value]));

                    if (!string.IsNullOrEmpty(finalParametersPattern))
                        ad.AdClickUrl = Regex.Replace(ad.AdClickUrl, finalParametersPattern, m => HttpUtility.UrlEncode(FinalParameters[m.Value]));

                    ad.AdClickUrl = ReplaceCalculatedValues(ad.AdClickUrl, FinalParameters);

                    ad.AdClickUrl = ad.AdClickUrl
                        .Replace("{clkToken}", "#clkToken#", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("{", "")
                        .Replace("}", "")
                        .Replace("#clkToken#", "{clkToken}", StringComparison.InvariantCultureIgnoreCase);
                }

                //Display URL
                if (!string.IsNullOrEmpty(ad.AdDisplayUrl))
                {
                    ad.AdDisplayUrl = Regex.Replace(ad.AdDisplayUrl, defaultParametersPattern, m => DefaultParameters[m.Value]);

                    if (!string.IsNullOrEmpty(finalParametersPattern))
                        ad.AdDisplayUrl = Regex.Replace(ad.AdDisplayUrl, finalParametersPattern, m => FinalParameters[m.Value]);

                    ad.AdDisplayUrl = ReplaceCalculatedValues(ad.AdDisplayUrl, FinalParameters);

                    ad.AdDisplayUrl = ad.AdDisplayUrl
                        .Replace("{clkToken}", "#clkToken#", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("{", "")
                        .Replace("}", "")
                        .Replace("#clkToken#", "{clkToken}", StringComparison.InvariantCultureIgnoreCase);
                }

                //Name
                if (!string.IsNullOrEmpty(ad.AdName))
                {
                    ad.AdName = Regex.Replace(ad.AdName, defaultParametersPattern, m => DefaultParameters[m.Value]);

                    if (!string.IsNullOrEmpty(finalParametersPattern))
                        ad.AdName = Regex.Replace(ad.AdName, finalParametersPattern, m => FinalParameters[m.Value]);

                    ad.AdName = ReplaceCalculatedValues(ad.AdName, FinalParameters);

                    ad.AdName = ad.AdName
                        .Replace("{clkToken}", "#clkToken#", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("{", "")
                        .Replace("}", "")
                        .Replace("#clkToken#", "{clkToken}", StringComparison.InvariantCultureIgnoreCase);
                }


                //Description
                if (!string.IsNullOrEmpty(ad.AdDescription))
                {
                    ad.AdDescription = Regex.Replace(ad.AdDescription, defaultParametersPattern, m => DefaultParameters[m.Value]);

                    if (!string.IsNullOrEmpty(finalParametersPattern))
                        ad.AdDescription = Regex.Replace(ad.AdDescription, finalParametersPattern, m => FinalParameters[m.Value]);

                    ad.AdDescription = ReplaceCalculatedValues(ad.AdDescription, FinalParameters);

                    ad.AdDescription = ad.AdDescription
                        .Replace("{clkToken}", "#clkToken#", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("{", "")
                        .Replace("}", "")
                        .Replace("#clkToken#", "{clkToken}", StringComparison.InvariantCultureIgnoreCase);
                }

                //Popular Programs
                if (!string.IsNullOrEmpty(ad.PopularPrograms))
                {
                    ad.PopularPrograms = Regex.Replace(ad.PopularPrograms, defaultParametersPattern, m => DefaultParameters[m.Value]);

                    if (!string.IsNullOrEmpty(finalParametersPattern))
                        ad.PopularPrograms = Regex.Replace(ad.PopularPrograms, finalParametersPattern, m => FinalParameters[m.Value]);

                    ad.PopularPrograms = ReplaceCalculatedValues(ad.PopularPrograms, FinalParameters);

                    ad.PopularPrograms = ad.PopularPrograms
                        .Replace("{clkToken}", "#clkToken#", StringComparison.InvariantCultureIgnoreCase)
                        .Replace("{", "")
                        .Replace("}", "")
                        .Replace("#clkToken#", "{clkToken}", StringComparison.InvariantCultureIgnoreCase);
                }
            }


            return FinalAds;
        }

        private Random _random = new Random();

        private string? ReplaceCalculatedValues(string? stringWithPlaceholders,
            Dictionary<string, string> finalParameters)
        {
            var res = stringWithPlaceholders;
            /*
                Adserver	{area_code}	derive from phone number
                Adserver	{city_state}	derived from city and state
                Adserver	{mobile_yes} {mobile_no}	derived from device
                Adserver	{phone|dash_separated}	derived from phone
                Adserver	{random}	 
                Adserver	{rank}
             */
            if (!string.IsNullOrEmpty(stringWithPlaceholders))
            {
                res = res.Replace("{random}", _random.Next().ToString(),
                    StringComparison.InvariantCultureIgnoreCase);
                res = res.Replace("{area_code}", GetAreaCodeFromPhoneNumber(finalParameters),
                    StringComparison.InvariantCultureIgnoreCase);
                res = res.Replace("{city_state}", GetCityState(finalParameters),
                    StringComparison.InvariantCultureIgnoreCase);
                res = res.Replace("{phone|dash_separated}", GetDashSeparatedPhoneNumber(finalParameters),
                    StringComparison.InvariantCultureIgnoreCase);
                res = res.Replace("{mobile}", IsMobileFinalParameters(finalParameters).ToString(),
                    StringComparison.InvariantCultureIgnoreCase);
                res = res.Replace("{ref}", HttpUtility.UrlEncode(finalParameters.GetValueOrDefault("{SiteUrl}", "")),
                    StringComparison.InvariantCultureIgnoreCase);
                res = res.Replace("{sub_1}", HttpUtility.UrlEncode(finalParameters.GetValueOrDefault("{Sub1}", "")),
                    StringComparison.InvariantCultureIgnoreCase);
                res = res.Replace("{sub_2}", HttpUtility.UrlEncode(finalParameters.GetValueOrDefault("{Sub2}", "")),
                    StringComparison.InvariantCultureIgnoreCase);
                res = res.Replace("{sub_3}", HttpUtility.UrlEncode(finalParameters.GetValueOrDefault("{Sub3}", "")),
                    StringComparison.InvariantCultureIgnoreCase);
            }

            return res;
        }

        private string? GetCityState(Dictionary<string, string> finalParameters)
        {
            var cityValue = GetCityValueFromFinalParameters(finalParameters);
            var stateValue = GetStateValueFromFinalParameters(finalParameters);

            if (!string.IsNullOrEmpty(cityValue) && !string.IsNullOrEmpty(stateValue))
            {
                return $"{cityValue}_{stateValue}";
            }

            return null;
        }

        private static string GetStateValueFromFinalParameters(Dictionary<string, string> finalParameters)
        {
            var stateKeys = finalParameters.Keys.Where(x =>
                x.Equals("state", StringComparison.InvariantCultureIgnoreCase)
                || x.Equals("{state}",
                    StringComparison.InvariantCultureIgnoreCase));
            string stateValue = null;
            if (stateKeys.Any())
            {
                stateValue = finalParameters[stateKeys.First()];
            }

            return stateValue;
        }

        private static string GetCityValueFromFinalParameters(Dictionary<string, string> finalParameters)
        {
            var cityKeys = finalParameters.Keys.Where(x => x.Equals("city", StringComparison.InvariantCultureIgnoreCase)
                                                           || x.Equals("{city}",
                                                               StringComparison.InvariantCultureIgnoreCase));
            string cityValue = null;
            if (cityKeys.Any())
            {
                cityValue = finalParameters[cityKeys.First()];
            }

            return cityValue;
        }

        private static string IsMobileFinalParameters(Dictionary<string, string> finalParameters)
        {
            var deviceKeys = finalParameters.Keys.Where(x =>
                x.Equals("device", StringComparison.InvariantCultureIgnoreCase)
                || x.Equals("{device}", StringComparison.InvariantCultureIgnoreCase)
                || x.Equals("device_type", StringComparison.InvariantCultureIgnoreCase)
                || x.Equals("devicetype", StringComparison.InvariantCultureIgnoreCase)
                || x.Equals("{device_type}", StringComparison.InvariantCultureIgnoreCase)
                || x.Equals("{devicetype}", StringComparison.InvariantCultureIgnoreCase)
                );
            string deviceValue = null;
            if (deviceKeys.Any())
            {
                deviceValue = finalParameters[deviceKeys.First()].ToLower();
                if (deviceValue.Equals("mobile", StringComparison.InvariantCultureIgnoreCase)
                 || deviceValue.Equals("tablet", StringComparison.InvariantCultureIgnoreCase))
                {
                    return "Yes";
                }
            }

            return "No";
        }

        private string? GetDashSeparatedPhoneNumber(Dictionary<string, string> finalParameters)
        {
            var phone = GetPhoneFromDictionary(finalParameters);
            if (!string.IsNullOrWhiteSpace(phone) && phone.Length == 10)
            {
                return $"{phone.Substring(0, 3)}-{phone.Substring(3, 3)}-{phone.Substring(6, 4)}";
            }

            if (!string.IsNullOrWhiteSpace(phone) && phone.Length == 7)
            {
                return $"{phone.Substring(0, 3)}-{phone.Substring(3, 4)}";
            }

            return string.Empty;
        }

        private string? GetAreaCodeFromPhoneNumber(Dictionary<string, string> finalParameters)
        {
            var phone = GetPhoneFromDictionary(finalParameters);
            if (!string.IsNullOrWhiteSpace(phone) && phone.Length == 10)
            {
                return phone.Substring(0, 3);
            }

            return string.Empty;
        }

        private static string GetPhoneFromDictionary(Dictionary<string, string> finalParameters)
        {
            var phoneKeys = finalParameters.Keys.Where(x =>
                x.Equals("phone", StringComparison.InvariantCultureIgnoreCase) ||
                x.Equals("{phone}", StringComparison.InvariantCultureIgnoreCase)
                );
            if (phoneKeys.Any())
            {
                var phoneValue = finalParameters[phoneKeys.First()];
                return phoneValue;
            }

            return null;
        }

        public Dictionary<string, string> PrepareFinalParameters(Dictionary<string, string> Parameters)
        {
            Dictionary<string, string> FinalParameters = new Dictionary<string, string>();

            foreach (var parameter in Parameters)
            {
                FinalParameters.Add("{" + parameter.Key + "}", parameter.Value);
            }

            ;

            return FinalParameters;
        }

        public Dictionary<string, string> createDynamicsParameter(AdsMatched Ad, FilteredContainerDictionary Filtered,
            Dictionary<string, string> DefaultParameters)
        {
            List<ClientAdAccountParameter> clientAdAccountParameters = new();

            clientAdAccountParameters = Filtered.ClientAdAccountParameterList.Select(x => x.Value).Where(x =>
                x.ClientAdAccountId == Ad.ClientAdAccountId && !x.IsDeleted && (bool)x.IsEnabled).ToList();

            foreach (ClientAdAccountParameter parameter in clientAdAccountParameters)
            {
                if (parameter.IsRollingDates != null && (bool)parameter.IsRollingDates)
                {
                    string[] dates = parameter.ParameterValue.Split(',');
                    int minDifference = 0;
                    string finalDate = string.Empty;
                    bool isFirstEvaluated = false;

                    for (int i = 0; i < dates.Length; i++)
                    {
                        DateTime aDate = Convert.ToDateTime(dates[i]);

                        if (aDate >= DateTime.Now.Date)
                        {
                            int daysBetween = (aDate - DateTime.Now.Date).Days;

                            minDifference = !isFirstEvaluated ? daysBetween : minDifference;

                            if (daysBetween <= minDifference)
                            {
                                finalDate = dates[i];
                                isFirstEvaluated = true;
                            }
                        }
                    }

                    DefaultParameters.Add(parameter.ParameterName, finalDate != string.Empty ? finalDate : "soon");
                }
                else if (parameter.ParameterValue.Contains('{'))
                {
                    try
                    {
                        string dynamicString = "return new string($\"" + parameter.ParameterValue + "\");";
                        var options = ScriptOptions.Default.AddReferences("System").AddImports("System");
                        var result = CSharpScript.EvaluateAsync<string>(dynamicString, options).Result;
                        DefaultParameters.Add(parameter.ParameterName, result);
                    }
                    catch (Exception ex)
                    {
                        //NLogLogger.Trace("Error trying to compile dynamicParameter + " + parameter.ParameterValue, ex);
                        DefaultParameters.Add(parameter.ParameterName, "");
                    }
                }
                else
                {
                    if (!DefaultParameters.ContainsKey(parameter.ParameterName))
                        DefaultParameters.Add(parameter.ParameterName, parameter.ParameterValue);
                }
            }

            ;

            return DefaultParameters;
        }

        public Dictionary<string, string> createDefaultParameters(AdsMatched Ad, FilteredContainerDictionary Filtered,
            Dictionary<string, string> RequestParameters)
        {
            Dictionary<string, string> DefaultParameters = new Dictionary<string, string>();

            DefaultParameters.Add("{campaign}", Ad.CampaignName);
            DefaultParameters.Add("{campaign_id}", Ad.CampaignId.ToString());
            DefaultParameters.Add("{ad_group}", Ad.AdGroupName);
            DefaultParameters.Add("{ad_group_id}", Ad.AdGroupId.ToString());
            DefaultParameters.Add("{ad}", Ad.AdName);
            DefaultParameters.Add("{ad_id}", Ad.AdId.ToString());

            //Other Default Parameters
            List<String> ParametersList =
                Filtered.ClientAdAccountDefaultParameterList.Select(x => x.Value.Macro).ToList();

            foreach (String parameter in ParametersList)
            {
                String parameterNameToSearch =
                    SubstituteParameterNameValuesBetweenDbAndParameterRequest(parameter.Replace("{", "")
                        .Replace("}", ""));
                var matchingValues = RequestParameters.Where(x =>
                    x.Key.Trim('{').Trim('}').Equals(parameterNameToSearch, StringComparison.OrdinalIgnoreCase));

                var newValue = "";
                if (matchingValues != null && matchingValues.Any())
                {
                    newValue = matchingValues.First().Value;
                }

                if (!DefaultParameters.ContainsKey(parameter) && !parameter.Contains("clkToken"))
                {
                    DefaultParameters.Add(parameter, newValue);
                }
            }

            return DefaultParameters;
        }

        private string SubstituteParameterNameValuesBetweenDbAndParameterRequest(string parameterNameFromDb)
        {
            if (parameterNameFromDb.Equals("Device", StringComparison.CurrentCultureIgnoreCase))
                return "DeviceType";
            if (parameterNameFromDb.Equals("country_code", StringComparison.CurrentCultureIgnoreCase))
                return "CountryCode";
            if (parameterNameFromDb.Equals("channel_id", StringComparison.CurrentCultureIgnoreCase))
                return "ChannelId";
            if (parameterNameFromDb.Equals("degree_level", StringComparison.CurrentCultureIgnoreCase))
                return "DegreeLevel";
            if (parameterNameFromDb.Equals("learning_preference", StringComparison.CurrentCultureIgnoreCase))
                return "LearningPreference";
            if (parameterNameFromDb.Equals("sub_1", StringComparison.CurrentCultureIgnoreCase))
                return "Sub1";
            if (parameterNameFromDb.Equals("sub_2", StringComparison.CurrentCultureIgnoreCase))
                return "Sub2";
            if (parameterNameFromDb.Equals("sub_3", StringComparison.CurrentCultureIgnoreCase))
                return "Sub3";
            if (parameterNameFromDb.Equals("source_id", StringComparison.CurrentCultureIgnoreCase))
                return "SourceId";
            return parameterNameFromDb;
        }
    }
}