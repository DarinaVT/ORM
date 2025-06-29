using System.ComponentModel.DataAnnotations;

namespace Models;

public class ElectricCar : Base
{
    public string Vin { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }

    public int MakeModelYearId { get; set; }
    public MakeModelYear MakeModelYear { get; set; }

    public string ElectricVehicleType { get; set; }
    public string CAFVEligibility { get; set; }

    public int? ElectricRange { get; set; }
    public int? BaseMsrp { get; set; }
    public string LegislativeDistric { get; set; }
    public string DOLVehicleId { get; set; }
    public string CensusTract2020 { get; set; }
    public ICollection<ElectricVehicleUtility> VehicleUtilities { get; set; }

}
