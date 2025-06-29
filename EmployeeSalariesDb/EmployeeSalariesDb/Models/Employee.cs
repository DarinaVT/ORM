namespace Models
{
    public class Employee : BaseEntity
    {
        public string Gender { get; set; }
        public double BaseSalary { get; set; }
        public double OvertimePay { get; set; }
        public double LongevityPay { get; set; }
        public string Grade { get; set; }

        public int DepartmentNameId { get; set; }
        public DepartmentName DepartmentName { get; set; }

        public int DivisionNameId { get; set; }
        public DivisionName Division { get; set; }
    }
}