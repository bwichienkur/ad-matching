using EDDY.IS.AdMatching.Domain.Settings;
using EDDY.IS.AdMatching.EAV.Extensions;
using EDDY.IS.AdMatching.EAV.Interface;
using EDDY.IS.AdMatching.EAV.MatchingEngineService;
using EDDY.IS.AdMatching.EAV.Models;
using Microsoft.Extensions.Options;
using static EDDY.IS.AdMatching.EAV.Extensions.Enums;

namespace EDDY.IS.AdMatching.EAV.Services
{
    public class EddyAdsListingService : IEddyAdsListingService
    {
        private readonly IMatchingEngineService _matchingEngineService;
        private readonly EAVSettings _settings;
        public EddyAdsListingService(
            IMatchingEngineService matchingEngineService,
             IOptions<EAVSettings> options
            )
        {
            _matchingEngineService = matchingEngineService;
            _settings = options.Value;
        }
        public async Task<EddyAdsListingResponse> GetEddyAdsListingMatched(EddyAdsListingRequest request) 
        {
            var Result = new EddyAdsListingResponse();
            var excludedSchools = request.Parameters?.GetValueOrDefault("ExcludedInstitutions");
            var eddyClickUrl = request.Parameters?.GetValueOrDefault("EddyClickURL");
            var directoryName = request.Parameters?.GetValueOrDefault("DirectoryName");
            var subSource = request.Parameters?.GetValueOrDefault("SubSource");
            var leadInitiatingUrl = request.LeadInitiatingUrl;
            var subid = string.IsNullOrEmpty(request.Parameters?.GetValueOrDefault("SubId")) 
                                                    ? request.Parameters?.GetValueOrDefault("Sub1") 
                                                    : request.Parameters?.GetValueOrDefault("SubId");
            var categories = request.Parameters?.GetValueOrDefault("AreaOfStudy");
            var subCategories = request.Parameters?.GetValueOrDefault("Subject");
            var specialties = request.Parameters?.GetValueOrDefault("Specialties");

            try
            {

                var matchRequest = BuildMatchRequest(request, request.MaxPrograms);

                // 1. Direct call to ME to retrieve ads 
                var meResult = await _matchingEngineService.GetInstitutionsAsync(matchRequest, GetInstitutionCampusOption.CampusOn2ndLevel);

                Result.MatchingEngineCount = meResult.InstitutionList != null ? meResult.InstitutionList.Count : 0;
                Result.MatchingEngineGuid = meResult.MatchResponseGuid;
                Result.Ads = new List<EddyVendorAd>();

                if (Result.MatchingEngineCount > 0)
                {
                    Result.Success = true;

                    //2. Remove excluded schools
                    RemoveExcludedInstitutionsFromInstitutionMatch(meResult, excludedSchools ?? string.Empty);


                    ////3. Remove Duplicate For Institution
                    if (request.DuplicateForInstitutionList != null && request.DuplicateForInstitutionList.Count > 0)
                        RemoveDuplicateForInstitutionFromInstitutionMatch(meResult, request.DuplicateForInstitutionList);

                    ////4. Create Ads
                    if (meResult.InstitutionList != null)
                        foreach (var institution in meResult.InstitutionList)
                        {
                            var campus = ((InstitutionWithCampus)institution).CampusList.FirstOrDefault();

                            EddyVendorAd ad = new()
                            {
                                ClickURL = BuildClickURL(eddyClickUrl, directoryName, institution.InstitutionId, institution.InstitutionName, null,
                                                        request.TrackId.ToString(), request.PlacementViewGuid, subSource, leadInitiatingUrl, subid,
                                                        categories, subCategories, specialties, request.WidgetName, request.WidgetRequestGuid
                                                        ),
                                Description = institution.InstitutionDescription,
                                InstitutionName = institution.InstitutionName,
                                CPC = campus.ProgramList.Average(pl => pl.EffectiveRevenuePerLead ?? 0) * (decimal)request.ConversionRate,
                                EstimatedRevShare = institution.EstimatedRevShare.GetValueOrDefault(),
                                SmallImageURL = GetLogo(ImageSize.Small, string.Empty, institution.InstitutionLogoURL),
                                MediumImageURL = GetLogo(ImageSize.Medium, string.Empty, institution.InstitutionLogoURL),
                                LargeImageURL = GetLogo(ImageSize.Large, string.Empty, institution.InstitutionLogoURL),
                                Header = institution.InstitutionName,
                                AdLinks = new List<EddyVendorAdLink>(),
                                CRId = institution.CRId,
                            };

                            if (campus != null)
                            {
                                ad.CampusName = campus.CampusName;
                                ad.CampusType = campus.CampusType.Value.ToString();
                                ad.Address = new Models.Address
                                {
                                    City = campus.City,
                                    State = campus.State,
                                    Street1 = campus.Address1,
                                    Street2 = campus.Address2,
                                    Country = campus.CountryCode,
                                    PostalCode = campus.PostalCode,
                                };
                            }

                            //SD-14328 → ME is returning 1 program, even though Max Programs is set to 0
                            if (request.MaxPrograms > 0)
                                foreach (var program in campus.ProgramList)
                                {
                                    ad.AdLinks.Add(new EddyVendorAdLink()
                                    {
                                        ClickURL = BuildClickURL(eddyClickUrl, directoryName, institution.InstitutionId, institution.InstitutionName,
                                                                program.ProgramId, request.TrackId.ToString(), request.PlacementViewGuid, subSource,
                                                                leadInitiatingUrl, subid, categories, subCategories, specialties, request.WidgetName,
                                                                request.WidgetRequestGuid
                                                                ),
                                        Name = program.ProgramName,
                                        Description = program.ProgramDescription,
                                    });

                                    ad.Programs.Add(new Models.Program()
                                    {
                                        Id = program.ProgramId,
                                        Name = program.ProgramName
                                    });
                                }

                            Result.Ads.Add(ad);
                        }

                    //Top x if results > max allowed (increased by exclusions to prevent under-results)
                    if (Result.Ads != null && Result.Ads.Count > request.MaxAds)
                    {
                        Result.Ads = Result.Ads.Take(request.MaxAds).ToList();
                    }

                    Result.AdsCount = Result.Ads?.Count ?? 0;
                    Result.Message = Result.AdsCount > 0 ? "Success!" : "No ads were matched with the supplied request parameters";
                }

                return Result;
            }
            catch (Exception e)
            {
                // TODO: Add Logger instance
                Result.Success = false;
                Console.WriteLine(e.Message);
                return new EddyAdsListingResponse();
            }
        }
        private string BuildClickURL(string eddyClickUrl, string directoryName, int institutionId, string institutionName, int? programId, string trackid, string placementViewGuid, string subSource = "", string leadInitiatingUrl = "", string subid = "", string categories = "", string subCategories = "", string specialties = "", string widgetName = "", string widgetRequestGuid = "")
        {
            var Result = string.Empty;
            var subSourceField = !string.IsNullOrEmpty(subSource) ? $"&subSource={subSource}" : string.Empty;
            var leadInitiatingUrlField = !string.IsNullOrEmpty(leadInitiatingUrl) ? $"&leadInitiatingUrl={leadInitiatingUrl}" : string.Empty;
            var affiliateIdField = !string.IsNullOrEmpty(subid) ? $"&aid={subid}" : string.Empty;
            var category = !string.IsNullOrEmpty(categories) ? $"&categories={categories}" : string.Empty;
            var specialty = !string.IsNullOrEmpty(specialties) ? $"&specialties={specialties}" : string.Empty;
            var subCategory = !string.IsNullOrEmpty(subCategories) ? $"&subcategories={subCategories}" : string.Empty;

            // Legacy elerners
            if (string.IsNullOrEmpty(eddyClickUrl))
            {
                if (programId.HasValue)
                    Result = string.Format(_settings.DirectoryInstitutionProgramURL, directoryName, institutionId, programId, trackid, placementViewGuid);
                else
                    Result = string.Format(_settings.DirectoryInstitutionURL, directoryName, institutionId, trackid, placementViewGuid);

                return $"{Result}{subSourceField}{leadInitiatingUrlField}";
            }

            var urlQueryString = new Uri(eddyClickUrl).Query;
            var hasQueryString = !string.IsNullOrEmpty(urlQueryString);
            var additionalFields = $"{subSourceField}{leadInitiatingUrlField}{affiliateIdField}{category}{subCategory}{specialty}";

            // Glas panel landing pages elearners
            if (eddyClickUrl.Contains("{institutionid}") || eddyClickUrl.Contains("{programid}") || eddyClickUrl.Contains("{institutionname}"))
            {
                if ((eddyClickUrl.LastIndexOf('?') + 1) == eddyClickUrl.Length)
                    eddyClickUrl = eddyClickUrl[..^1];

                Result = eddyClickUrl
                   .Replace("{institutionid}", institutionId.ToString())
                   .Replace("{programid}", programId.HasValue ? programId.Value.ToString() : string.Empty)
                   .Replace("{institutionname}", institutionName);
                Result = Result + "?trackid=" + trackid;

                return $"{Result}{additionalFields}";
            }

            // SD-14386 → Destination URL can work without parameters and display the correct institution and program - Additional Requirement
            Result = eddyClickUrl + institutionId.ToString();
            Result += (programId.HasValue ? "/" + programId.Value.ToString() : string.Empty);
            Result += "?trackid=" + trackid;

            return $"{Result}{additionalFields}";

            // Unreachable code → SD-14386
            // New widget landing pages
            //var queryString = $"institutionid={institutionId}&institutionname={institutionName}&trackid={trackid}";

            //if (hasQueryString)
            //    return $"{eddyClickUrl}&{queryString}{additionalFields}";

            //return $"{eddyClickUrl}?{queryString}{additionalFields}";
        }

