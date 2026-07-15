using EDDY.IS.AdMatching.Core.Logging;
using EDDY.IS.AdMatching.Domain.Dto;
using EDDY.IS.AdMatching.Domain.Services.Interfaces;
using EDDY.IS.AdMatching.EAV.Interface;
using EDDY.IS.AdMatching.EAV.Models;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using NewRelic.Api.Agent;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace EDDY.IS.AdMatching.Service.Services
{
    public class AdsService : Ads.AdsBase
    {
        

        private readonly ILogger<AdsService> _logger;
        private readonly IAdMatchingService _adMatchingService;
        private readonly IEddyAdsListingService _eddyListingService;
        private readonly PerformanceLogger _performanceLog;

        public AdsService(
            ILogger<AdsService> logger,
            IAdMatchingService adMatchingService, 
            IEddyAdsListingService eddyListingService,
            PerformanceLogger performanceLogger)
        {
            _logger = logger;
            _adMatchingService = adMatchingService;
            _eddyListingService = eddyListingService;
            _performanceLog = performanceLogger;
        }

        /// <summary>
        /// This method returns a list of ads the match the input parameters
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [Transaction]
        public override async Task<getAdMatchingResponse> AdMatchingServiceInvoke(getAdMatchingRequest request, ServerCallContext context)
        {
            getAdMatchingResponse response = new getAdMatchingResponse();
            DateTime startTime = DateTime.Now;

            try
            {
                response = GetgetAdMatchingResponse(await _adMatchingService.GetAdsMatched(GetAdMatchingRequestFrom(request)));
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "AMS error at AdMatchingServiceInvoke");
                response = new getAdMatchingResponse
                {
                    Message = "There is an error: " + ex.Message
                };
            }
            finally
            {
                DateTime finishTime = DateTime.Now;
                _performanceLog.LogPerformance(request.SearchGuid, JsonSerializer.Serialize(request), JsonSerializer.Serialize(response), startTime, finishTime);
            }


            return response;
        }

        public override async Task<getEddyAdsListingResponse> EddyAdsListingServiceInvoke(getEddyAdsListingRequest request, ServerCallContext context)
        {
            getEddyAdsListingResponse response = new();
            DateTime startTime = DateTime.Now;

            try
            {
                var adsMatched = await _eddyListingService.GetEddyAdsListingMatched(GetEddyAdsListingRequestFrom(request));
                
                response = GetFormatedMatchingEngineResponse(adsMatched);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "AMS error at EddyAdsListingServiceInvoke");
                response = new getEddyAdsListingResponse
                {
                    Success = false,
                    Message = "There is an error: " + ex.Message
                };
            }
            finally
            {
                DateTime finishTime = DateTime.Now;
                _performanceLog.LogPerformance(string.Empty, JsonSerializer.Serialize(request), JsonSerializer.Serialize(response), startTime, finishTime);//request.SearchGuid, preguntar por este campo
            }
            return response;
        }
        private getAdMatchingResponse GetgetAdMatchingResponse(AdMatchingResponse getAdsMatched)
        {
            var res = new getAdMatchingResponse()
            {
                AdsReturned = getAdsMatched.AdsReturned,
                Message = getAdsMatched.Message,
            };
            foreach (var ad in getAdsMatched.AdsMatched)
            {
                res.AdsMatched.Add( new getAdMatchingResponse.Types.AdsMatchedResponse
                {
                    AdId = ad.AdId,
                    AdClickUrl = ad.AdClickUrl??string.Empty,
                    AdDescription = ad.AdDescription,
                    AdDisplayUrl = ad.AdDisplayUrl??string.Empty,
                    AdHeader = ad.AdHeader,
                    AdName = ad.AdName,
                    BoostedCPC = ad.BoostedCPC,
                    CampaignId = ad.CampaignId,
                    InstitutionName = ad.InstitutionName,
                    LargeImageURL = ad.LargeImageURL??string.Empty,
                    MediumImageURL = ad.MediumImageURL??string.Empty,
                    OriginalImageURL = ad.OriginalImageURL??string.Empty,
                    RealCPC = ad.RealCPC,
                    SmallImageURL = ad.SmallImageURL,
                    AdGroupId = ad.AdGroupId,
                    ClientAdAccountId = ad.ClientAdAccountId,
                    CRId = ad.ClientRelationshipId,
                    PopularPrograms = ad.PopularPrograms,
                    ClientToken = ad.ClientToken.ToString(),
                    ProductTypeId = ad.ProductTypeId
                });
            }
            return res;
        }

        private static getEddyAdsListingResponse GetFormatedMatchingEngineResponse(EddyAdsListingResponse getAdsListingMatched)
        {
            var response = new getEddyAdsListingResponse()
            {
                Success = getAdsListingMatched.Success,
                AdsCount= getAdsListingMatched.AdsCount,
                MatchingEngineGuid = getAdsListingMatched.MatchingEngineGuid.ToString(),
                MatchingEngineCount = getAdsListingMatched.MatchingEngineCount,
                PreMatchCount = getAdsListingMatched.PreMatchCount,
                Message = getAdsListingMatched.Message,
            };

            foreach (var ad in getAdsListingMatched.Ads)
            {
                var eddyAd = new getEddyAdsListingResponse.Types.EddyVendorAd
                {
                    InstitutionName = ad.InstitutionName,
                    ClickURL = ad.ClickURL,
                    Header = ad.Header,
                    Description = ad.Description,
                    LargeImageURL = ad.LargeImageURL,
                    MediumImageURL = ad.MediumImageURL,
                    SmallImageURL = ad.SmallImageURL,
                    CPC = (double)ad.CPC,
                    EstimatedRevShare = (double)ad.EstimatedRevShare,
                    Pixel = ad.Pixel ?? string.Empty,
                    CampusName = ad.CampusName ?? string.Empty,
                    CampusType = ad.CampusType ?? string.Empty,
                    Addres = new getEddyAdsListingResponse.Types.EddyVendorAd.Types.Address
                    {
                        Street1 = ad.Address.Street1 ?? string.Empty,
                        Street2 = ad.Address.Street2 ?? string.Empty,
                        City = ad.Address.City ?? string.Empty,
                        PostalCode = ad.Address.PostalCode ?? string.Empty,
                        State = ad.Address.State ?? string.Empty,
                        Country = ad.Address.Country ??string.Empty,
                    },
                    CRId = ad.CRId,
                };
                
                foreach (var adLink in ad.AdLinks)
                {
                    eddyAd.AdLinks.Add(new getEddyAdsListingResponse.Types.EddyVendorAd.Types.EddyVendorAdLink
                    {
                        ClickURL = adLink.ClickURL ?? string.Empty,
                        Name = adLink.Name ?? string.Empty,
                        Description = adLink.Description ?? string.Empty,
                    });
                }

                foreach(var program in ad.Programs)
                {
                    eddyAd.Programs.Add(new getEddyAdsListingResponse.Types.EddyVendorAd.Types.Program
                    {
                        Id = program.Id,
                        Name = program.Name ?? string.Empty,
                    });
                }
                
                response.Ads.Add(eddyAd);
            }

            return response;
        }

        private AdMatchingRequest GetAdMatchingRequestFrom(getAdMatchingRequest request)
        {
            return new AdMatchingRequest()
            {
                MaxAds = request.MaxAds,
                Parameters = new Dictionary<string, string>(request.Parameters),
                SearchGuid = request.SearchGuid,
                SourceId = request.SourceId,
                StaticAdGuid = request.StaticAdGuid,
                PreExcludeInstitutions = new List<string>(request.PreExcludeInstitutions)
            };
        }

        private static EddyAdsListingRequest GetEddyAdsListingRequestFrom(getEddyAdsListingRequest request)
        {
            return new EddyAdsListingRequest(request.MaxAds, request.TrackId, request.PlacementViewGuid
                                            , request.MaxPrograms, request.ConversionRate, request.WidgetName
                                            , request.WidgetRequestGuid, new Dictionary<string, string>(request.Parameters)
                                            , request.ApplicationId, new List<string>(request.DuplicateForInstitutionList)
                                            , request.LeadInitiatingUrl
                                            );

        }
    }
}

