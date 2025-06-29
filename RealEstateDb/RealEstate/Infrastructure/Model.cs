namespace Infrastructure;

public class Model
{
    public string SerialNumber { get; set; }
    public string ListYear { get; set; }
    public DateTime DateRecorded { get; set; }
    public string Town { get; set; }
    public string Address { get; set; }
    public string AssessedValue { get; set; }
    public string SaleAmount { get; set; }
    public string SalesRatio { get; set; }
    public string PropertyType { get; set; }
    public string? ResidentialType { get; set; }
    public string? NonUseCode { get; set; }
    public string? AssessorRemarks { get; set; }
    public string? OPMremarks { get; set; }
    public string? Location { get; set; }
}