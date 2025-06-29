namespace Estate.Models;

public class PropertyInfo : BaseEntity
{
    public string SerialNumber { get; set; }
    public string ListYear { get; set; }
    public DateTime DateRecorded { get; set; }
    public string PropertyType { get; set; }
    public string ResidentialType { get; set; }
    public string NonUseCode { get; set; }
    public Location Location { get; set; }
    public Assessment Assessment { get; set; }
    public Remark Remark { get; set; }
}