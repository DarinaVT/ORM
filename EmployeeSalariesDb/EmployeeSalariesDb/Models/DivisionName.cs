namespace Models
{
    public class DivisionName : BaseEntity
    {
        public string Division { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}