using System.ComponentModel.DataAnnotations;

namespace Models;

public class Location : Base
{
    public string County { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public int? LegislativeDistrict { get; set; }
}