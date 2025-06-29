namespace Infrastructure;
using CsvHelper.Configuration;
public class ModelClassMap : ClassMap<Model>
{
    public ModelClassMap()
    {
        Map(m => m.Index).Name("Index");
        Map(m => m.OrganizationID).Name("Organization Id");
        Map(m => m.Name).Name("Name");
        Map(m => m.Website).Name("Website");
        Map(m => m.Country).Name("Country");
        Map(m => m.Description).Name("Description");
        Map(m => m.Founded).Name("Founded");
        Map(m => m.Industry).Name("Industry");
        Map(m => m.EmployeesAmount).Name("Number of employees");
    }
}