using System.ComponentModel.DataAnnotations;

namespace Models;

public class Organisation : AdditionalInfo
{
    public int Index { get; set; }
    public string OrganisationID { get; set; }
    public string Name { get; set; }
    public string Website { get; set; }
    public string Description { get; set; }
    public int EmployeesAmount { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; }
    public int IndustryId { get; set; }
    public Industry Industry { get; set; }
    public int YearId { get; set; }
    public Year Year { get; set; }
}