namespace EDDY.IS.AdMatching.EAV.Models;

public class EddyAdsListingResponse
{
    public bool Success { get; set; }
    public int AdsCount { get; set; }
    public List<EddyVendorAd> Ads { get; set; }
    public Guid MatchingEngineGuid { get; set; }
    public int MatchingEngineCount { get; set; }
    public int PreMatchCount { get; set; }
    public string Message { get; set; }
}
public class EddyVendorAd
{
    public string InstitutionName { get; set; }
    public string ClickURL { get; set; }
    public string Header { get; set; }
    public string Description { get; set; }
    public string LargeImageURL { get; set; }
    public string MediumImageURL { get; set; }
    public string SmallImageURL { get; set; }
    public decimal CPC { get; set; }
    public decimal EstimatedRevShare { get; set; }
    public string Pixel { get; set; }
    public List<EddyVendorAdLink> AdLinks { get; set; }
    public List<Program> Programs { get; set; } = new List<Program>();
    public Address Address { get; set; }
    public string CampusName { get; set; }
    public string CampusType { get; set; }
    public int CRId { get; set; }
}

public class EddyVendorAdLink
{
    public string ClickURL { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }

}

public class Address
{
    public string Street1 { get; set; }
    public string Street2 { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
}

public class Program
{
    public int Id { get; set; }
    public string Name { get; set; }
}
