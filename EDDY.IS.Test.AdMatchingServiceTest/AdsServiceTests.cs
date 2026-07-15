using EDDY.IS.AdMatching.Service;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using NUnit.Framework;

namespace EDDY.IS.Test.AdMatchingServiceTest;

public class AdsServiceTests
{
    Ads.AdsClient client;

    [SetUp]
    public void Setup()
    {
        //Creating a handler with de subdirectory for the service
        var handler = new SubdirectoryHandler(new HttpClientHandler(), "/EDDY.IS.AdMatching.Service");
        var handler2 = new GrpcWebHandler(GrpcWebMode.GrpcWebText, handler);

        var channel = GrpcChannel.ForAddress("https://gp15-issvc1.eddycorp.local:4443", new GrpcChannelOptions
            { HttpClient = new HttpClient(handler2) });

        client = new Ads.AdsClient(channel);
    }

    [Test]
    public void AdMatchingServiceInvoke_Works()
    {
        //Arrange
        //Dynamic Parameters
        Google.Protobuf.Collections.MapField<string, string> DynamicParameters = new Google.Protobuf.Collections.MapField<string, string>();
        DynamicParameters.Add("currentDate", "return new string($\"{System.DateTime.Now: yyyy MM dd}\");");

        getAdMatchingRequest request = new getAdMatchingRequest();
        Guid g = Guid.NewGuid();
        request.SourceId = 1;
        request.SearchGuid = g.ToString();
        request.DynamicParameters.Add(DynamicParameters);

        //Act
        var response = client.AdMatchingServiceInvoke(request);

        //Assert
        Assert.IsNotNull(response);

    }

    [Test]
    public void AdMatchingServiceInvoke_CorrectMessage()
    {
        //Arrange
        //Dynamic Parameters
        Google.Protobuf.Collections.MapField<string, string> DynamicParameters = new Google.Protobuf.Collections.MapField<string, string>();
        DynamicParameters.Add("currentDate", "return new string($\"{System.DateTime.Now: yyyy MM dd}\");");

        getAdMatchingRequest request = new getAdMatchingRequest();
        Guid g = Guid.NewGuid();
        request.SourceId = 1;
        request.SearchGuid = g.ToString();
        request.DynamicParameters.Add(DynamicParameters);

        //Act
        var response = client.AdMatchingServiceInvoke(request);

        //Assert
        if(response.AdsReturned == 0)
        {
            Assert.AreEqual(response.Message, "No ads were matched with the supplied request parameters");
        }
        else
        {
            Assert.AreEqual(response.Message, "Succes!");
        }
            
    }

    [Test]
    public void AdMatchingServiceInvoke_NumberOfAdsReturned()
    {
        //Arrange
        //Dynamic Parameters
        Google.Protobuf.Collections.MapField<string, string> DynamicParameters = new Google.Protobuf.Collections.MapField<string, string>();
        DynamicParameters.Add("currentDate", "return new string($\"{System.DateTime.Now: yyyy MM dd}\");");

        getAdMatchingRequest request = new getAdMatchingRequest();
        Guid g = Guid.NewGuid();
        request.SourceId = 1;
        request.SearchGuid = g.ToString();
        request.DynamicParameters.Add(DynamicParameters);

        //Act
        var response = client.AdMatchingServiceInvoke(request);

        //Assert
        Assert.AreEqual(response.AdsReturned, 1);
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