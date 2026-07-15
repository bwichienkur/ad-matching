using System;
using System.Collections.Generic;
using System.Linq;
using EDDY.IS.AdMatching.Core.Auxiliary;
using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Domain.BusinessEntities;
using EDDY.IS.AdMatching.Domain.Services.Interfaces;
using EDDY.IS.AdMatching.Entities;

namespace EDDY.IS.AdMatching.Core.Engines
{
    internal class StopsEvaluator
    {
        private readonly DebugLogger _debugLogger;
        private IDataManager _dataManager;
        public StopsEvaluator(DebugLogger debugLogger)
        {
            _debugLogger = debugLogger;
        }

        public FilteredDictionary Process(FilteredContainerDictionary Filtered, FilteredDictionary MainDictionaryEvaluated, Dictionary<string, string> parameters)
        {
            //Acounts Evaluations
            if (Filtered.ClientAdAccounts != null)
                foreach (var clientAdAccount in Filtered.ClientAdAccounts)
                {
                    //Stops Evaluator
                    if (!EvaluateClientAdAccount(clientAdAccount.Value, Filtered))
                    {
                        MainDictionaryEvaluated.ClientAdAccounts.Add(clientAdAccount.Key, clientAdAccount.Value);

                        List<Campaign> validCampaigns = new List<Campaign>();
                        validCampaigns = Filtered.CampaignsList
                            .Select(x => x.Value)
                            .Where(campaign => campaign.ClientAdAccountId == clientAdAccount.Value.ClientAdAccountId)
                            .ToList();


                        //Adding the valids campaings to the main dictionary
                        foreach (Campaign campaign in validCampaigns)
                        {
                            if (!MainDictionaryEvaluated.CampaignsList.ContainsKey(campaign.CampaignId))
                            {
                                _debugLogger.Log($"***StopsEvaluator.Process: adding ItemCampaign.Key:", campaign.CampaignId);
                                MainDictionaryEvaluated.CampaignsList.Add(campaign.CampaignId, campaign);
                            }
                        }
                    }
                }

            if (MainDictionaryEvaluated.CampaignsList != null)
                foreach (var ItemCampaign in MainDictionaryEvaluated.CampaignsList)
                {
                    List<CampaignStop> CampaignStops = getCampaingStopToEvaluate(ItemCampaign.Value, Filtered);
                    //Stops Evaluator at Campaign Level
                    if (EvaluateCampaign(CampaignStops, ItemCampaign.Value.StopsTimeZoneId.GetValueOrDefault(7), Filtered, parameters))
                    {
                        _debugLogger.Log($"***StopsEvaluator.Process: removing ItemCampaign.Key:", ItemCampaign.Key);
                        MainDictionaryEvaluated.CampaignsList.Remove(ItemCampaign.Key);
                    }
                }

            return MainDictionaryEvaluated;

        }



        public Boolean EvaluateClientAdAccount(ClientAdAccount clientAdAccount, FilteredContainerDictionary Filtered)
        {
            Boolean EvaluationResult = false;
            CommonTimeZoneManager TimeZoneAuxiliary = new CommonTimeZoneManager();
            DateTime UtcNow = DateTime.Now;
            if (clientAdAccount!=null)
            {
                //getting Stops  by ClientAdAccount c
                var ClientAdAccountStops = from clientAccountStop in Filtered.ClientAccountStopFiltered
                                            where clientAccountStop.ClientAdAccountId == clientAdAccount.ClientAdAccountId 
                                            && clientAccountStop.IsEnabled== true && clientAccountStop.IsDeleted==false
                                            select clientAccountStop;

                foreach (var ClientAdAccountStopItem in ClientAdAccountStops)
                {
                    UtcNow = TimeZoneInfo.ConvertTimeToUtc(UtcNow);
                    DateTime BeginStopUtc = TimeZoneInfo.ConvertTimeToUtc(ClientAdAccountStopItem.BeginStop);  //TimeZoneAuxiliary.ConvertTimeZoneToUtc(/*CampaignStopItem.TimeZone*/"Eastern Standard Time", CampaignStopItem.BeginStop);
                    DateTime EndStopUtc = TimeZoneInfo.ConvertTimeToUtc(ClientAdAccountStopItem.EndStop); //TimeZoneAuxiliary.ConvertTimeZoneToUtc("Eastern Standard Time", CampaignStopItem.EndStop);

                    if (UtcNow>=BeginStopUtc && UtcNow<=EndStopUtc)
                    {
                        EvaluationResult=true;
                    }
                }
            }

            return EvaluationResult;
        }

        public Boolean EvaluateCampaign(List<CampaignStop> CampaingStops, int campaignTimeZoneId,
            FilteredContainerDictionary Filtered,
            Dictionary<string, string> parameters)
        {
            Boolean EvaluationResult = false;
            DateTime UtcNow = DateTime.Now;

            if (CampaingStops != null)
            {
                foreach (var CampaignStopItem in CampaingStops)
                {
                    if (Filtered == null || Filtered.TimeZoneList == null || Filtered.ScheduleOptionList == null) continue;
                    var campaignTimeZone = Filtered.TimeZoneList.GetValueOrDefault(campaignTimeZoneId);
                    if (campaignTimeZone == null) continue;

                    TimeZoneInfo? timeZoneInfo = null;
                    if (campaignTimeZoneId == (int)TimeZoneId.Local)
                    {
                        if (parameters.TryGetValue("StateCode", out string? stateCode))
                        {
                            if (stateCode != null && Filtered.StateTimeZoneList.TryGetValue(stateCode, out StateTimeZone? stateTimeZone))
                                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(stateTimeZone.TimeZoneCode);
                        }

                        // Default timezone if user local timezone can't be derived
                        if (timeZoneInfo == null)
                            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");

                    }
                    else
                        timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(campaignTimeZone.Code);

                    UtcNow = TimeZoneInfo.ConvertTimeToUtc(UtcNow);
                    DateTime BeginStopUtc = TimeZoneInfo.ConvertTimeToUtc(CampaignStopItem.BeginStop, timeZoneInfo);
                    DateTime EndStopUtc = TimeZoneInfo.ConvertTimeToUtc(CampaignStopItem.EndStop, timeZoneInfo);

                    if (UtcNow >= BeginStopUtc && UtcNow <= EndStopUtc)
                        EvaluationResult = true;

                }
            }
            return EvaluationResult;
        }

        private List<CampaignStop> getCampaingStopToEvaluate(Campaign campaign, FilteredContainerDictionary Filtered)
        {
            List<CampaignStop> campaignStop = new List<CampaignStop>();

            //var fatherCampaing = Filtered.CampaignRelationshipList.FirstOrDefault(x => x.Value.CampaignId == campaign.CampaignId && x.Value.Lin);
            int fatherCampaingId = 0;//fatherCampaing.Value != null ? fatherCampaing.Value.ParentCampaignId : 0;

            if (fatherCampaingId>0)
            {
                //campaignStop = (List<CampaignStop>)Filtered.CampaignStopList.Select(x => x.Value.CampaignId == fatherCampaingId);
            }
            else
            {
                if (Filtered.CampaignStopList != null)
                    campaignStop = Filtered.CampaignStopList
                        .Select(x => x.Value)
                        .Where(x => x.CampaignId == campaign.CampaignId && x.IsEnabled && !x.IsDeleted)
                        .ToList();
            }

            return campaignStop;

        }
    }
}
