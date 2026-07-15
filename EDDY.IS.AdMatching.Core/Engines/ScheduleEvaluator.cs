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
    internal class ScheduleEvaluator
    {
        private readonly DebugLogger _debugLogger;

        public ScheduleEvaluator(DebugLogger debugLogger)
        {
            _debugLogger = debugLogger;
        }

        public FilteredDictionary Process(FilteredContainerDictionary Filtered, FilteredDictionary MainDictionaryEvaluated, Dictionary<string,string> parameters)
        {
            foreach (var ItemCampaign in MainDictionaryEvaluated.CampaignsList)
            {
                int campaignSchedulerId = NewEvaluateCampaignScheduler(Filtered.CampaignSchedulerList
                        .Where(x=>x.Value.CampaignId == ItemCampaign.Value.CampaignId && x.Value.IsEnabled && !x.Value.IsDeleted)
                        .Select(x=>x.Value),
                    Filtered, ItemCampaign.Value.TimeZoneId == null ? 7 : (int)ItemCampaign.Value.TimeZoneId, parameters);

                if (campaignSchedulerId > 0)
                {
                    CampaignSchedule campaignSchedule =
                        Filtered.CampaignSchedulerList.FirstOrDefault(x =>
                            x.Value.CampaignScheduleId == campaignSchedulerId).Value;

                    if (campaignSchedule.DisableAds == 1) {
                        _debugLogger.Log($"***ScheduleEvaluator.Process: removing ItemCampaign.Key:", ItemCampaign.Key);
                        MainDictionaryEvaluated.CampaignsList.Remove(ItemCampaign.Key);
                    } else {
                        if (!MainDictionaryEvaluated.CampaignSchedulerList.ContainsKey(campaignSchedulerId))
                        {
                            _debugLogger.Log($"ScheduleEvaluator.Handle: adding CampaignSchedulerList:", campaignSchedulerId);
                            MainDictionaryEvaluated.CampaignSchedulerList.Add(campaignSchedulerId, campaignSchedule);
                        }
                    }
                }
            }

            return MainDictionaryEvaluated;
        }

        public int NewEvaluateCampaignScheduler(
            IEnumerable<CampaignSchedule> CampaignsSchedule, 
            FilteredContainerDictionary Filtered, 
            int campaignTimeZoneId,
            Dictionary<string, string> parameters)
        {
            int campaignSchedulerId = 0;

            if (CampaignsSchedule != null)
            {
                foreach(CampaignSchedule campaignSchedule in CampaignsSchedule)
                {
                    if (Filtered == null || Filtered.TimeZoneList == null || Filtered.ScheduleOptionList == null) continue;
                    //Get campaing's time zone code ('Eastern Standard Time')
                    var campaignTimeZone = Filtered.TimeZoneList.GetValueOrDefault(campaignTimeZoneId);
                    if (campaignTimeZone == null) continue;



                    string daySelected = Filtered.ScheduleOptionList.FirstOrDefault(x => x.Value.ScheduleOptionId == campaignSchedule.ScheduleOptionId).Value.DayOfWeek;

                    TimeZoneInfo? timeZoneInfo = null;

                    if (campaignTimeZoneId == (int)TimeZoneId.Local)
                    {
                        if (parameters.TryGetValue("StateCode", out string? stateCode))
                        {
                            if (stateCode != null && Filtered.StateTimeZoneList.TryGetValue(stateCode, out StateTimeZone? stateTimeZone)){
                                timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(stateTimeZone.TimeZoneCode);
                            }
                        }

                        // Default timezone if user local timezone can't be derived
                        if (timeZoneInfo == null)
                            timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("Central Standard Time");
                        
                    }
                    else
                    {
                        timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(campaignTimeZone.Code);
                    }
                                     
                    var timeInDestinationTimeZone = TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneInfo);

                    int TodayVar = (int)timeInDestinationTimeZone.DayOfWeek;
                    TimeSpan TimeOfDay = timeInDestinationTimeZone.TimeOfDay;

                    if (daySelected.Contains(TodayVar.ToString()))
                    {
                        if (TimeOfDay >= campaignSchedule.StartTime && TimeOfDay <= campaignSchedule.EndTime)
                        {
                            campaignSchedulerId = campaignSchedule.CampaignScheduleId;
                            return campaignSchedulerId;
                        }
                    }

                }

            }

            return campaignSchedulerId;
        }
    }
}
