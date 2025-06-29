namespace Infrastructure;
using CsvHelper.Configuration;
public class ModelClassMap : ClassMap<Model>
{
    public ModelClassMap()
    {
        Map(m => m.Abbreviation).Name("Department");
        Map(m => m.DepartmentName).Name("Department_Name");
        Map(m => m.Division).Name("Division");
        Map(m => m.Gender).Name("Gender");
        Map(m => m.BaseSalary).Name("Base_Salary");
        Map(m => m.OvertimePay).Name("Overtime_Pay");
        Map(m => m.LongevityPay).Name("Longevity_Pay");
        Map(m => m.Grade).Name("Grade");
    }
}