        private static void RemoveExcludedInstitutionsFromInstitutionMatch(InstitutionResponse meResult, string exclude)
        {
            List<string> ExcludedSchools = new();
            
            if (!string.IsNullOrWhiteSpace(exclude))
                ExcludedSchools = exclude.Split('|').ToList().ConvertAll(s => s.ToLowerInvariant());

            ExcludedSchools ??= new List<string>();

            for (int index = meResult.InstitutionList.Count - 1; index >= 0; index--)
            {
                var ExcludeSchool = false;

                if (meResult.InstitutionList[index] != null && !string.IsNullOrWhiteSpace(meResult.InstitutionList[index].InstitutionName))
                {
                    string InstitutionName = meResult.InstitutionList[index].InstitutionName.ToLowerInvariant();
                    foreach (var school in ExcludedSchools)
                    {
                        if (school == InstitutionName)
                        {
                            ExcludeSchool = true;
                            break;
                        }
                    }
                }

                if (ExcludeSchool)
                    meResult.InstitutionList.RemoveAt(index);
            }
        }
        private static void RemoveDuplicateForInstitutionFromInstitutionMatch(InstitutionResponse meResult, List<string> excludedInstitutionNames)
        {
            for (int index = meResult.InstitutionList.Count - 1; index >= 0; index--)
            {
                var ExcludeSchool = false;

                if (meResult.InstitutionList[index] != null && meResult.InstitutionList[index].InstitutionId > 0)
                {
                    string institutionName = meResult.InstitutionList[index].InstitutionName.ToLowerInvariant();
                    foreach (string excludedInstitutionName in excludedInstitutionNames)
                    {
                        if (institutionName == excludedInstitutionName.ToLowerInvariant())
                        {
                            ExcludeSchool = true;
                            break;
                        }
                    }
                }

                if (ExcludeSchool)
                    meResult.InstitutionList.RemoveAt(index);
            }
        }
        private string GetLogo(ImageSize imageSize, string campusLogoURL, string institutionLogoURL)
        {
            string Size = _settings.EddyLogoImageSizeSmall;

            switch (imageSize)
            {
                case ImageSize.Large:
                    Size = _settings.EddyLogoImageSizeLarge;
                    break;
                case ImageSize.Medium:
                    Size = _settings.EddyLogoImageSizeMedium;
                    break;
            }

            if (!string.IsNullOrEmpty(campusLogoURL))
                return _settings.EddyLogoImagePathDomain + campusLogoURL.Replace("{FILENAME}", string.Format(_settings.EddyLogoImageFileName, Size));
            
            return _settings.EddyLogoImagePathDomain + institutionLogoURL.Replace("{FILENAME}", string.Format(_settings.EddyLogoImageFileName, Size));
        }
        private static DirectoryMatchRequest BuildMatchRequest(EddyAdsListingRequest request, int? MaxNestedProgramCount = null, bool OnlyClickPrograms = false)
        {
            var Result = new DirectoryMatchRequest
            {
                IncludeOnlyClickPrograms = OnlyClickPrograms,
                TrackGuid = Guid.Parse(request.TrackId),
                MaxNestedProgramCount = MaxNestedProgramCount,
                MaxResultsCount = request.MaxAds,
                SortMethod = EntitySortMethod.RankScore,
                IncludeImages = false
            };

            var degrees = request.Parameters?.GetValueOrDefault("DegreeLevel");
            if (!string.IsNullOrWhiteSpace(degrees))
                Result.ProgramLevelList = degrees.Split(',').Select(f => Convert.ToInt32(f)).ToList();

            var categories = request.Parameters?.GetValueOrDefault("AreaOfStudy");
            if (!string.IsNullOrWhiteSpace(categories))
                Result.CategoryList = categories.Split(',').Select(f => Convert.ToInt32(f)).ToList();

            var subCategories = request.Parameters?.GetValueOrDefault("Subject");
            if (!string.IsNullOrWhiteSpace(subCategories))
                Result.SubjectList = subCategories.Split(',').Select(f => Convert.ToInt32(f)).ToList();

            var specialties = request.Parameters?.GetValueOrDefault("Specialties");
            if (!string.IsNullOrWhiteSpace(specialties))
                Result.SpecialtyList = specialties.Split(',').Select(f => Convert.ToInt32(f)).ToList();

            //exclude_schools --> increase the number of requested schools to account for possible
            var exclude = request.Parameters?.GetValueOrDefault("ExcludedInstitutions");
            if (!string.IsNullOrWhiteSpace(exclude))
                Result.MaxResultsCount += exclude.Split('|').Length;

            Result.RemoveInvalidEntities = true;

            //Prospect area
            Result.ProspectInput = new ProspectInput();
            int age;
            if (int.TryParse(request.Parameters?.GetValueOrDefault("Age"), out age))
                Result.ProspectInput.Age = age;

            if (!string.IsNullOrWhiteSpace(request.Parameters?.GetValueOrDefault("Email")))
                Result.ProspectInput.Email = request.Parameters.GetValueOrDefault("Email");

            if (!string.IsNullOrWhiteSpace(request.Parameters?.GetValueOrDefault("StateCode")))
            {
                USStates state;
                if (Enum.TryParse<USStates>(request.Parameters?.GetValueOrDefault("StateCode")?.ToUpper(), out state))
                    Result.ProspectInput.StateId = (int)state;
            }

            //if the zip code was figured out because of the users IP address we do not want to send that to matching engine
            if (request.Parameters?.GetValueOrDefault("ZipCodeDerived") == "No")
                Result.ProspectInput.PostalCode = request.Parameters?.GetValueOrDefault("ZipCode");

            int edLevel;
            if (int.TryParse(request.Parameters?.GetValueOrDefault("EducationLevel"), out edLevel))
                Result.ProspectInput.EducationLevelId = edLevel;

            int hsGradYear;
            if (int.TryParse(request.Parameters?.GetValueOrDefault("HighSchoolGradYear"), out hsGradYear))
                Result.ProspectInput.HSGraduationYear = hsGradYear;

            //Use these filters for possible International targeting
            var termList = request.Parameters?.GetValueOrDefault("Term");
            if (!string.IsNullOrWhiteSpace(termList))
                Result.TermList = termList.Split(',').Select(f => Convert.ToInt32(f)).ToList();

            var programTypeListString = request.Parameters?.GetValueOrDefault("ProgramType");
            if (!string.IsNullOrWhiteSpace(programTypeListString))
            {
                List<int> programTypeList = programTypeListString.Split(',').Select(f => Convert.ToInt32(f)).ToList();
                Result.ProgramTypeList = new List<ProgramType>();
                foreach (int programListType in programTypeList)
                {
                    ProgramType currentProgramType = new();
                    switch (programListType)
                    {
                        case 1:
                            currentProgramType = ProgramType.FullDegree;
                            break;
                        case 2:
                            currentProgramType = ProgramType.Courses;
                            break;
                        case 3:
                            currentProgramType = ProgramType.Certificates;
                            break;
                        case 4:
                            currentProgramType = ProgramType.StudyAbroad;
                            break;
                        case 5:
                            currentProgramType = ProgramType.VolunteerAbroad;
                            break;
                        case 6:
                            currentProgramType = ProgramType.TeachAbroad;
                            break;
                        case 7:
                            currentProgramType = ProgramType.InternAbroad;
                            break;
                        case 8:
                            currentProgramType = ProgramType.IntensiveLanguage;
                            break;
                    }
                    Result.ProgramTypeList.Add(currentProgramType);
                }
            }

            var workTypeList = request.Parameters?.GetValueOrDefault("WorkType");
            if (!string.IsNullOrWhiteSpace(workTypeList))
                Result.WorkTypeList = workTypeList.Split(',').Select(f => Convert.ToInt32(f)).ToList();

            #region This section of code was commented, after discussion with Alexis
            //var countryList = request.Parameters.GetValueOrDefault("country_selections");
            //string geoTargetState = request.Parameters.GetValueOrDefault("geotarget_state");
            //string geoTargetZipCode = request.Parameters.GetValueOrDefault("geotarget_zipcode");
            //string geoTargetZipRadius = request.Parameters.GetValueOrDefault("geotarget_zipradius");

            //if (!String.IsNullOrWhiteSpace(countryList) || !String.IsNullOrWhiteSpace(geoTargetState) || !String.IsNullOrWhiteSpace(geoTargetZipCode) || !String.IsNullOrWhiteSpace(geoTargetZipRadius))
            //    Result.GeoTarget = new GeoTarget();

            //if (!String.IsNullOrWhiteSpace(countryList))
            //    Result.GeoTarget.CountryList = countryList.Split(',').Select(f => Convert.ToInt32(f)).ToList();

            //if (!String.IsNullOrWhiteSpace(geoTargetState))
            //{
            //    USStates state = (USStates)Enum.Parse(typeof(USStates), geoTargetState, true);
            //    Result.GeoTarget.StateList = ((int)state).ToString().Split(',').Select(f => Convert.ToInt32(f)).ToList();
            //}

            //if (!String.IsNullOrWhiteSpace(geoTargetZipCode))
            //    Result.GeoTarget.PostalCode = geoTargetZipCode;

            //if (!String.IsNullOrWhiteSpace(geoTargetZipRadius))
            //{
            //    var radiusResult = int.TryParse(geoTargetZipRadius, out int radius);
            //    if (radiusResult)
            //        Result.GeoTarget.RadiusFromPostalCode = radius;
            //}
            #endregion

            if (request.ApplicationId > 0)
                Result.ApplicationId = request.ApplicationId;
            else
                //click inquiry issue in ME
                Result.ApplicationId = 24; //Ad Aggregator

            if (!string.IsNullOrWhiteSpace(request.Parameters?.GetValueOrDefault("MilitaryAffiliation")))
            {
                int militaryStatusId;
                var isValidMilitaryId = int.TryParse(request.Parameters.GetValueOrDefault("MilitaryAffiliation"), out militaryStatusId);

                if (isValidMilitaryId)
                {
                    Result.ProspectInput.MilitaryStatusId = militaryStatusId;
                    Result.ProspectInput.IsMilitary = militaryStatusId != 126;
                }
            }

            if (!string.IsNullOrWhiteSpace(request.Parameters?.GetValueOrDefault("LearningPreference")))
            {
                var campus = request.Parameters.GetValueOrDefault("LearningPreference") ?? string.Empty;

                switch (campus.ToLower())
                {
                    case "online":
                        Result.CampusType = CampusType.Online;
                        break;
                    case "campus":
                        Result.CampusType = CampusType.Ground;
                        break;
                    default:
                        break;
                }
            }

            if (!string.IsNullOrWhiteSpace(request.Parameters?.GetValueOrDefault("PlannedStart")))
            {
                Result.ProspectInput.KVCodeData = new List<KeyValuePair<string, int>>() { request.Parameters.GetValueOrDefault("PlannedStart").StartDateToKeyValue() };
            }

            if (!string.IsNullOrWhiteSpace(request.Parameters?.GetValueOrDefault("USCitizen", "null")))
            {
                bool? isUsCitizen = true;
                var usCitizenField = request.Parameters.GetValueOrDefault("USCitizen", "null").ToLower();

                isUsCitizen = usCitizenField switch
                {
                    "yes" => true,
                    "no" => false,
                    "true" => true,
                    "false" => false,
                    "null" => null,
                    _ => null,
                };

                if (isUsCitizen != null)
                {
                    var citizenValue = new KeyValuePair<string, int>("us_citizen", isUsCitizen.Value ? 22 : 23);

                    if (Result.ProspectInput.KVCodeData != null && ((List<KeyValuePair<string, int>>)Result.ProspectInput.KVCodeData).Count > 0)
                        Result.ProspectInput.KVCodeData.Add(citizenValue);
                    else
                        Result.ProspectInput.KVCodeData = new List<KeyValuePair<string, int>>() { citizenValue };
                }
            }

            return Result;
        }
    }
}
