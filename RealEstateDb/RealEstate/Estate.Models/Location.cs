using System.Globalization;

namespace Estate.Models;

public class Location : BaseEntity
{
    public string TownName { get; set; }
    public string Address { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
    public int PropertyId { get; set; }
    public PropertyInfo Property { get; set; }
}