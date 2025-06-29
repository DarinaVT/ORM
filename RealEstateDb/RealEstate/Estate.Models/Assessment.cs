namespace Estate.Models;

public class Assessment : BaseEntity
{
    public decimal AssessedValue { get; set; }
    public decimal SaleAmount { get; set; }
    public decimal SalesRatio { get; set; }
    public int PropertyId { get; set; }
    public PropertyInfo Property { get; set; }
}