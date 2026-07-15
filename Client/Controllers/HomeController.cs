using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;
using EDDY.IS.AdMatching.Service;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;

namespace Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly string _subdirectory;

        public ActionResult Index()
        {   
            //Creating a handler with de subdirectory for the service
            var handler = new SubdirectoryHandler(new HttpClientHandler(), "/EDDY.IS.AdMatching.Service");
            var webHandler = new GrpcWebHandler(GrpcWebMode.GrpcWebText, handler);

            var channel = GrpcChannel.ForAddress("https://gp15-issvc1.eddycorp.local:4443", new GrpcChannelOptions 
            { HttpClient = new HttpClient(webHandler) });

            //Random number to simulate SearchGuid
            Random r = new Random();
            int rInt = r.Next(0, 10000);

            //Dynamic Parameters
            Google.Protobuf.Collections.MapField<string, string> DynamicParameters = new Google.Protobuf.Collections.MapField<string, string>();
            DynamicParameters.Add("currentDate", "return new string($\"{System.DateTime.Now: yyyy MM dd}\");");

            var client = new Ads.AdsClient(channel);
            //var response = client.AdsServiceInvoke(new getAdsRequest { SourceId = "1" });
            getAdMatchingRequest request = new getAdMatchingRequest();
            Guid g = Guid.NewGuid();
            request.SourceId = 1;
            request.SearchGuid = g.ToString();
            request.DynamicParameters.Add(DynamicParameters);

             var response = client.AdMatchingServiceInvoke(request);
           
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}

/// <summary>
/// A delegating handler that adds a subdirectory to the URI of gRPC requests.
/// </summary>
public class SubdirectoryHandler : DelegatingHandler
{
    private readonly string _subdirectory;

    public SubdirectoryHandler(HttpMessageHandler innerHandler, string subdirectory)
        : base(innerHandler)
    {
        _subdirectory = subdirectory;
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var url = $"{request.RequestUri.Scheme}://{request.RequestUri.Host}";
        url += $"{_subdirectory}{request.RequestUri.AbsolutePath}";
        request.RequestUri = new Uri(url, UriKind.Absolute);

        return base.SendAsync(request, cancellationToken);
    }
}