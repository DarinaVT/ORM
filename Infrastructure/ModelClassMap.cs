namespace Infrastructure;
using CsvHelper.Configuration;
public class ModelClassMap : ClassMap<Model>
{
    public ModelClassMap()
    {
        Map(m => m.VIN).Name("VIN (1-10)");
        Map(m => m.County).Name("County");
        Map(m => m.City).Name("City");
        Map(m => m.State).Name("State");
        Map(m => m.PostalCode).Name("Postal Code");
        Map(m => m.ModelYear).Name("Model Year");
        Map(m => m.Make).Name("Make");
        Map(m => m.ModelName).Name("Model");
        Map(m => m.ElectricVehicleType).Name("Electric Vehicle Type");
        Map(m => m.CAFV).Name("Clean Alternative Fuel Vehicle (CAFV) Eligibility");
        Map(m => m.ElectricRange).Name("Electric Range");
        Map(m => m.BaseMSRP).Name("Base MSRP");
        Map(m => m.LegislativeDistrict).Name("Legislative District");
        Map(m => m.DOLVehicleID).Name("DOL Vehicle ID");
        Map(m => m.VehicleLocation).Name("Vehicle Location");
        Map(m => m.ElectricUtility).Name("Electric Utility");
        Map(m => m.CensusTract).Name("2020 Census Tract");
    }
}