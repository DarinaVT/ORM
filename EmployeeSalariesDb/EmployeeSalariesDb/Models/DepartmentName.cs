namespace Models
{
    public class DepartmentName : BaseEntity
    {
        public string Department { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}