namespace Models;

public class ElectricUtility : Base
{
    public string Name { get; set; }

    public ICollection<ElectricVehicleUtility> VehicleUtilities { get; set; }
}