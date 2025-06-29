namespace Infrastructure;
using CsvHelper.Configuration;

public class ModelClassMap : ClassMap<Model>
{
    public ModelClassMap()
    {
        Map(m => m.SerialNumber).Name("Serial Number");
        Map(m => m.ListYear).Name("List Year");
        Map(m => m.DateRecorded).Name("Date Recorded");
        Map(m => m.Town).Name("Town");
        Map(m => m.Address).Name("Address");
        Map(m => m.AssessedValue).Name("Assessed Value");
        Map(m => m.SaleAmount).Name("Sale Amount");
        Map(m => m.SalesRatio).Name("Sales Ratio");
        Map(m => m.PropertyType).Name("Property Type");
        Map(m => m.ResidentialType).Name("Residential Type");
        Map(m => m.NonUseCode).Name("Non Use Code");
        Map(m => m.AssessorRemarks).Name("Assessor Remarks");
        Map(m => m.OPMremarks).Name("OPM remarks");
        Map(m => m.Location).Name("Location					");
    }
}