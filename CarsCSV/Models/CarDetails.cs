namespace CarCSV;

public class CarDetails : Base
{
    public decimal? Mpg {  get; set; }
    public int? Cylinders { get; set; }
    public decimal? Displacement { get; set; }
    public int? HorsePower { get; set; }
    public int? Weight { get; set; }
    public decimal? Acceleration { get; set; }
    public int? Year { get; set; }
    public int? Origin { get; set; }
    public string Model { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }

}
